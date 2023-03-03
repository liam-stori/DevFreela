using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataUpdateIsOk_Executed_ReturnNone()
        {
            //Arrange
            var project = new Project("Titulo update", "Descrição update", 1, 2, 10000);
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(project.Id).Result).Returns(project);

            var updateProjectCommand = new UpdateProjectCommand(project.Id);
            var updateProjectCommandHandler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(project.Id), Times.Once());
            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once());
        }
    }
}
