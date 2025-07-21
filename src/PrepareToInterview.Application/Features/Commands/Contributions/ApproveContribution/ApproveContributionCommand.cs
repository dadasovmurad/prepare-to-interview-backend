using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PrepareToInterview.Application.Constants;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace PrepareToInterview.Application.Features.Commands.Contributions.AcceptContribution
{
    public class ApproveContributionCommand : IRequest<IResult>
    {
        public int ContributionId { get; set; }

        public class ApproveContributionCommandHandler : IRequestHandler<ApproveContributionCommand, IResult>
        {
            private readonly IMapper _mapper;
            private readonly IContributionReadRepository _contributionReadRepository;
            private readonly IContributionWriteRepository _contributionWriteRepository;
            private readonly IQuestionWriteRepository _questionWriteRepository;
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categorWriteRepository;
            private readonly ITagReadRepository _tagReadRepository;
            private readonly ITagWriteRepository _tagWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IConfiguration _configuration;

            public ApproveContributionCommandHandler(IMapper mapper, IContributionReadRepository contributionReadRepository, IContributionWriteRepository contributionWriteRepository, IQuestionWriteRepository questionWriteRepository, IQuestionReadRepository questionReadRepository, ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categorWriteRepository, ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository, IUserReadRepository userReadRepository, IConfiguration configuration)
            {
                _mapper = mapper;
                _contributionReadRepository = contributionReadRepository;
                _contributionWriteRepository = contributionWriteRepository;
                _questionWriteRepository = questionWriteRepository;
                _questionReadRepository = questionReadRepository;
                _categoryReadRepository = categoryReadRepository;
                _categorWriteRepository = categorWriteRepository;
                _tagReadRepository = tagReadRepository;
                _tagWriteRepository = tagWriteRepository;
                _userReadRepository = userReadRepository;
                _configuration = configuration;
            }

            public async Task<IResult> Handle(ApproveContributionCommand request, CancellationToken cancellationToken)
            {
                var contribution = await _contributionReadRepository.GetAsync(x => x.Id == request.ContributionId);
                if (contribution is null)
                {
                    return new ErrorResult(Messages.ContributionNotFound);
                }
                // Get or create main category
                var category = await _categoryReadRepository.GetAsync(x => x.Name == contribution.CategoryName);
                Category subCategory = null;
                if (category is null)
                {
                    category = await _categorWriteRepository.AddAsync(new Domain.Entities.Category
                    {
                        Name = contribution.CategoryName,
                    });
                    await _categorWriteRepository.SaveAsync();
                }

                // Handle subcategory if provided
                if (!string.IsNullOrEmpty(contribution.SubCategoryName))
                {
                    // Check if subcategory already exists under this parent category
                    var existingSubCategory = await _categoryReadRepository.GetAsync(x =>
                        x.Name == contribution.SubCategoryName && x.ParentId == category.Id);

                    if (existingSubCategory is null)
                    {
                        // Create new subcategory
                        subCategory = await _categorWriteRepository.AddAsync(new Domain.Entities.Category
                        {
                            Name = contribution.SubCategoryName,
                            ParentId = category.Id // Assuming you have ParentId property
                        });
                        await _categorWriteRepository.SaveAsync();

                        // Initialize Children collection if null and add the subcategory
                        if (category.Children == null)
                        {
                            category.Children = new List<Category>();
                        }
                        category.Children.Add(subCategory);
                        await _categorWriteRepository.SaveAsync();
                    }
                }

                contribution.Status = Domain.Enums.ContributionStatus.Accepted;
                string[] tagsJson = JsonConvert.DeserializeObject<string[]>(contribution.Tags);

                // Process tags - check if they exist, create if they don't
                var questionTags = new List<QuestionTag>();

                foreach (string tagName in tagsJson)
                {
                    // Check if tag already exists
                    var existingTag = await _tagReadRepository.GetAsync(x => x.Name == tagName);

                    Tag tag;
                    if (existingTag == null)
                    {
                        // Create new tag if it doesn't exist
                        tag = await _tagWriteRepository.AddAsync(new Tag
                        {
                            Name = tagName
                        });
                        await _tagWriteRepository.SaveAsync();
                    }
                    else
                    {
                        // Use existing tag
                        tag = existingTag;
                    }

                    // Create QuestionTag relationship
                    questionTags.Add(new QuestionTag
                    {
                        TagID = tag.Id,
                        Tag = tag
                        // QuestionID will be set after question is created
                    });
                }
                string shortUrl;
                do
                {
                    shortUrl = ShortUrlHelper.GenerateShortUrl();
                }
                while (await _questionReadRepository.AnyAsync(q => q.ShortUrl == shortUrl));

                // Create the question
                var question = await _questionWriteRepository.AddAsync(new Domain.Entities.Question
                {
                    Category = subCategory ?? category,
                    Content = contribution.QuestionTitle,
                    UserId = contribution.UserId,
                    QuestionTags = questionTags, // Use the processed tags
                    Difficulty = Domain.Enums.Difficulty.Hard,
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            Content = contribution.Answer,
                            // QuestionId will be set automatically by EF Core
                        }
                    },
                    ShortUrl = shortUrl
                });

                await _questionWriteRepository.SaveAsync();

                // Send congratulatory email to the user
                var user = await _userReadRepository.GetAsync(u => u.Id == contribution.UserId);
                //if (user != null && !string.IsNullOrEmpty(user.Email))
                //{
                //    var smtpSection = _configuration.GetSection("Smtp");
                //    var smtpHost = smtpSection["Host"];
                //    var smtpPort = int.Parse(smtpSection["Port"]);
                //    var smtpUser = smtpSection["User"];
                //    var smtpPass = smtpSection["Pass"];
                //    var subject = "Təbriklər! Töhfəniz qəbul olundu";
                //    var sb = new StringBuilder();
                //    sb.Append("<div style='font-family:sans-serif;max-width:500px;margin:auto;border:1px solid #e0e0e0;padding:24px;border-radius:8px;background:#fafcff;'>");
                //    sb.Append("<h2 style='color:#2e7d32;'>🎉 Təbriklər!</h2>");
                //    sb.Append("<p>Hörmətli <b>" + user.FullName + "</b>,</p>");
                //    sb.Append("<p>Sizin <b>\"" + contribution.QuestionTitle + "\"</b> başlıqlı töhfəniz <span style='color:#388e3c;font-weight:bold;'>qəbul olundu</span>!</p>");
                //    sb.Append("<p>İcmanın inkişafına verdiyiniz töhfəyə görə təşəkkür edirik. Uğurlarınızın davamını arzulayırıq!</p>");
                //    sb.Append("<hr style='border:none;border-top:1px solid #e0e0e0;margin:24px 0;'>");
                //    sb.Append("<p style='font-size:13px;color:#888;'>Bu avtomatik göndərilmiş mesajdır. Zəhmət olmasa cavab verməyin.</p>");
                //    sb.Append("</div>");
                //    var body = sb.ToString();
                //    await MailHelper.SendEmailAsync(smtpHost, smtpPort, smtpUser, smtpPass, user.Email, subject, body);
                //}

                return new SuccessResult();
            }
        }
    }
}
