using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_dev_2017.Models
{
    public interface IStoreManagerRepository
    {
        IQueryable<Album> Albums { get; }
        IQueryable<Artist> Artists { get;  }
        IQueryable<Genre> Genres { get; }

        Album Get(int id);
        Album Save(Album album);
        void Delete(Album album);
    }
}