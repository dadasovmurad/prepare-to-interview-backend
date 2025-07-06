using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Application.DTOs;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Questions.GetQuestionByShortUrl
{
    public class GetQuestionByShortUrlQuery : IRequest<IDataResult<QuestionGetByShortUrlDto>>
    {
        public string ShortUrl { get; set; }

        public class GetBuIdQuestionQueryHanler : IRequestHandler<GetQuestionByShortUrlQuery, IDataResult<QuestionGetByShortUrlDto>>
        {
            private readonly IQuestionReadRepository _questionReadRepository;
            private readonly IMapper _mapper;

            public GetBuIdQuestionQueryHanler(IQuestionReadRepository questionReadRepository, IMapper mapper)
            {
                _questionReadRepository = questionReadRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<QuestionGetByShortUrlDto>> Handle(GetQuestionByShortUrlQuery request, CancellationToken cancellationToken)
            {
                if (!await _questionReadRepository.AnyAsync(x => x.ShortUrl == request.ShortUrl))
                {
                    return new ErrorDataResult<QuestionGetByShortUrlDto>("Question not found");
                }
                var targetQuestion = await _questionReadRepository.GetAll(q => q.ShortUrl == request.ShortUrl)
                                                        .Include(q => q.Answers)
                                                        .Include(u => u.User)
                                                        .Include(q => q.QuestionTags)
                                                        .ThenInclude(x => x.Tag)
                                                        .FirstAsync();
                if (targetQuestion is not null)
                {
                    var resultData = _mapper.Map<QuestionGetByShortUrlDto>(targetQuestion);
                    return new SuccessDataResult<QuestionGetByShortUrlDto>(resultData);
                }
                return new ErrorDataResult<QuestionGetByShortUrlDto>();
            }
        }
    }
}
