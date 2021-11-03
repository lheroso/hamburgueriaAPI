using System.Collections.Generic;
using HamburgerModel;
using IngredientModel;

namespace GetDiscountRequest
{
    public class GetDiscountRequestModel
    {
        public List<HamburgerIngredient> HamburgerIngredient { get; set; }
        public List<Ingredient> Ingredient { get; set; }

    }
}
