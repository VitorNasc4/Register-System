using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectName.Application.ViewModels;
using ProjectName.Core.Repositories;
using MediatR;

namespace ProjectName.Application.Queries.GetUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAllUsersAsync(request.Query);

            if (user.Count == 0 || user is null)
            {
                return null;
            }

            return user
                .Select(u => new UserViewModel(fullName: u.FullName, email: u.Email))
                .ToList();
        }
    }
}