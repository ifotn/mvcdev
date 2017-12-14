using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvc_dev_2017;
using mvc_dev_2017.Controllers;

namespace mvc_dev_2017.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SumSucceeds()
        {
            HomeController controller = new HomeController();
            int x = 10;
            int y = 10;
            int expResult = 20;

            int result = controller.Sum(x, y);
            Assert.AreEqual(result, expResult);
        }

        [TestMethod]
        public void SumFail()
        {
            HomeController controller = new HomeController();
            int x = 10;
            int y = 20;
            int expResult = 30;

            int result = controller.Sum(x, y);
            Assert.AreEqual(result, expResult);
        }

        [TestMethod]
        public void GetWeatherColdPass()
        {
            HomeController controller = new HomeController();
            string expResult = "I need a coffee";
            int temp = -14;

            string result = controller.GetWeather(temp);
            Assert.AreEqual(result, expResult);
        }

        [TestMethod]
        public void GetWeatherColdFail()
        {
            HomeController controller = new HomeController();
            string expResult = "I need a coat";
            int temp = -14;

            string result = controller.GetWeather(temp);
            Assert.AreEqual(result, expResult);

        }

        [TestMethod]
        public void GetWeatherMildPass()
        {
            HomeController controller = new HomeController();
            string expResult = "I need a smoothie";
            int temp = 15;

            string result = controller.GetWeather(temp);
            Assert.AreEqual(result, expResult);
        }

        [TestMethod]
        public void GetWeatherHotPass()
        {
            HomeController controller = new HomeController();
            string expResult = "I need a margarita";
            int temp = 28;

            string result = controller.GetWeather(temp);
            Assert.AreEqual(result, expResult);
        }
    }
}
