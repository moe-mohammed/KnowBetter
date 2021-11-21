using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class UserFavoriteIngredient
    {
        public int UserFavoriteIngredientId { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
    }
}
