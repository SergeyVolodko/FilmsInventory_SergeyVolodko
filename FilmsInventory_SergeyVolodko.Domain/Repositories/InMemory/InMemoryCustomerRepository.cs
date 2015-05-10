using System.Collections.Generic;
using System.Linq;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;

namespace FilmsInventory.Repositories
{
    public class InMemoryCustomerRepository: ICustomerRepository
    {
        private List<Customer> customers;

        public InMemoryCustomerRepository()
        {
            this.customers = new List<Customer>();
        }

        public void Save(Customer customer)
        {
            if (customer == null)
            {
                throw new NullCannotBeSavedException();
            }

            var existingCustomer = Load(customer.Name);

            if (existingCustomer == null)
            {
                this.customers.Add(customer);
            }
            else
            {
                var index = this.customers.IndexOf(existingCustomer);
                this.customers[index] = customer;
            }
        }
        
        public Customer Load(string name)
        {
            var customer = this.customers.FirstOrDefault(c => c.Name == name);
            return customer;
        }
    }
}
