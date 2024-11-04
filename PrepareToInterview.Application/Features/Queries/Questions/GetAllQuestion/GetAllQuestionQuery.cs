using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion
{
    public class GetAllQuestionQuery : IRequest<IDataResult<QuestionListModel>>
    {
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, IDataResult<QuestionListModel>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;
            public GetAllQuestionQueryHandler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<QuestionListModel>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
            {
                var includedData = await _questionReadRepository.GetAll()
                                                         .Include(q => q.Answers)
                                                         .Include(q => q.Comments)
                                                         .ToListAsync();

                var questionListDto = _mapper.Map<List<QuestionListDto>>(includedData);


                QuestionListModel questionListModel = new QuestionListModel()
                {
                    Items = questionListDto
                };

                return new SuccessDataResult<QuestionListModel>(questionListModel);
            }
        }
    }
}
