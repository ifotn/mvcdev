using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc_dev_2017.Models
{
    public interface IGenreRepository
    {
        IQueryable<Genre> Genres { get; }
        Genre Save(Genre genre);
        void Delete(Genre genre);
    }
}
