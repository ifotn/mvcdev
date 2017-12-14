using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvc_dev_2017;
using mvc_dev_2017.Controllers;
using System.Web.Mvc;
using mvc_dev_2017.Models;
using Moq;

namespace mvc_dev_2017.Tests.Controllers
{
    [TestClass]
    public class StoreManagerControllerTest
    {
        //StoreManagerController controller;
        //Mock<IStoreManagerRepository> mock;
        //List<Album> albums;

        [TestMethod]
        public void IndexFails()
        {
            Mock<IStoreManagerRepository> mock = new Mock<IStoreManagerRepository>();
            StoreManagerController controller = new StoreManagerController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    controller = new StoreManagerController();
        //    albums = new List<Album>
        //    {
        //        new Album { AlbumId = 1, Title = "Album 1", Price = 9 },
        //        new Album { AlbumId = 2, Title = "Album 2", Price = 10 },
        //        new Album { AlbumId = 2, Title = "Album 3", Price = 8 }
        //    };
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    mock.VerifyAll();
        //}

        [TestMethod]
        public void IndexPass()
        {
            // Arrange
            Mock <IStoreManagerRepository> mock = new Mock<IStoreManagerRepository>();
            Mock<IArtistRepository> artistMock = new Mock<IArtistRepository>();
            Mock<IGenreRepository> genreMock = new Mock<IGenreRepository>();

            List<Album> albums = new List<Album>
            {
                new Album { AlbumId = 1, Title = "Album 1", Price = 9 },
                new Album { AlbumId = 2, Title = "Album 2", Price = 10 },
                new Album { AlbumId = 2, Title = "Album 3", Price = 8 }
            };

            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());

            // move this to setup so mock data can be used by all tests
            //mock.Setup(m => m.Albums).Returns(new Album[]
            //{
            //    new Album { AlbumId = 1, Title = "Album 1", Price = 9 },
            //    new Album { AlbumId = 2, Title = "Album 2", Price = 10 },
            //    new Album { AlbumId = 2, Title = "Album 3", Price = 8 }
            //}.AsQueryable());

            StoreManagerController controller = new StoreManagerController(mock.Object);

            // Act
            var actual = (List<Album>)controller.Index().Model;

            // Assert
       
            CollectionAssert.AreEqual(albums, actual);

            //Assert.IsInstanceOfType(actual, List<Album>);
        }

        [TestMethod]
        public void DetailsPass()
        {
            // Arrange
            Mock<IStoreManagerRepository> mock = new Mock<IStoreManagerRepository>();
            Mock<IArtistRepository> artistMock = new Mock<IArtistRepository>();
            Mock<IGenreRepository> genreMock = new Mock<IGenreRepository>();

            Album album = new Album
            {
                 AlbumId = 1, Title = "Album 1", Price = 9 
            };

            mock.Setup(m => m.Get(1)).Returns(album);

            // move this to setup so mock data can be used by all tests
            //mock.Setup(m => m.Albums).Returns(new Album[]
            //{
            //    new Album { AlbumId = 1, Title = "Album 1", Price = 9 },
            //    new Album { AlbumId = 2, Title = "Album 2", Price = 10 },
            //    new Album { AlbumId = 2, Title = "Album 3", Price = 8 }
            //}.AsQueryable());

            StoreManagerController controller = new StoreManagerController(mock.Object);

            // Act
            Album actual = (Album)controller.Details(1).Model;

            // Assert
            Assert.AreEqual(album, actual);
        }
    }
}
