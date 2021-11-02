using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HamburgerModel;

namespace IngredientModel
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public virtual ICollection<HamburgerIngredient> HamburgerIngredient { get; set; }
    }
}
