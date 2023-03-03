using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataCommentIsOk_Executed_ReturnNone()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createCommentCommand = new CreateCommentCommand
            {
                Content = "Conteúdo de teste",
                IdProject = 1,
                IdUser = 1
            };
            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock.Object);
            
            //Act
            await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(pr => pr.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once());
            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once());
        }
    }
}
