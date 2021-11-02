using HamburgerModel;
using IngredientModel;
using Microsoft.EntityFrameworkCore;

namespace ApiContextDb
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        { }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Hamburger> Hamburger { get; set; }
        public DbSet<HamburgerIngredient> HamburgerIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HamburgerIngredient>()
                .HasKey(hi => new
                {
                    hi.HamburgerID, hi.IngredientID
                });
            modelBuilder.Entity<HamburgerIngredient>()
                .HasOne(hi => hi.Hamburger)
                .WithMany(i => i.HamburgerIngredient)
                .HasForeignKey(hi => hi.HamburgerID);
            
            modelBuilder.Entity<HamburgerIngredient>()
                .HasOne(hi => hi.Ingredient)
                .WithMany(i => i.HamburgerIngredient)
                .HasForeignKey(hi => hi.IngredientID);

        }
    }
}