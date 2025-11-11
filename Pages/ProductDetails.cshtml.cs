using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using TheMiniShop.Services;

namespace TheMiniShop.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public ProductDetailsModel(ProductService productService, CartService cartService)
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

        public IActionResult OnPostAddToCart(int id)
        {
            _cartService.AddToCart(id, 1);
            return RedirectToPage("/Cart");
        }
    }
}
