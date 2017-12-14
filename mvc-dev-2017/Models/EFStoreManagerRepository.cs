using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace mvc_dev_2017.Models
{
    public class EFStoreManagerRepository : IStoreManagerRepository
    {
        MusicStoreModel context = new MusicStoreModel();


        public IQueryable<Album> Albums { get { return context.Albums; } }
        public IQueryable<Artist> Artists {  get { return context.Artists; } }
        public IQueryable<Genre> Genres { get { return context.Genres; } }

        public void Delete(Album album)
        {
            context.Albums.Remove(album);
            context.SaveChanges();
        }

        public Album Get(int id)
        {
            return context.Albums.FirstOrDefault(a => a.AlbumId == id);
        }

        public Album Save(Album album)
        {
            if (album.AlbumId == 0)
            {
                context.Albums.Add(album);
            }
            else
            {
                context.Entry(album).State = EntityState.Modified;
            }

            context.SaveChanges();
            return album;
        }
    }
}