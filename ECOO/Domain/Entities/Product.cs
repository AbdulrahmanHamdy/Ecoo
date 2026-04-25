namespace ECOO.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        // Foreign key to Category
        public int CategoryId { get; set; }

        // Navigation property: each product belongs to one category
        public Category? Category { get; set; }

        // Navigation property: one product can appear in many order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
