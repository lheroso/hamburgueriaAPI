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
    }
}