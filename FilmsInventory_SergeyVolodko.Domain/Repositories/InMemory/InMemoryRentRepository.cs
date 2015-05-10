using System.Collections.Generic;
using System.Linq;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;

namespace FilmsInventory.Repositories
{
    public class InMemoryRentRepository : IRentRepository
    {
        private readonly List<Rent> rents;

        public InMemoryRentRepository()
        {
            this.rents = new List<Rent>();
        }

        public void Save(Rent rent)
        {
            if (rent == null)
            {
                throw new NullCannotBeSavedException();
            }

            var existingRent = Load(rent.Id);

            if (existingRent == null)
            {
                this.rents.Add(rent);
                rent.Id = this.rents.Count;
            }
            else
            {
                var index = this.rents.IndexOf(existingRent);
                this.rents[index] = rent;
            }
        }

        public Rent Load(int id)
        {
            return this.rents.FirstOrDefault(r => r.Id == id);
        }

        public List<Rent> LoadAll()
        {
            return this.rents;
        }

        public List<Rent> LoadByCustomerName(string customerName)
        {
            var customerRents = this.rents.Where(r => r.CustomerName == customerName);
            return customerRents.ToList();
        }
    }
}
