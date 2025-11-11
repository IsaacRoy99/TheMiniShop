using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using TheMiniShop.Services;

namespace TheMiniShop.Pages
{
    public class CartModel : PageModel
    {
        private readonly CartService _cartService;

        public CartModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public List<CartItem> Items { get; set; } = new();

        public void OnGet()
        {
            Items = _cartService.GetCart();
        }

        public IActionResult OnPostUpdate(int productId, int qty)
        {
            _cartService.UpdateQuantity(productId, qty);
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int productId)
        {
            _cartService.Remove(productId);
            return RedirectToPage();
        }

        public IActionResult OnPostCheckout()
        {
            return RedirectToPage("/Checkout");
        }
    }
}
