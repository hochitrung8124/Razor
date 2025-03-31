using Microsoft.EntityFrameworkCore;

namespace U3.Pages.Models
{
    public class MyDBc : DbContext
    {
        public MyDBc() { }

        public MyDBc(DbContextOptions<MyDBc> options) : base(options) { }

        // Định nghĩa DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TRUNGHC\\SQLEXPRESS;Database=Razor_AA;Trusted_Connection=True;Encrypt=False;");



            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình cho bảng Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .IsRequired();
                entity.HasIndex(e => e.Name)
                      .IsUnique();

                // Description là optional
                entity.Property(e => e.Description);

                // Quan hệ 1 - N: Category -> Product, khi Category bị xóa, category_id của Product sẽ set thành null
                entity.HasMany(e => e.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Cấu hình cho bảng Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .IsRequired();

                entity.Property(e => e.Description);

                // Vì trong C# Product.ImagePaths là List<string> nhưng trong DB lưu dưới dạng TEXT,
                // ta sử dụng ValueConverter để chuyển đổi List<string> thành chuỗi và ngược lại.
                entity.Property(e => e.ImagePaths)
                      .HasConversion(
                          v => string.Join(";", v),  // Chuyển từ List<string> sang chuỗi (dùng dấu ; làm phân cách)
                          v => v.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                      );

                entity.Property(e => e.Price)
                      .IsRequired();
                // Check constraint cho Price >= 0
                entity.HasCheckConstraint("CK_Product_Price", "Price >= 0");

                entity.Property(e => e.Stock)
                      .IsRequired();
                // Check constraint cho Stock >= 0
                entity.HasCheckConstraint("CK_Product_Stock", "Stock >= 0");

                // Khóa ngoại (CategoryId) được cấu hình ở phía Category
            });

            // Cấu hình cho bảng User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                      .IsRequired();
                entity.HasIndex(e => e.Username)
                      .IsUnique();

                entity.Property(e => e.Email)
                      .IsRequired();
                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.Password)
                      .IsRequired();

                entity.Property(e => e.Role)
                      .IsRequired()
                      .HasDefaultValue("customer");

                entity.HasCheckConstraint("CK_User_Role", "Role IN ('admin', 'customer')");
            });
        }
    }
}
