#region IMPORTS
using Alfasoft.Controllers;
using Alfasoft.Models;
using Alfasoft.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication2.Controllers;
using WebApplication2.Models;
#endregion

public class Tests
{
    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        // Arrange
        var controller = new HomeController(null); 

        // Act
        var result = controller.Privacy();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Error_ReturnsViewResult_WithErrorViewModel()
    {
        // Arrange
        var controller = new HomeController(null);

        // Act
        var result = controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.True(model.ShowRequestId);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithPerson()
    {
        // Arrange
        var mockService = new Mock<PersonService>();
        mockService.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
                   .ReturnsAsync(new Person { Id = 1, Name = "John Doe" });
        var controller = new PeopleController(null, mockService.Object, null, null);

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Person>(viewResult.Model);
        Assert.Equal("John Doe", model.Name);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenPersonDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<PersonService>();
        mockService.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>()))
                   .ReturnsAsync(value: null);
        var controller = new PeopleController(null, mockService.Object, null, null);

        // Act
        var result = await controller.Details(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateOrEdit_ReturnsViewResult_ForEdit()
    {
        // Arrange
        var mockService = new Mock<PersonService>();
        mockService.Setup(service => service.GetPersonByIdAsync(1))
                   .ReturnsAsync(new Person { Id = 1, Name = "John Doe" });
        var controller = new PeopleController(null, mockService.Object, null, null);

        // Act
        var result = await controller.CreateOrEdit(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Person>(viewResult.Model);
        Assert.Equal("John Doe", model.Name);
    }

    [Fact]
    public async Task CreateOrEdit_ReturnsViewResult_ForCreate()
    {
        // Arrange
        var controller = new PeopleController(null, null, null, null);

        // Act
        var result = await controller.CreateOrEdit((int?)null);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<Person>(viewResult.Model); 
    }


    [Fact]
    public async Task Delete_RedirectsAfterDeletion()
    {
        // Arrange
        var mockService = new Mock<PersonService>();
        mockService.Setup(service => service.DeletePersonAsync(It.IsAny<int>()))
                   .Returns(Task.CompletedTask);
        var controller = new PeopleController(null, mockService.Object, null, null);

        // Act
        var result = await controller.Delete(1);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }
}
