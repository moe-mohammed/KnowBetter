using KnowBetter_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;


namespace KnowBetter_WebApp.Data
{
    public class DataSeeder
    {
        public static void SeedProducts(KnowBetter_WebAppContext context)
        {
            if (!context.Product.Any())
            {

                /*  Seed file must be prepared before use:
                 *  1. Remove headings
                 *  2. Find and replace single quote (') with 2 single quotes ('')
                 */
                ParseSeed(".\\Data\\KBSeed1.csv", context);

                //Add test user for development purposes
                /*User testUser = new User
                {
                    UserId = "TestUserId",
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "test@test.com",
                    Age = "22",
                    Password = "TestPassword"
                };
                context.User.Add(testUser);
                context.SaveChanges();
                */
            }

        }

        public static void ParseSeed(string filePath, KnowBetter_WebAppContext context)
        {
            var lines = File.ReadLines(filePath);
            string[] lineArray;
            string productName;
            string brand;
            int productId;
            Ingredient ingredient;
            int ingredientId;
            string ingredientName;
            Product product;
            ProductIngredient productIngredient;

            foreach (string line in lines)
            {
                //read line
                lineArray = line.Split(",");
                brand = lineArray[0];
                productName = lineArray[1];

                //add product and get product ID
                product = new Product { Brand = brand, ProductName = productName };
                context.Add(product);
                context.SaveChanges();
                productId = product.ProductId;

                //add ingredients and get ingredient IDs
                for (int i = 2; i < lineArray.Length; i++)
                {
                    ingredientName = lineArray[i];

                    //check if ingredient already exists
                    ingredient = context.Ingredient.FirstOrDefault(i => i.IngredientName == ingredientName);
                    if (ingredient == null)
                    {
                        //add ingredient if it doesn't exist
                        ingredient = new Ingredient { IngredientName = ingredientName };
                        context.Add(ingredient);
                        context.SaveChanges();
                    }

                    //get ingredientID
                    ingredientId = ingredient.IngredientId;

                    //Add ProductIngredient entry
                    productIngredient = new ProductIngredient { ProductId = productId, IngredientId = ingredientId };
                    context.ProductIngredient.Add(productIngredient);
                }

                context.SaveChanges();
            }


        }
    }
}
