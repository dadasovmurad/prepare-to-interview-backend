using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Extensions;
using PrepareToInterview.Application.Features.Base;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBuddy.Models.Paging;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion
{
    public class GetAllQuestionQuery : BasePagedQuery<PagedResponse<QuestionListDto>>
    {
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, PagedResponse<QuestionListDto>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }
         
            public async Task<PagedResponse<QuestionListDto>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
            {
                var includedData = await _questionReadRepository.GetAll()
                                                         .Include(q => q.Answers)
                                                         .Include(q => q.Comments)
                                                         .GetPageAsync(request.PageNumber, request.PageSize);

                return _mapper.Map<PagedResponse<QuestionListDto>>(includedData);
            }
        }
    }
}