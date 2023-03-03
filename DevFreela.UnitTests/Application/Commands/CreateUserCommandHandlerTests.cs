using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand
            {
                Email = "email@email.com",
                Password = "SenhaDificil123@@",
                FullName = "Nome de Teste Unitário",
                BirthDate = DateTime.Now,
                Role = "Client"
            };
            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            //Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(createUserCommand.Password), Times.Once);
            userRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<User>()), Times.Once);
            userRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }
    }
}
