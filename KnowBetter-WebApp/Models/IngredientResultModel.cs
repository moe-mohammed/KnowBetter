using System.Collections.Generic;

namespace KnowBetter_WebApp.Models
{
    public class IngredientResultModel
    {
        public List<Ingredient> UsersFavOrAvoidIngredients { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
    }
}
