using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public sealed class EShopDbContext : IdentityDbContext<User, UserRole, int>
{
    public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserAddress> UserAddresses { get; set; }

    public DbSet<UserPayment> UserPayments { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<PaymentDetails> PaymentDetails { get; set; }

    public DbSet<OrderItems> OrderItems { get; set; }

    public DbSet<OrderDetails> OrderDetails { get; set; }

    public DbSet<Discount> Discounts { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
}