using Microsoft.EntityFrameworkCore;
using U3.Pages.Models;

namespace U3.Pages.Data
{
    public class MyDBc : DbContext
    {
        public MyDBc() { }

        public MyDBc(DbContextOptions<MyDBc> options) : base(options) { }

        // Định nghĩa DbSet
        public DbSet<Productaa> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TRUNGHC\\SQLEXPRESS;Database=Razor;Trusted_Connection=True;Encrypt=False;");


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Productaa>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Price).IsRequired().HasColumnType("REAL");
                entity.Property(p => p.Stock).IsRequired();

                // Đặt ràng buộc CHECK ở cấp entity thay vì cấp property
                entity.HasCheckConstraint("CK_Product_Price", "Price >= 0");
                entity.HasCheckConstraint("CK_Product_Stock", "Stock >= 0");

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(255);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Role).IsRequired().HasDefaultValue("customer");
                entity.HasCheckConstraint("CK_User_Role", "Role IN ('admin', 'customer')");
            });
        }
    }
}
