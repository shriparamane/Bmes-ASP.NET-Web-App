using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bmes.Models.Cart;
using Bmes.ViewModels.Cart;

namespace Bmes.Services
{
    public interface ICartService
    {
        string UniqueCartId();
        Cart GetCart();
        void AddToCart(AddToCartViewModel addToCartViewModel);
        void RemoveFromCart(RemoveFromCartViewModel removeFromCartViewModel);
        IEnumerable<CartItem> GetCartItems();
        int CartItemsCount();
        decimal GetCartTotal();
    }
}
