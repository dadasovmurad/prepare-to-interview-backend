using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.Features.Commands.Contributions.CreateContribution
{
    public class CreateContributionCommand : IRequest<IResult>
    {
        public string UserPassKey { get; set; }
        public string QuestionTitle { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Difficulty { get; set; }
        public List<string>? Tags { get; set; }
        public string Experience { get; set; }
        public string Answer { get; set; }
        public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, IResult>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IContributionReadRepository _contributionReadRepository;
            private readonly IContributionWriteRepository _contributionWriteRepository;
            private readonly IMapper _mapper;

            public CreateContributionCommandHandler(IUserReadRepository userReadRepository, IContributionReadRepository contributionReadRepository, IContributionWriteRepository contributionWriteRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _contributionReadRepository = contributionReadRepository;
                _contributionWriteRepository = contributionWriteRepository;
                _mapper = mapper;
            }

            public async Task<IResult> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var appUser = await _userReadRepository.GetUserByPassKeyAsync(request.UserPassKey);
                    if (appUser is null)
                        return new ErrorResult("User not found.");

                    var contribution = _mapper.Map<Contribution>(request);
                    contribution.User = appUser;

                    await _contributionWriteRepository.AddAsync(contribution);
                    await _contributionWriteRepository.SaveAsync();

                    return new SuccessResult("Contribution successfully created.");
                }
                catch (Exception exc)
                {

                }
                return new ErrorResult("");
            }
        }
    }
}