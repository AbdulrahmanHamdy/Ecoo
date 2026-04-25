using ECOO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ECOO.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets — one per aggregate root / entity
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Category ───────────────────────────────────────────────
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            // ── Product ────────────────────────────────────────────────
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(2000);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.ImageUrl).HasMaxLength(500);

                // Product → Category (many-to-one)
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Order ──────────────────────────────────────────────────
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(200);
                entity.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(200);
                entity.Property(o => o.ShippingAddress).IsRequired().HasMaxLength(500);
            });

            // ── OrderItem ──────────────────────────────────────────────
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");

                // OrderItem → Order (many-to-one)
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // OrderItem → Product (many-to-one)
                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Seed Data ──────────────────────────────────────────────
            SeedData(modelBuilder);
        }

        /// <summary>Seeds categories and products so the app is usable immediately after migrations.</summary>
        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Reusable Goods" },
                new Category { Id = 2, Name = "Organic Food" },
                new Category { Id = 3, Name = "Eco Apparel" },
                new Category { Id = 4, Name = "Zero-Waste Kitchen" },
                new Category { Id = 5, Name = "Natural Beauty" }
            );

            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Bamboo Water Bottle", Description = "Durable, BPA-free bamboo & stainless steel bottle. Keeps drinks cold for 24 hrs.", Price = 24.99m, ImageUrl = "/images/products/bamboo-bottle.jpg", CategoryId = 1 },
                new Product { Id = 2, Name = "Beeswax Food Wraps (3-pack)", Description = "Replace single-use plastic wrap. Reusable, washable, compostable.", Price = 14.99m, ImageUrl = "/images/products/beeswax-wraps.jpg", CategoryId = 1 },
                new Product { Id = 3, Name = "Organic Matcha Powder", Description = "Ceremonial-grade matcha from shade-grown Japanese tea leaves. 100g.", Price = 19.99m, ImageUrl = "/images/products/matcha.jpg", CategoryId = 2 },
                new Product { Id = 4, Name = "Cold-Pressed Olive Oil", Description = "Extra-virgin, single-origin olive oil in a recyclable glass bottle. 500ml.", Price = 12.49m, ImageUrl = "/images/products/olive-oil.jpg", CategoryId = 2 },
                new Product { Id = 5, Name = "Hemp Tote Bag", Description = "Sturdy, naturally dyed hemp bag. Holds up to 20 kg. Certified organic.", Price = 18.00m, ImageUrl = "/images/products/hemp-tote.jpg", CategoryId = 3 },
                new Product { Id = 6, Name = "Recycled Cotton T-Shirt", Description = "Soft tee made from 100% post-consumer recycled cotton. Available S–XL.", Price = 29.99m, ImageUrl = "/images/products/recycled-tshirt.jpg", CategoryId = 3 },
                new Product { Id = 7, Name = "Compostable Dish Brush", Description = "Bamboo handle + plant-fibre bristles. Fully compostable after use.", Price = 9.99m, ImageUrl = "/images/products/dish-brush.jpg", CategoryId = 4 },
                new Product { Id = 8, Name = "Stainless Steel Straw Set", Description = "Set of 4 reusable straws + cleaning brush in a cotton pouch.", Price = 11.99m, ImageUrl = "/images/products/steel-straws.jpg", CategoryId = 4 },
                new Product { Id = 9, Name = "Rosehip Face Oil", Description = "100% organic cold-pressed rosehip oil. Vitamin-C rich. 30ml amber glass bottle.", Price = 22.00m, ImageUrl = "/images/products/rosehip-oil.jpg", CategoryId = 5 },
                new Product { Id = 10, Name = "Solid Shampoo Bar", Description = "Palm-oil-free, vegan shampoo bar. Replaces 3 plastic bottles. All hair types.", Price = 13.50m, ImageUrl = "/images/products/shampoo-bar.jpg", CategoryId = 5 }
            );
        }
    }
}
