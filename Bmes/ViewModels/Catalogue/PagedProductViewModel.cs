using System.Collections.Generic;
using Bmes.Models.Product;

namespace Bmes.ViewModels.Catalogue
{
    public class PagedProductViewModel
    {
        public PaginationViewModel PagedProducts { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
