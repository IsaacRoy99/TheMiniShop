using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using TheMiniShop.Services;

namespace TheMiniShop.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public ProductModel(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Product? Product { get; set; }

        public void OnGet()
        {
            Product = _productService.GetById(Id);
        }

        public IActionResult OnPostAddToCart(int id, int qty = 1)
        {
            _cartService.AddToCart(id, qty);
            return RedirectToPage("/Cart");
        }
    }
}
