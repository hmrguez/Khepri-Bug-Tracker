using BugTrackerWebApp.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BugTrackerTests.Controllers;

public class ProjectControllerTests
{
    private readonly ProjectController _sut;
    private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
    private readonly Mock<ITrackableRepository> _trackableRepository = new();
    private readonly Mock<UserManager<AppUser>> _userManagerMock = new();
    
    public ProjectControllerTests()
    {
        _sut = new ProjectController(_projectRepositoryMock.Object, _trackableRepository.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task Index_ShouldReturnSuccess()
    {
        //Arrange
        var user = new AppUser() { Email = "zealot.algo@gmail.com", Id = "1"};
        var projects = new List<Project>
        {
            new()
            {
                AppUserId = "1",
                Name = "Bug Tracker",
            },
            new()
            {
                AppUserId = "2",
                Name = "Bug Tracker 2",
            },
            new()
            {
                AppUserId = "1",
                Name = "Bug Tracker 3",
            }
        };
        _userManagerMock.Setup(x => x.GetUserAsync(_sut.User)).ReturnsAsync(user);
        _projectRepositoryMock.Setup(x => x.GetByUser(user.Id))
            .ReturnsAsync(new Project[] { projects[0], projects[2] });
        
    
        //Act
        var result = await _sut.Index();

        //Assert
        result.Should().BeOfType<IActionResult>();
    }
}