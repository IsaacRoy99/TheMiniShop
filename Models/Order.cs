namespace TheMiniShop.Models
{
    public class Order
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.Total);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CustomerName { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
    }
}
