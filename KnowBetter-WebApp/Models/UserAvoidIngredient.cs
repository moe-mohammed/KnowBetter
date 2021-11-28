using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class UserAvoidIngredient
    {
        public int UserAvoidIngredientId { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
    }
}
