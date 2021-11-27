using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class ProductIngredient
    {
        public int ProductIngredientId { get; set; }
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
    }
}
