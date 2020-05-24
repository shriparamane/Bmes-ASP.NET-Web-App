using Bmes.ViewModels.Catalogue;

namespace Bmes.Services
{
    public interface ICatalogueService
    {
        PagedProductViewModel FetchProducts(string categorySlug, string brandSlug);
    }
}
