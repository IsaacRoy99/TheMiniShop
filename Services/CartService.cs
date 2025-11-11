using System.Text.Json;
using TheMiniShop.Models;

namespace TheMiniShop.Services
{
    public class CartService
    {
        private const string SessionKey = "TheMiniShop.Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor, ProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private List<CartItem> GetCartInternal()
        {
            var json = Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCartInternal(List<CartItem> items)
        {
            Session.SetString(SessionKey, JsonSerializer.Serialize(items));
        }

        public List<CartItem> GetCart() => GetCartInternal();

        public void AddToCart(int productId, int qty = 1)
        {
            var product = _productService.GetById(productId);
            if (product == null) return;

            var items = GetCartInternal();
            var existing = items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = qty,
                    ImageUrl = product.ImageUrl
                });
            }
            SaveCartInternal(items);
        }

        public void UpdateQuantity(int productId, int qty)
        {
            var items = GetCartInternal();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (qty <= 0) items.Remove(item);
                else item.Quantity = qty;
                SaveCartInternal(items);
            }
        }

        public void Remove(int productId)
        {
            var items = GetCartInternal();
            items.RemoveAll(i => i.ProductId == productId);
            SaveCartInternal(items);
        }

        public void Clear() => SaveCartInternal(new List<CartItem>());
    }
}
