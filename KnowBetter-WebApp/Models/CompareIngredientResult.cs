using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class CompareIngredientResult
    {
        public string IngredientName { get; set; }
        public compResult Result { get; set; }
    }

    public enum compResult
    {
        Default,
        Avoid,
        Favorite
    }
}
