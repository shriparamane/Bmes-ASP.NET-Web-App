using Bmes.ViewModels.Checkout;

namespace Bmes.Services
{
    public interface ICheckoutService
    {
        void ProcessCheckout(CheckoutViewModel checkoutViewModel);
    }
}
