using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoolTweets.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KoolTweets.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void GetAllTweets_StartLessThanEnd_ReturnTrue()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            var result = controller.GetAllTweets(new DateTime(2016,01,01,00,00,00), new DateTime(2017, 01, 01, 00, 00, 00));
            dynamic data = result.Data;
            var success = data.success;
            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GetAllTweets_StartEqualToEnd_ReturnFalse()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            var result = controller.GetAllTweets(new DateTime(2016, 01, 01, 00, 00, 00), new DateTime(2016, 01, 01, 00, 00, 00));
            dynamic data = result.Data;
            var success = data.success;
            //Assert
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetAllTweets_StartGreaterThanEnd_ReturnFalse()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            var result = controller.GetAllTweets(new DateTime(2017, 01, 01, 00, 00, 00), new DateTime(2016, 01, 01, 00, 00, 00));
            dynamic data = result.Data;
            var success = data.success;
            //Assert
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetAllTweets_DateInFuture_ReturnFalse()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            var result = controller.GetAllTweets(new DateTime(2029, 01, 01, 00, 00, 00), new DateTime(2020, 01, 01, 00, 00, 00));
            dynamic data = result.Data;
            var success = data.success;
            //Assert
            Assert.IsFalse(success);
        }
    }
}