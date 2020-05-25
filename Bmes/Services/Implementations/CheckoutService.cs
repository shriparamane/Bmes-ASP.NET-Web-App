using Bmes.Models.Address;
using Bmes.Models.Customer;
using Bmes.Models.Order;
using Bmes.Models.Shared;
using Bmes.Repositories;
using Bmes.ViewModels.Checkout;

namespace Bmes.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private ICustomerRepository _customerRepository;
        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private ICartRepository _cartRepository;
        private ICartItemRepository _cartItemRepository;
        private ICartService _cartService;
        public CheckoutService(ICustomerRepository customerRepository,
            IPersonRepository personRepository,
            IAddressRepository addressRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            ICartService cartService)
        {
            _customerRepository = customerRepository;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
        }

        public void ProcessCheckout(CheckoutViewModel checkoutViewModel)
        {
            var person = new Person
            {
                FirstName = checkoutViewModel.FirstName,
                MiddleName = checkoutViewModel.MiddleName,
                LastName = checkoutViewModel.LastName,
                EmailAddress = checkoutViewModel.EmailAddress,
                IsDeleted = false
            };

            _personRepository.SavePerson(person);

            var address = new Address
            {
                AddressLine1 = checkoutViewModel.AddressLine1,
                AddressLine2 = checkoutViewModel.AddressLine2,
                City = checkoutViewModel.City,
                State = checkoutViewModel.State,
                Country = checkoutViewModel.Country,
                ZipCode = checkoutViewModel.ZipCode,
                IsDeleted = false
            };

            _addressRepository.SaveAddress(address);

            var customer = new Customer
            {
                PersonId = person.Id,
                Person = person,
                IsDeleted = false
            };

            _customerRepository.SaveCustomer(customer);

            var cart = _cartService.GetCart();

            if (cart != null)
            {
                var cartItems = _cartItemRepository.FindCartItemsByCartId(cart.Id);
                var cartTotal = _cartService.GetCartTotal();
                decimal shippingCharge = 0;
                var orderTotal = cartTotal + shippingCharge;

                var order = new Order
                {
                    OrderTotal = orderTotal,
                    OrderItemTotal = cartTotal,
                    ShippingCharge = shippingCharge,
                    AddressId = address.Id,
                    DeliveryAddress = address,
                    CustomerId = customer.Id,
                    Customer = customer,
                    OrderStatus = OrderStatus.Submitted
                };

                _orderRepository.SaveOrder(order);

                foreach (var cartItem in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = cartItem.Quantity,
                        Order = order,
                        OrderId = order.Id,
                        Product = cartItem.Product,
                        ProductId = cartItem.ProductId
                    };

                    _orderItemRepository.SaveOrderItem(orderItem);
                }

                _cartRepository.DeleteCart(cart);
            }
        }
    }
}
