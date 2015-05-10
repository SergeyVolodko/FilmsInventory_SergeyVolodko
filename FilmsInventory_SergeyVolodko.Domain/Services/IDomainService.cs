using System.Collections.Generic;
using FilmsInventory.Entities;

namespace FilmsInventory.Services
{
    public interface IDomainService
    {
        Film CreateFilm(string name, FilmType type);
        Film GetFilmByName(string name);
        List<Film> GetAllFilms();
        void RemoveFilm(string name);
        Film ChangeFilmType(string name, FilmType type);
        Customer CreateCustomer(string name);
        Customer GetCustomer(string name);
        Customer GainBonusPoints(string customerName, FilmType filmType);
        bool CanCustomerPayByBonuses(string customerName, FilmType filmType, int rentDaysCount);
    }
}
