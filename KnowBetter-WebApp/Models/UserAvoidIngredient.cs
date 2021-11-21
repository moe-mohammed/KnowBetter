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
        [Key]
        public int UserAvoidIngredientId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }
    }
}
