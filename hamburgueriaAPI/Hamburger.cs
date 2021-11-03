using IngredientModel;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HamburgerModel
{
    public class Hamburger
    {
        public int HamburgerID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<HamburgerIngredient> HamburgerIngredient { get; set; }
    }

    public class HamburgerIngredient
    {
        public int HamburgerID { get; set; }
        public Hamburger Hamburger { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientQuantity { get; set; }
    }
}
