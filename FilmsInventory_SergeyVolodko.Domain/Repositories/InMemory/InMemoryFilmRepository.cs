using System.Collections.Generic;
using System.Linq;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;

namespace FilmsInventory.Repositories
{
    public class InMemoryFilmRepository : IFilmRepository
    {
        private readonly List<Film> films;

        public InMemoryFilmRepository()
        {
            this.films = new List<Film>();
        }

        public void Save(Film film)
        {
            if (film == null)
            {
                throw new NullCannotBeSavedException();
            }

            var existingFilm = Load(film.Name);

            if (existingFilm == null)
            {
                this.films.Add(film);
            }
            else
            {
                var index = this.films.IndexOf(existingFilm);
                this.films[index] = film;
            }
        }

        public ICollection<Film> LoadAll()
        {
            return this.films;
        }

        public Film Load(string name)
        {
            var film = this.films.FirstOrDefault(f => f.Name == name);
            return film;
        }

        public void Remove(string name)
        {
            var film = this.films.FirstOrDefault(f => f.Name == name);
            if (film == null)
            {
                throw new FilmWithSpecifiedNameDoesNotExistException();
            }

            this.films.Remove(film);
        }
    }
}
