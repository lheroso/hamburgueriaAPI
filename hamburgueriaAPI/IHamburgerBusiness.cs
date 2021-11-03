using HamburgerModel;
using IngredientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hamburgueriaAPI
{
    public interface IHamburgerBusiness
    {
        public double GetDiscount(List<HamburgerIngredient> hamburgerIngredients, List<Ingredient> ingredients);
    }
}
