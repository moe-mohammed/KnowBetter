using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Since there's no array type mapping in EF, we'll use a string with ingredients separated using commas.
        // This string will be stored in the database called InternalAvoidIngredients.
        // AvoidIngredients is the C# Array that we will use to access that string to retrieve list of avoid ingredients and modify it.
        private string InternalAvoidIngredients { get; set; }

        [NotMapped]
        public string[] AvoidIngredients
        {
            get { return InternalAvoidIngredients.Split(','); }
            set
            {
                AvoidIngredients = value;
                InternalAvoidIngredients = String.Join(",", AvoidIngredients.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
