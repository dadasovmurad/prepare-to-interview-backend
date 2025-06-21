using MediatR;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System.Threading;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Users.CheckUserEmailExists
{
    public class CheckUserEmailExistsQuery : IRequest<bool>
    {
        public string Email { get; set; }
    }

    public class CheckUserEmailExistsQueryHandler : IRequestHandler<CheckUserEmailExistsQuery, bool>
    {
        private readonly IUserReadRepository _userReadRepository;

        public CheckUserEmailExistsQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<bool> Handle(CheckUserEmailExistsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(u => u.Email == request.Email);
            return user is not null;
        }
    }
}