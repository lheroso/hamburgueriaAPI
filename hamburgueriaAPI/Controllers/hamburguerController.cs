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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        

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
            var response = _context.Hamburger.ToList();
            return response;
        }

    }
}

/* 1. Criar modelos de input e response
 * 2. Mapear quais endpoints serão necessários
 * 3. Colocar as regras de negócio nos endpoints
 */
