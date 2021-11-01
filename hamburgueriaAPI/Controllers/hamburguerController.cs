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
            //var response = _context.Ingredient.Where(i => i.Id == id).Select(i => new Ingredient { Id = i.Id, Ingrediente = i.Ingrediente, Preco = i.Preco }).ToList().FirstOrDefault();
            var response = _context.Hamburger.Include(t => t.ingredients).ToList();
            return response;
        }

        [HttpPost]
        [Route("/getDiscount")]
        public double GetDiscount(List<Ingredient> ingredients)
        {
            double price = ingredients.Sum(items => items.price * items.quantity);
            double discount = 0;

            //Regra de disconto = "Muita Carne"
            var ingredientBurger = ingredients.Find(item => item.id == 3);
            discount = discount + Convert.ToInt32(ingredientBurger.quantity/3) * ingredientBurger.price ;
            //Regra de disconto = "Muito Queijo"
            var ingredientCheese = ingredients.Find(item => item.id == 5);
            discount = discount + Convert.ToInt32(ingredientCheese.quantity / 3) * ingredientCheese.price;
            //Regra de disconto = "Light"
            if (ingredients.Find(item => item.id == 1).quantity > 0 &&
                ingredients.Find(item => item.id == 2).quantity == 0)
                discount = ((price-discount) * 0.1) + discount;

            return discount;

        }

    }
}

/* 1. Criar modelos de input e response
 * 2. Mapear quais endpoints serão necessários
 * 3. Colocar as regras de negócio nos endpoints
 */
