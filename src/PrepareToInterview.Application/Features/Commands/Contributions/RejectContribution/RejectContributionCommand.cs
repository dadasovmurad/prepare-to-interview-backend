using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PrepareToInterview.Application.Constants;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;

namespace PrepareToInterview.Application.Features.Commands.Contributions.RejectContribution
{
    public class RejectContributionCommand: IRequest<IResult>
    {
        public int ContributionId { get; set; }
        public string Note {get;set;}
        public class RejectContributionCommandHandler : IRequestHandler<RejectContributionCommand, IResult>
        {
            private readonly IMapper _mapper;
            private readonly IContributionReadRepository _contributionReadRepository;
            private readonly IContributionWriteRepository _contributionWriteRepository;
            private readonly IConfiguration _configuration;
            private readonly IUserReadRepository _userReadRepository;
            
            public RejectContributionCommandHandler(
                IMapper mapper,
                IContributionReadRepository contributionReadRepository,
                IContributionWriteRepository contributionWriteRepository,
                IConfiguration configuration,
                IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _contributionReadRepository = contributionReadRepository;
                _contributionWriteRepository = contributionWriteRepository;
                _configuration = configuration;
                _userReadRepository = userReadRepository;
            }

            public async Task<IResult> Handle(RejectContributionCommand request, CancellationToken cancellationToken)
            {
                var contribution = await _contributionReadRepository.GetAsync(x => x.Id == request.ContributionId);
                if (contribution is null)
                {
                    return new ErrorResult(Messages.ContributionNotFound);
                }
                contribution.Status = Domain.Enums.ContributionStatus.Rejected;
                contribution.Note = request.Note;

                await _contributionWriteRepository.SaveAsync();

                // Send rejection email to the user
                var user = await _userReadRepository.GetAsync(u => u.Id == contribution.UserId);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var smtpSection = _configuration.GetSection("Smtp");
                    var smtpHost = smtpSection["Host"];
                    var smtpPort = int.Parse(smtpSection["Port"]);
                    var smtpUser = smtpSection["User"];
                    var smtpPass = smtpSection["Pass"];
                    var subject = "Töhfəniz qəbul edilmədi";
                    var sb = new StringBuilder();
                    sb.Append("<div style='font-family:sans-serif;max-width:500px;margin:auto;border:1px solid #e57373;padding:24px;border-radius:8px;background:#fff8f8;'>");
                    sb.Append("<h2 style='color:#c62828;'>Töhfə rədd edildi</h2>");
                    sb.Append("<p>Hörmətli <b>" + user.FullName + "</b>,</p>");
                    sb.Append("<p><b>\"" + contribution.QuestionTitle + "\"</b> başlıqlı töhfənizə görə təşəkkür edirik.</p>");
                    sb.Append("<p>Təəssüf ki, töhfəniz aşağıdakı səbəbə görə qəbul edilmədi:</p>");
                    sb.Append("<blockquote style='background:#ffebee;padding:12px 16px;border-left:4px solid #e57373;margin:16px 0;color:#b71c1c;'>" + contribution.Note + "</blockquote>");
                    sb.Append("<p>Siz həmişə təkmilləşdirib yenidən göndərə bilərsiniz. İcmanın bir hissəsi olduğunuz üçün təşəkkür edirik!</p>");
                    sb.Append("<hr style='border:none;border-top:1px solid #e0e0e0;margin:24px 0;'>");
                    sb.Append("<p style='font-size:13px;color:#888;'>Bu avtomatik göndərilmiş mesajdır. Zəhmət olmasa cavab verməyin.</p>");
                    sb.Append("</div>");
                    var body = sb.ToString();
                    await MailHelper.SendEmailAsync(smtpHost, smtpPort, smtpUser, smtpPass, user.Email, subject, body);
                }

                return new SuccessResult();
            }
        }
    }
}