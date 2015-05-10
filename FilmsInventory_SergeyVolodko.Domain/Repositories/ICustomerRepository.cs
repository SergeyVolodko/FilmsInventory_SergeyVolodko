using FilmsInventory.Entities;

namespace FilmsInventory.Repositories
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
        Customer Load(string name);
    }
}
