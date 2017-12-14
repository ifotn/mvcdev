using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc_dev_2017.Models;
using PagedList;

namespace mvc_dev_2017.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        //private MusicStoreModel db = new MusicStoreModel();
        private IStoreManagerRepository smRepo;

        public StoreManagerController()
        {
            this.smRepo = new EFStoreManagerRepository();
        }

        public StoreManagerController(IStoreManagerRepository smRepo)
        {
            this.smRepo = smRepo;
        }

        // GET: StoreManager
        public ViewResult Index()
        {
            var albums = smRepo.Albums.Include(a => a.Artist).Include(a => a.Genre);

            // Sort lists first
            var artists = smRepo.Artists.ToList().OrderBy(a => a.Name);
            var genres = smRepo.Genres.ToList().OrderBy(g => g.Name);


            //Artist ar = new Artist();
            //ar.ArtistId = -1;
            //ar.Name = "-Select-";

            //List<Artist> aOptions = new List<Artist>();
            //aOptions.Add(ar);

            //List<Artist> aFinal = new List<Artist>();
            //aFinal.Concat(aOptions);
            //aFinal.Concat(artists);

            ////SelectList al = new SelectList(artists, "ArtistId", "Name");

            //ViewBag.ArtistId = new SelectList(aFinal, "ArtistId", "Name");
            //ViewBag.GenreId = new SelectList(genres, "GenreId", "Name");

            ViewBag.AlbumCount = albums.Count().ToString();

            return View(albums.OrderBy(a => a.Artist.Name).ThenBy(b => b.Title).ToList());
        }

        [AllowAnonymous]
        // GET: StoreManager/Details/5
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // pre interface code
            //Album album = db.Albums.Find(id);

            Album album = smRepo.Albums.FirstOrDefault(a => a.AlbumId == id);
            if (album == null)
            {
                return View("Error"); // HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            // Sort lists first
            var artists = smRepo.Artists.ToList().OrderBy(a => a.Name);
            var genres = smRepo.Genres.ToList().OrderBy(g => g.Name);

            ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name");

            return View("Create");
        }

        //// POST: StoreManager/Create - Original
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Albums.Add(album);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
        //    return View(album);
        //}
        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (album  == null)
                {
                    return View("Error");
                }
                //bool redirect = true;

                //if (album.AlbumId > 0)
                //{
                //    redirect = false;
                //}
                smRepo.Save(album);
                //db.SaveChanges();

                //if (redirect)
                //{
                    return RedirectToAction("Index");
                //}
                //else
                //{
                    //return View(album);
                //}        
            }

            ViewBag.ArtistId = new SelectList(smRepo.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(smRepo.Genres, "GenreId", "Name", album.GenreId);
            return View("Create", album);
        }

        // GET: StoreManager/Edit/5
        public ViewResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Album album = db.Albums.Find(id);
            //if (album == null)
            //{
            //    return HttpNotFound();
            //}

            //// Sort lists first
            //var artists = db.Artists.ToList().OrderBy(a => a.Name);
            //var genres = db.Genres.ToList().OrderBy(g => g.Name);

            if (id == null)
            {
                return View("Error");
            }

            Album album = smRepo.Albums.FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return View("Error");
            }

            // Sort lists first
            var artists = smRepo.Artists.ToList().OrderBy(a => a.Name);
            var genres = smRepo.Genres.ToList().OrderBy(g => g.Name);

            ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", album.GenreId);
            return View("Edit", album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (album == null)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                if (Request != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        // if a new cover image has been uploaded
                        if (file.FileName != null && file.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Content/Images/") + file.FileName;
                            file.SaveAs(path);
                            album.AlbumArtUrl = "/Content/Images/" + file.FileName;
                        }
                    }
                }

                //db.Entry(album).State = EntityState.Modified;
                //db.SaveChanges();
                smRepo.Save(album);
                //return View("Index"); 
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(smRepo.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(smRepo.Genres, "GenreId", "Name", album.GenreId);
            return View("Edit", album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Album album = db.Albums.Find(id);
            //if (album == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(album);
            if (id == null)
            {
                return View("Error");
            }

            Album album = smRepo.Albums.FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return View("Error");
            }

            return View("Delete", album);

        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //Album album = db.Albums.Find(id);
            //db.Albums.Remove(album);
            //db.SaveChanges();

            if (id == null)
            {
                return View("Error");
            }

            Album album = smRepo.Albums.FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return View("Error");
            }

            smRepo.Delete(album);
            return RedirectToAction("Index");
        }


        //// POST: StoreManager/Index
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(String Title)
        //{
        //    //var query = from p in Pets select p;
        //    //if (OwnerID != null) query = query.Where(x => x.OwnerID == OwnerID);
        //    //if (AnotherID != null) query = query.Where(x => x.AnotherID == AnotherID);
        //    //if (TypeID != null) query = query.Where(x => x.TypeID == TypeID);

        //    var albums = from a in db.Albums select a;

        //    //if (GenreID > 0)
        //    //{
        //    //    albums = albums.Where(ma => ma.GenreId == GenreID);
        //    //}

        //    //if (ArtistID > 0)
        //    //{
        //    //    albums = albums.Where(ma => ma.ArtistId == ArtistID);
        //    //}

        //    if (Title != "")
        //    {
        //        albums = albums.Where(ma => ma.Title.Contains(Title));
        //    }

        //    ViewBag.AlbumCount = albums.Count().ToString();

        //    //// Sort lists first
        //    //var artists = db.Artists.ToList().OrderBy(a => a.Name);
        //    //var genres = db.Genres.ToList().OrderBy(g => g.Name);

        //    //ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name");
        //    //ViewBag.GenreId = new SelectList(genres, "GenreId", "Name");
        //    return View(albums);
        //}


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
