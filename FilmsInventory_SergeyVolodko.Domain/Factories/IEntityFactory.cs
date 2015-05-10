using System;
using FilmsInventory.Entities;

namespace FilmsInventory.Factories
{
    public interface IEntityFactory
    {
        Customer CreateCustomer(string name);
        Film CreateFilm(string name, FilmType type);
        Rent CreateRent(string filmName, string customerName, DateTime startDate, int daysCount);
        Payment CreatePayment(int cost, Currency currency);
    }
}
