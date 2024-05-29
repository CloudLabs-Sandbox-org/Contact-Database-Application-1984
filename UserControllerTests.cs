using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _userlist;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new UserController();
            _userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" },
                new User { Id = 3, Name = "Bob", Email = "bob@example.com" }
            };
            _controller.userlist = _userlist;
        }

        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist, result.Model);
        }

        [TestMethod]
        public void Details_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            int id = 2;

            // Act
            var result = _controller.Details(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == id), result.Model);
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            int id = 4;

            // Act
            var result = _controller.Details(id) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ReturnsView()
        {
            // Arrange

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 4, Name = "Alice", Email = "alice@example.com" };

            // Act
            var result = _controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            int id = 3;

            // Act
            var result = _controller.Edit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == id), result.Model);
        }

        [TestMethod]
        public void Edit_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            int id = 4;

            // Act
            var result = _controller.Edit(id) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_ValidId_UpdatesUserAndRedirectsToIndex()
        {
            // Arrange
            int id = 2;
            var user = new User { Id = 2, Name = "Updated Name", Email = "updated@example.com" };

            // Act
            var result = _controller.Edit(id, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(user.Name, _userlist.FirstOrDefault(u => u.Id == id).Name);
            Assert.AreEqual(user.Email, _userlist.FirstOrDefault(u => u.Id == id).Email);
        }

        [TestMethod]
        public void Delete_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == id), result.Model);
        }

        [TestMethod]
        public void Delete_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            int id = 4;

            // Act
            var result = _controller.Delete(id) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmed_ValidId_RemovesUserAndRedirectsToIndex()
        {
            // Arrange
            int id = 3;

            // Act
            var result = _controller.DeleteConfirmed(id) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(_userlist.FirstOrDefault(u => u.Id == id));
        }

        [TestMethod]
        public void Delete_FormCollection_ValidId_RemovesUserAndRedirectsToIndex()
        {
            // Arrange
            int id = 2;
            var collection = new FormCollection();

            // Act
            var result = _controller.Delete(id, collection) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(_userlist.FirstOrDefault(u => u.Id == id));
        }
    }
}
