using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputIdExists_Executed_ReturnNone()
        {
            //Arrange
            var project = new Project("Titulo start", "Descrição start", 1, 2, 10000);
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(project.Id).Result).Returns(project);

            var startProjectCommand = new StartProjectCommand(project.Id);
            var startProjectCommandHandler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken());


            //Assert
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(project.Id), Times.Once());
            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once());
        }
    }
}
