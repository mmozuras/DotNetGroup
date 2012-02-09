﻿namespace Tests.Web.Mobile
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Moq;

    using NUnit.Framework;

    using global::Services.Model;

    using Tests.Helpers;

    using global::Web.Mobile.Controllers;

    using global::Web.Mobile.Models.ViewModels;

    using global::Web.Mobile.Services;

    [TestFixture]
    public class HomeControllerTests
    {
        [SetUp]
        public void Init()
        {
            MappingService.Init();
        }

        [Test]
        public void Index_Action_Should_Return_ViewResult_With_ItemCompactView_List()
        {
            var streamServiceFake = new Mock<IStreamService>();
            var controller = new HomeController(streamServiceFake.Object);

            var returnsViewResult = controller.Index(null).ReturnsViewResult();

            Assert.IsTrue(returnsViewResult.Model is IEnumerable<ItemCompactView>);             
        }

        [Test]
        public void Item_Action_Should_Return_ViewResult_With_ItemView()
        {
            var id = "000000000000000000000000";
            var streamServiceFake = new Mock<IStreamService>();
            streamServiceFake.Setup(x => x.GetItem(id)).Returns(new Item());

            var controller = new HomeController(streamServiceFake.Object);

            var returnsViewResult = controller.Item(id).ReturnsViewResult();

            Assert.IsTrue(returnsViewResult.Model is ItemView);
        }
        
        [Test]
        public void Given_Not_Existing_Id_Item_Action_Should_Return_HttpNotFoundResult()
        {
            var id = "000000000000000000000000";
            var streamServiceFake = new Mock<IStreamService>();
            streamServiceFake.Setup(x => x.GetItem(id)).Returns((Item)null);
            var controller = new HomeController(streamServiceFake.Object);

            var viewResult = controller.Item(id);

            Assert.IsTrue(viewResult is HttpNotFoundResult);
        }

        [Test]
        public void Given_No_Id_Item_Action_Should_Return_HttpNotFoundResult()
        {
            string id = null;
            var streamServiceFake = new Mock<IStreamService>();
            streamServiceFake.Setup(x => x.GetItem(id)).Returns((Item)null);
            var controller = new HomeController(streamServiceFake.Object);

            var viewResult = controller.Item(id);

            Assert.IsTrue(viewResult is HttpNotFoundResult);
        }
    }
}