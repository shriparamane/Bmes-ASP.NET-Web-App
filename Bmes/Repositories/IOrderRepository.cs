using System.Collections.Generic;
using Bmes.Models.Order;

namespace Bmes.Repositories
{
    public interface IOrderRepository
    {
        Order FindOrderById(long id);
        IEnumerable<Order> GetAllOrders();
        void SaveOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
