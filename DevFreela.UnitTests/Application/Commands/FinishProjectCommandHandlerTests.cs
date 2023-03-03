using DevFreela.Application.Commands.FinishProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputIdExists_Executed_ReturnNone()
        {
            //Arrange
            var project = new Project("Titulo finish", "Descrição finish", 1, 2, 10000);
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(project.Id).Result).Returns(project);

            var finishProjectCommand = new FinishProjectCommand(project.Id);
            var finishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(project.Id), Times.Once);
            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once);
        }
    }
}
