﻿using System.Collections.Generic;
using Bmes.Database;
using Bmes.Models.Order;

namespace Bmes.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private BmesDbContext _context;

        public OrderRepository(BmesDbContext context)
        {
            _context = context;
        }

        public Order FindOrderById(long id)
        {
            var order = _context.Orders.Find(id);
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _context.Orders;
            return orders;
        }

        public void SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
