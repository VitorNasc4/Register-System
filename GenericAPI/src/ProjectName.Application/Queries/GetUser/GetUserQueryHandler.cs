using System.Threading;
using System.Threading.Tasks;
using ProjectName.Application.ViewModels;
using ProjectName.Core.Repositories;
using MediatR;
using ProjectName.Infrastructure.CacheService;

namespace ProjectName.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;


        public GetUserQueryHandler(IUserRepository userRepository, ICacheService cache)
        {
            _userRepository = userRepository;
            _cache = cache;

        }
        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = request.Id.ToString();
            var userViewModel = await _cache.GetAsync<UserViewModel>(cacheKey);

            if (userViewModel is null)
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);

                if (user == null)
                {
                    return null;
                }

                userViewModel = new UserViewModel(user.FullName, user.Email);

                await _cache.SetAsync(cacheKey, userViewModel);
            }

            return userViewModel;
        }
    }
}