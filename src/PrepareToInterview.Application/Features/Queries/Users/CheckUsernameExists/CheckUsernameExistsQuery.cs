using MediatR;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Results;
using System.Threading;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Queries.Users.CheckUsernameExists
{
    public class CheckUsernameExistsQuery : IRequest<bool>
    {
        public string Username { get; set; }
    }

    public class CheckUsernameExistsQueryHandler : IRequestHandler<CheckUsernameExistsQuery, bool>
    {
        private readonly IUserReadRepository _userReadRepository;

        public CheckUsernameExistsQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<bool> Handle(CheckUsernameExistsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(u => u.Username == request.Username);
            return user is not null;
        }
    }
} 