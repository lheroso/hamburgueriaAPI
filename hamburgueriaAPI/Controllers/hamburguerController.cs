using ApiContextDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IngredientModel;
using HamburgerModel;

namespace hamburgueriaAPI.Controllers
{
    [ApiController]
    public class hamburguerController : ControllerBase
    {    

        private readonly ILogger<hamburguerController> _logger;

        private readonly ApiContext _context;

        public hamburguerController(ILogger<hamburguerController> logger, ApiContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Route("/getIngredients")]
        public List<Ingredient> GetIngredients()
        {
            return _context.Ingredient.ToList();
        }

        [HttpGet]
        [Route("/getBurgers")]
        public List<Hamburger> GetBurgers()
        {
            //var response = _context.Hamburger.Include(t => t.Ingredients).ToList();
            //var response = _context.Hamburger.SelectMany(i => i.HamburgerIngredient).Select(h => h.Hamburger).ToList();
            var response = _context.Hamburger.ToList();

            
            return response;
        }
        
        [HttpGet]
        [Route("/getBurgerIngredients")]
        public List<Ingredient> GetBurgerIngredients(int hid)
        {
            var hamburgers = _context.HamburgerIngredients.Where(hi => hi.HamburgerID == hid).ToList();

            var hamburgersIngredients = new List<Ingredient>();

            foreach (var hamburger in hamburgers)
            {
                hamburgersIngredients.Add(_context.Ingredient.Where(i => i.IngredientID == hamburger.IngredientID)
                    .FirstOrDefault());
            }

            return hamburgersIngredients;
        }

        [HttpPost]
        [Route("/getDiscount")]
        public double GetDiscount(List<HamburgerIngredient> ingredients)
        {
            double price = ingredients.Sum(items => items.Ingredient.Price * items.IngredientQuantity);
            double discount = 0;

            //Regra de disconto = "Muita Carne"
            var ingredientBurger = ingredients.Find(item => item.IngredientID == 3);
            discount = discount + Convert.ToInt32(ingredientBurger.IngredientQuantity/3) * ingredientBurger.Ingredient.Price ;
            //Regra de disconto = "Muito Queijo"
            var ingredientCheese = ingredients.Find(item => item.IngredientID == 5);
            discount = discount + Convert.ToInt32(ingredientCheese.IngredientQuantity / 3) * ingredientCheese.Ingredient.Price;
            //Regra de disconto = "Light"
            if (ingredients.Find(item => item.IngredientID == 1).IngredientQuantity > 0 &&
                ingredients.Find(item => item.IngredientID == 2).IngredientQuantity == 0)
                discount = ((price-discount) * 0.1) + discount;

            return discount;

        }

    }
}

/* 1. Criar modelos de input e response
 * 2. Mapear quais endpoints serão necessários
 * 3. Colocar as regras de negócio nos endpoints
 */
