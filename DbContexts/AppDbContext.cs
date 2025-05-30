using Microsoft.EntityFrameworkCore;
using PackSolverAPI.Models;

namespace PackSolverAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Box> boxes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ProductBox> productBoxes { get; set; }
        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductBox>().HasKey(opb => new {opb.ProductId, opb.BoxId});

            modelBuilder.Entity<Box>().HasData(
                new Box { BoxId = "CAIXA_1", Height = 30, Width = 40, Length = 80 },
                new Box { BoxId = "CAIXA_2", Height = 80, Width = 50, Length = 40 },
                new Box { BoxId = "CAIXA_3", Height = 50, Width = 80, Length = 60 }
            );
        }
    }
}
