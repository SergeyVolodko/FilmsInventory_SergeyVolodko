using System;
using FilmsInventory.Entities;

namespace FilmsInventory.Factories
{
    public class EntityFactory : IEntityFactory
    {
        public Customer CreateCustomer(string name)
        {
            return new Customer(name);
        }

        public Film CreateFilm(string name, FilmType type)
        {
            return new Film(name, type);
        }

        public Rent CreateRent(string filmName, string customerName, DateTime startDate, int daysCount)
        {
            return new Rent(customerName, filmName, startDate, daysCount);
        }

        public Payment CreatePayment(int cost, Currency currency)
        {
            return new Payment(cost, currency);
        }
    }
}
