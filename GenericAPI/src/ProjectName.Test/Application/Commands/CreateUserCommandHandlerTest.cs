using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Application.Commands.UserCommands.CreateUser;
using ProjectName.Core.Entities;
using ProjectName.Core.Repositories;
using ProjectName.Core.Services;
using ProjectName.Test.Mocks;
using Moq;
using Xunit;

namespace ProjectName.Test.Application.Commands
{
    public class CreateUserCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand
            {
                BirthDate = DateTime.UtcNow,
                Email = "email@email.com",
                FullName = "Teste",
                Password = "Teste",
                Role = "client"
            };

            var sut = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);
            var id = await sut.Handle(createUserCommand, new CancellationToken());

            Assert.True(id >= 0);
            userRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<User>()), Times.Once);
            authServiceMock.Verify(a => a.ComputeSha256Hash(createUserCommand.Password), Times.Once);
        }
    }
}