using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using IngredientModel;
using HamburgerModel;

namespace hamburgueriaAPI.Controllers.Tests
{
    [TestClass()]
    public class hamburguerControllerTests
    {
        [TestMethod()]
        public void GetDiscountTest()
        {
            //Arrange
            HamburgerBusiness hamburgerStore = new HamburgerBusiness();
            List<HamburgerIngredient> hamburgerIngredients;
            var ingredients = new List<Ingredient> { new Ingredient { IngredientID = 1, Name = "Alface", Price = 0.4 },
                                                     new Ingredient { IngredientID = 2, Name = "Bacon", Price = 2},
                                                     new Ingredient { IngredientID = 3, Name = "Hamburguer de Carne", Price = 3},
                                                     new Ingredient { IngredientID = 4, Name = "Ovo", Price = 0.8},
                                                     new Ingredient { IngredientID = 5, Name = "Queijo", Price = 1.5}};
            double response;

            //Test promocao "Muita Carne"
            hamburgerIngredients = getMockIngredients(2);
            response = hamburgerStore.GetDiscount(hamburgerIngredients, ingredients);
            Assert.AreEqual(ingredients[2].Price * 2, response);

            //Test promocao "Light"
            hamburgerIngredients = getMockIngredients(1);
            response = hamburgerStore.GetDiscount(hamburgerIngredients, ingredients);
            Assert.AreEqual(ingredients[0].Price * 0.1, response);

            //Test promocao "Muito Queijo"
            hamburgerIngredients = getMockIngredients(3);
            response = hamburgerStore.GetDiscount(hamburgerIngredients, ingredients);
            Assert.AreEqual(ingredients[4].Price * 2, response);

        }

        private List<HamburgerIngredient> getMockIngredients(int promocao)
        {

            var hamburgerIngredients = new List<HamburgerIngredient> { new HamburgerIngredient { HamburgerID = 1, IngredientID = 1, IngredientQuantity = 0  },
                                                                       new HamburgerIngredient { HamburgerID = 1, IngredientID = 3, IngredientQuantity = 0  },
                                                                       new HamburgerIngredient { HamburgerID = 1, IngredientID = 5, IngredientQuantity = 0  }};
            switch (promocao)
            {
                //Tem alface, mas nao tem bacon
                case 1:
                    hamburgerIngredients[0].IngredientQuantity = 1;
                    break;
                //6 Hamburguers de carne
                case 2:
                    hamburgerIngredients[1].IngredientQuantity = 6;
                    break;
                //6 Bacons
                case 3:
                    hamburgerIngredients[2].IngredientQuantity = 6;
                    break;
            }
            return hamburgerIngredients;
        }
    }
}
