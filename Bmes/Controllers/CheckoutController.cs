using Bmes.Services;
using Bmes.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace Bmes.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly ICartService _cartService;
        public CheckoutController(ICheckoutService checkoutService, ICartService cartService)
        {
            _checkoutService = checkoutService;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            _checkoutService.ProcessCheckout(checkoutViewModel);

            return RedirectToAction("Receipt");
        }

        public IActionResult Receipt()
        {
            return View();
        }
    }
}