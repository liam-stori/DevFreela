using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputIdIsOk_Executed_ReturnNone()
        {
            //Arrange
            var project = new Project("Titulo delete", "Descrição delete", 1, 2, 10000);
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetByIdAsync(project.Id).Result).Returns(project);

            var deleteProjectCommand = new DeleteProjectCommand(project.Id);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());


            //Assert
            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(project.Id), Times.Once());
            projectRepositoryMock.Verify(pr => pr.SaveChangesAsync(), Times.Once());
        }
    }
}
