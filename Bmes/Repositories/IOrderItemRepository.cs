using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bmes.Models.Order;

namespace Bmes.Repositories
{
    public interface IOrderItemRepository
    {
        OrderItem FindOrderItemById(long id);
        IEnumerable<OrderItem> FindOrderItemByOrderId(long orderId);
        IEnumerable<OrderItem> GetAllOrderItems();
        void SaveOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);
    }
}
