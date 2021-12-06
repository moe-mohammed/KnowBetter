using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowBetter_WebApp.Models;

namespace KnowBetter_WebApp.Models
{
    public class APIResultModel
    {
        public List<APIResult> APILinks { get; set; }
        public string IngredientName { get; set; }

    }
}
