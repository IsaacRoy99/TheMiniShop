using Microsoft.AspNetCore.Mvc.RazorPages;
using TheMiniShop.Models;
using System.Text.Json;

namespace TheMiniShop.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        public Order? Order { get; set; }

        public void OnGet()
        {
            if (TempData.TryGetValue("OrderJson", out var o))
            {
                var json = o as string;
                if (!string.IsNullOrEmpty(json))
                {
                    Order = JsonSerializer.Deserialize<Order>(json);
                }
            }
        }
    }
}
