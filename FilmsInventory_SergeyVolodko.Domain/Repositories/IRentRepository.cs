using System.Collections.Generic;
using FilmsInventory.Entities;

namespace FilmsInventory.Repositories
{
    public interface IRentRepository
    {
        void Save(Rent rent);
        Rent Load(int id);
        List<Rent> LoadAll();
        List<Rent> LoadByCustomerName(string customerName);
    }
}
