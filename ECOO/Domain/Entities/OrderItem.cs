namespace ECOO.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Foreign key to Order
        public int OrderId { get; set; }

        // Navigation property: each item belongs to one order
        public Order? Order { get; set; }

        // Foreign key to Product
        public int ProductId { get; set; }

        // Navigation property: each item refers to one product
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        // Price captured at the time of purchase (snapshot)
        public decimal Price { get; set; }
    }
}
