using System.Collections.Generic;
using FilmsInventory.Entities;

namespace FilmsInventory.Repositories
{
    public interface IFilmRepository
    {
        void Save(Film film);
        ICollection<Film> LoadAll();
        Film Load(string name);
        void Remove(string name);
    }
}
