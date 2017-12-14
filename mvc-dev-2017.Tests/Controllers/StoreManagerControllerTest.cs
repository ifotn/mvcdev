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
        StoreManagerController controller;
        Mock<IStoreManagerRepository> mock;
        Mock<IArtistRepository> artistMock;
        Mock<IGenreRepository> genreMock;
        List<Album> albums;

        [TestMethod]
        public void IndexFails()
        {
            //Mock<IStoreManagerRepository> mock = new Mock<IStoreManagerRepository>();
            //StoreManagerController controller = new StoreManagerController(mock.Object);

            //ViewResult result = controller.Index() as ViewResult;
            //Assert.IsNotNull(result);

            var actual = (List<Album>)controller.Index().Model;
            CollectionAssert.AreNotEqual(albums, actual);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // instantiate mock
            mock = new Mock<IStoreManagerRepository>();
            artistMock = new Mock<IArtistRepository>();
            genreMock = new Mock<IGenreRepository>();

            // mock data
            albums = new List<Album>
            {
                new Album { AlbumId = 1, Title = "Album 1", Price = 9 },
                new Album { AlbumId = 2, Title = "Album 2", Price = 10 },
                new Album { AlbumId = 3, Title = "Album 3", Price = 8 }
            };

            // populate repo from mock data
            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());

            // pass repo with data to controller
            controller = new StoreManagerController(mock.Object);
        }

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    mock.VerifyAll();
        //}

        [TestMethod]
        public void IndexPass()
        {
            // act
            var actual = (List<Album>)controller.Index().Model;

            // assert
            CollectionAssert.AreEqual(albums, actual);
        }

        [TestMethod]
        public void DetailsValidId()
        {
            // act
            var actual = (Album)controller.Details(1).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsValidInvalidId()
        {
            // act
            ViewResult actual = (ViewResult)controller.Details(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = (ViewResult)controller.Details(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void CreateAlbumAdded()
        {
            // arrange
            var album = new Album()
            {
                AlbumId = 4,
                Title = "Album 4",
                Price = 44
            };

            // act
            //var actual = (RedirectToRouteResult)controller.Create(album);

            //// assert
            //Assert.AreEqual("Index", actual.RouteValues["action"]);
            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(album);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);

        }

        [TestMethod]
        public void CreateInvalidAlbum()
        {
            // arrange
            Album album = null;

            // act
            var actual = (ViewResult)controller.Create(album);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteLoadPass()
        {
            // act
            ViewResult actual = controller.Delete(1) as ViewResult;

            // assert
            Assert.AreEqual("Delete", actual.ViewName);
        }

        [TestMethod]
        public void DeleteLoadInvalidAlbum()
        {
            // act
            ViewResult actual = controller.Delete(4444) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteLoadNoAlbum()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = controller.Delete(id) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeletePass()
        {
            // arrange
            //var album = new Album()
            //{
            //    AlbumId = 4,
            //    Title = "Album 4",
            //    Price = 44
            //};

            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.DeleteConfirmed(3);
            //ViewResult actual = controller.DeleteConfirmed(album.AlbumId);
            //var actual = (RedirectToRouteResult)controller.DeleteConfirmed(albums[0].AlbumId);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
            //Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidAlbum()
        {
            // arrange
            var album = new Album()
            {
                AlbumId = 44444,
                Title = "Album 4",
                Price = 44
            };

            // act
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(album.AlbumId);

            // assert
            Assert.AreEqual("Error", actual.ViewName);

            //// act
            //var actual = (RedirectToRouteResult)controller.DeleteConfirmed(album.AlbumId);

            //// assert
            //Assert.AreEqual("Error", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteNoAlbum()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditLoadPass()
        {
            // act
            ViewResult actual = controller.Edit(1) as ViewResult;

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditValidInvalidId()
        {
            // act
            ViewResult actual = (ViewResult)controller.Edit(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = (ViewResult)controller.Edit(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditSavePass()
        {
            // arrange
            var album = new Album()
            {
                AlbumId = 4,
                Title = "Album 4",
                Price = 44
            };

            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(album);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);

            //// act
            //var actual = (RedirectToRouteResult)controller.Edit(album);

            //// assert
            //Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditSaveEmptyAlbum()
        {
            // arrange
            Album album = null;

            // act
            ViewResult actual = (ViewResult)controller.Edit(album);

            // assert
            Assert.AreEqual("Error", actual.ViewName);

            //// act
            //var actual = (RedirectToRouteResult)controller.Edit(album);

            //// assert
            //Assert.AreEqual("Error", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditInvalidModel()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error message");

            Album album = new Album
            {
                AlbumId = 3,
                Title = "Album 3",
                Price = 8
            };

            // act
            ViewResult actual = (ViewResult)controller.Edit(album);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
            Assert.IsNotNull(actual.ViewBag.ArtistId);
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        


        [TestMethod]
        public void CreateFormLoadPass()
        {
            // act
            ViewResult actual = controller.Create() as ViewResult;

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        [TestMethod]
        public void CreateFormLoadFail()
        {
            // act
            ViewResult actual = controller.Create() as ViewResult;

            // assert
            Assert.AreNotEqual("Error", actual.ViewName);

        }

        [TestMethod]
        public void CreateInvalidModel()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error message");

            Album album = new Album
            {
                AlbumId = 4, Title = "Album 4", Price = 8
            };

            // act
            ViewResult actual = (ViewResult)controller.Create(album);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
            Assert.IsNotNull(actual.ViewBag.ArtistId);
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

    }
}

