using KnowBetter_WebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace KnowBetter_WebApp.Data
{
    public class DataSeeder
    {
        public static void SeedProducts(DbContext context)
        {
            var newProduct = new Product {ProductName = "Test Product 1"};
            context.Add(newProduct);
            context.SaveChanges();
        }
    }
}
