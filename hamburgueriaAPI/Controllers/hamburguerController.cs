using ApiContextDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using IngredientModel;
using HamburgerModel;
using GetDiscountRequest;

namespace hamburgueriaAPI.Controllers
{
    public class hamburguerController : ControllerBase
    {    

        private readonly ILogger<hamburguerController> _logger;
        private readonly ApiContext _context;
        private readonly IHamburgerBusiness _hamburgerBusiness;
    

        public hamburguerController(ILogger<hamburguerController> logger, ApiContext context, IHamburgerBusiness hamburgerBusiness)
        {
            _logger = logger;
            _context = context;
            _hamburgerBusiness = hamburgerBusiness;
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
        public double GetDiscount([FromBody()] GetDiscountRequestModel getDiscountRequestModel)
        {
            return _hamburgerBusiness.GetDiscount(getDiscountRequestModel.HamburgerIngredient, getDiscountRequestModel.Ingredient);
        }

    }
}