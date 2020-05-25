using System.Collections.Generic;
using Bmes.Models.Cart;

namespace Bmes.Repositories
{
    public interface ICartRepository
    {
        Cart FindCartById(long id);
        IEnumerable<Cart> GetAllCarts();
        void SaveCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
    }
}
