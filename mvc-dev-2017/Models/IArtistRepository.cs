using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc_dev_2017.Models
{
    public interface IArtistRepository
    {
        IQueryable<Artist> Artists { get; }
        Artist Save(Artist artist);
        void Delete(Artist artist);
    }
}
