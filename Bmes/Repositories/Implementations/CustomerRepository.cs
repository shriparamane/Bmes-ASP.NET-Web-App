﻿using System.Collections.Generic;
using Bmes.Database;
using Bmes.Models.Customer;

namespace Bmes.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private BmesDbContext _context;

        public CustomerRepository(BmesDbContext context)
        {
            _context = context;
        }

        public Customer FindCustomerById(long id)
        {
            var note = _context.Customers.Find(id);
            return note;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var notes = _context.Customers;
            return notes;
        }

        public void SaveCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
