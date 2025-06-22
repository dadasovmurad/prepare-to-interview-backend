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
using PrepareToInterview.Domain.Entities;

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

            public ApproveContributionCommandHandler(IMapper mapper, IContributionReadRepository contributionReadRepository, IContributionWriteRepository contributionWriteRepository, IQuestionWriteRepository questionWriteRepository, IQuestionReadRepository questionReadRepository, ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categorWriteRepository, ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository)
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
                        var subCategory = await _categorWriteRepository.AddAsync(new Domain.Entities.Category
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

                // Create the question
                var question = await _questionWriteRepository.AddAsync(new Domain.Entities.Question
                {
                    Category = category,
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
                });

                await _questionWriteRepository.SaveAsync();

                return new SuccessResult();
            }
        }
    }
}
