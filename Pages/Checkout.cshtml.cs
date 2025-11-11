using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using TheMiniShop.Services;

namespace TheMiniShop.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly CartService _cartService;

        public CheckoutModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [BindProperty]
        public string CustomerName { get; set; } = "";

        [BindProperty]
        public string CustomerEmail { get; set; } = "";

        public List<CartItem> Items { get; set; } = new();

        public void OnGet()
        {
            Items = _cartService.GetCart();
        }

        public IActionResult OnPost()
        {
            Items = _cartService.GetCart();
            if (!Items.Any()) return RedirectToPage("/Cart");

            var order = new Order
            {
                Items = Items,
                CustomerName = CustomerName,
                CustomerEmail = CustomerEmail
            };

            // For this simple demo we'll just clear the cart and pass the order to confirmation
            _cartService.Clear();

            TempData["OrderJson"] = System.Text.Json.JsonSerializer.Serialize(order);
            return RedirectToPage("/OrderConfirmation");
        }
    }
}

