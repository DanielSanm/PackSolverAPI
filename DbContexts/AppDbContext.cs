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
                new Box { BoxId = "CAIXA_1", Dimensions = "30x40x80" },
                new Box { BoxId = "CAIXA_2", Dimensions = "80x50x40" },
                new Box { BoxId = "CAIXA_3", Dimensions = "50x80x60" }
            );
        }
    }
}
