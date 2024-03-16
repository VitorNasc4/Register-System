using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectName.Core.Entities;
using ProjectName.Core.Repositories;
using ProjectName.Core.Services;
using ProjectName.Infrastructure.Persistence;
using MediatR;

namespace ProjectName.Application.Commands.UserCommands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService, INotificationService notificationService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _notificationService = notificationService;
        }
        public async Task<int?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            var user = CreateUserCommand.ToEntity(request, passwordHash);

            var userExist = await _userRepository.UserExistAsync(user.Email);
            if (userExist)
            {
                return null;
            }
            await _userRepository.AddAsync(user);

            var notificationInfoDTO = User.ToDTO(user);

            _notificationService.ProcessNotification(notificationInfoDTO);

            return user.Id;
        }
    }
}