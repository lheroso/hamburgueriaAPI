using IngredientModel;
using System;
using System.Collections.Generic;

namespace HamburgerModel
{
    public class Hamburger
    {
        public int id { get; set; }
        public String name { get; set; }
        public ICollection<Ingredient> Ingredientes { get; set; }
    }
}
