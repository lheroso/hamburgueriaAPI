using HamburgerModel;
using IngredientModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hamburgueriaAPI
{
    public class HamburgerBusiness: IHamburgerBusiness
    {

        public HamburgerBusiness(){}
        public double GetDiscount(List<HamburgerIngredient> hamburgerIngredients, List<Ingredient> ingredients)
        {
            List<Ingredient> hamIng = new List<Ingredient>();
            double price;

            foreach (var hamburger in hamburgerIngredients)
            {
                hamIng.Add(ingredients.Where(i => i.IngredientID == hamburger.IngredientID)
                    .FirstOrDefault());

            }

            
            double discount = 0;
            price = hamIng.Sum(item => item.Price * hamburgerIngredients.Find(i => i.IngredientID == item.IngredientID).IngredientQuantity);

            //Regra de desconto = "Muita Carne"
            var ingredientBurger = hamIng.Find(item => item.IngredientID == 3);
            if(ingredientBurger!=null)
                discount = discount + Convert.ToInt32(((hamburgerIngredients.Find(i => i.IngredientID == ingredientBurger.IngredientID).IngredientQuantity) / 3)) * ingredientBurger.Price;
            //Regra de desconto = "Muito Queijo"
            var ingredientCheese = hamIng.Find(item => item.IngredientID == 5);
            if (ingredientCheese != null)
                discount = discount + Convert.ToInt32(((hamburgerIngredients.Find(i => i.IngredientID == ingredientCheese.IngredientID).IngredientQuantity) / 3)) * ingredientCheese.Price;
            //Regra de desconto = "Light";
            if (hamburgerIngredients.Find(item => item.IngredientID == 1) != null)
            {
                if (hamburgerIngredients.Find(item => item.IngredientID == 1).IngredientQuantity > 0 &&
                    hamburgerIngredients.Find(item => item.IngredientID == 2) == null)
                    discount = ((price - discount) * 0.1) + discount;
                else if(hamburgerIngredients.Find(item => item.IngredientID == 1).IngredientQuantity > 0 &&
                    hamburgerIngredients.Find(item => item.IngredientID == 2).IngredientQuantity == 0 )
                    discount = ((price - discount) * 0.1) + discount;
            }

            return discount;

        }
    }
}
