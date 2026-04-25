namespace ECOO.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        // Shipping / customer info stored on the order
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        // Navigation property: one order contains many order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
