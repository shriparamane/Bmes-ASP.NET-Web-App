using System.Collections.Generic;
using Bmes.Models.Customer;

namespace Bmes.Repositories
{
    public interface ICustomerRepository
    {
        Customer FindCustomerById(long id);
        IEnumerable<Customer> GetAllCustomers();
        void SaveCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
