using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bmes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bmes.Controllers
{
    public class CatalogueController : Controller
    {
        private ICatalogueService _catalogueService;
        private readonly ICartService _cartService;
        public CatalogueController(ICatalogueService catalogueService, ICartService cartService)
        {
            _catalogueService = catalogueService;
            _cartService = cartService;
        }

        public IActionResult Index(string category_slug = "all-categories", string brand_slug = "all-brands")
        {
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;

            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();


            var pagedProducts = _catalogueService.FetchProducts(category_slug, brand_slug);

            ViewData["Page"] = pagedProducts.PagedProducts.CurrentPage;

            return View(pagedProducts);
        }
    }
}