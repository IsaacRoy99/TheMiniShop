using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using TheMiniShop.Services;

namespace TheMiniShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public IndexModel(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public List<Product> Products { get; set; } = new();

        public void OnGet()
        {
            Products = _productService.GetProducts();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            _cartService.AddToCart(id, 1);
            return RedirectToPage("/Cart");
        }
    }
}
