using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Domain.src.Entities;

namespace WebApi.Infrastructure.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Image> Images { get; set; }

        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Below code has been moved to the program.cs file due to the npgslp version 4 recomentations not to create new npgsqldatasourcebuilder within the scope
            // var builder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("Default"));
            // builder.MapEnum<UserRole>();
            // builder.MapEnum<OrderStatus>();
            // optionsBuilder.AddInterceptors(new TimeStampInterceptor());
            // optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresEnum<UserRole>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<OrderDetail>().HasKey("OrderId", "ProductId"); 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User) 
                .WithMany(u => u.Orders) 
                .HasForeignKey(o => o.UserId); 
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages) 
                .WithOne()                     
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}