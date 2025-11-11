using System.Text.Json;
using TheMiniShop.Models;

namespace TheMiniShop.Services
{
    public class ProductService
    {
        private readonly IWebHostEnvironment _env;
        private List<Product>? _cache;

        public ProductService(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string DataFilePath => Path.Combine(_env.ContentRootPath, "Data", "products.json");

        public List<Product> GetProducts()
        {
            if (_cache != null)
                return _cache;

            try
            {
                if (!File.Exists(DataFilePath))
                {
                    Console.WriteLine($"Product data file not found: {DataFilePath}");
                    _cache = new List<Product>();
                    return _cache;
                }

                var json = File.ReadAllText(DataFilePath);

                _cache = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading products: {ex.Message}");
                _cache = new List<Product>();
            }

            return _cache;
        }

        public Product? GetById(int id) => GetProducts().FirstOrDefault(p => p.Id == id);
    }
}
