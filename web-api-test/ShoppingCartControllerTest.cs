using System;
using System.Collections.Generic;
using interview_katar;
using interview_katar.Controllers;
using interview_katar.Services;
using Microsoft.AspNetCore.Mvc;
using web_api_test.Services;
using Xunit;

namespace web_api_test
{
    public class ShoppingCartControllerTest
    {
        ShoppingCartController _controller;
        IShoppingCartService _service;

        public ShoppingCartControllerTest()
        {
            _service = new ShoppingCartServiceFake();
            _controller = new ShoppingCartController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnOkResult()
        {
            //Act
            var items = _controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(items.Result);
        }

        [Fact]
        public void GetAll_WhenCall_ReturnAllItems()
        {
            //Act
            var okResult = _controller.Get().Result as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_WhenCall_ReturnNotFound()
        {
            //Act
            var item = _controller.Get(6);

            //Assert
            Assert.IsType<NotFoundResult>(item.Result);
        }

        [Fact]
        public void AddNew_WhenPost_ReturnBadRequest()
        {
            //Arrange
            var newItem = new ShoppingItem()
            {
                Id = 6,
                Price = 5,
                Manufacturer = "Sei la"
            };
            _controller.ModelState.AddModelError("Name", "Name property must be filled!");
            //Act
            var action = _controller.Post(newItem);

            //Assert
            Assert.IsType<BadRequestObjectResult>(action);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnCreatedAction()
        {
            var newItem = new ShoppingItem()
            {
                Name = "Carro",
                Price = 4000,
                Manufacturer = "VW"
            };

            var createAction = _controller.Post(newItem);

            Assert.IsType<CreatedAtActionResult>(createAction);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnCreatedActionItem()
        {
            var newItem = new ShoppingItem()
            {
                Name="Fox",
                Price=29.000M,
                Manufacturer="VW"
            };

            var action = _controller.Post(newItem) as CreatedAtActionResult;
            var item = action.Value as ShoppingItem;

            Assert.IsType<CreatedAtActionResult>(action);
            Assert.Equal("Fox", item.Name);
        }

        [Fact]
        public void Remove_NotExistingObjectPassed_ReturnsNotFoundResponse()
        {
            var id = 99;

            var result = _controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Remove_ExistingObjectPassed_ReturnOkResponse()
        {
            var action = _controller.Delete(1);
            Assert.Equal(2, (_service.GetAll() as List<ShoppingItem>).Count);
        }
    }
}
