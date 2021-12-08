using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class IngredientResultModel
    {
        public List<Ingredient> UsersFavOrAvoidIngredients { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
    }
}
