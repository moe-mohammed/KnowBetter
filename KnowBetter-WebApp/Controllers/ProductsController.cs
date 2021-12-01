using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;
using System.Diagnostics;

namespace KnowBetter_WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly KnowBetter_WebAppContext _context;

        public ProductsController(KnowBetter_WebAppContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }

        public async Task<IActionResult> CompareProductSelect()
        {
            //Get all products and send to View
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> CompareProductResults(int id)
        {
            //Create list of ingredient names that match ProductId == id
            List<string> ingredientNames = new List<string>();
            List<ProductIngredient> productsIngredients =
                await _context.ProductIngredient.Where(pI => pI.ProductId == id).ToListAsync();
            foreach (var productIngredient in productsIngredients)
            {
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(ing =>
                    ing.IngredientId == productIngredient.IngredientId);
                ingredientNames.Add(ingredient.IngredientName);
            }
            //Convert list of ingredient names to array and pass to compare task
            return await CompareProductResult(ingredientNames.ToArray());
        }

        public async Task<IActionResult> CompareProductResult(string[] ingredientNames)
        {
            //create list of user avoid ingredient names
            List<string> userAvoidIngredientNames = new List<string>();
            //Assumes userID is 1 to match test user.
            List<UserAvoidIngredient> avoidIngredients =
                await _context.UserAvoidIngredient.Where(uAI => uAI.UserId == 1).ToListAsync();
            foreach (var avoidIngredient in avoidIngredients)
            {
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(ing =>
                    ing.IngredientId == avoidIngredient.IngredientId);
                userAvoidIngredientNames.Add(ingredient.IngredientName);
            }
            string[] avoidIngArray = userAvoidIngredientNames.ToArray();

            //create list of user favorite ingredient names
            List<string> userFavoriteIngredientNames = new List<string>();
            //Assumes userID is 1 to match test user.
            List<UserFavoriteIngredient> favoriteIngredients =
                await _context.UserFavoriteIngredient.Where(uFI => uFI.UserId == 1).ToListAsync();
            foreach (var favoriteIngredient in favoriteIngredients)
            {
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(ing =>
                    ing.IngredientId == favoriteIngredient.IngredientId);
                userFavoriteIngredientNames.Add(ingredient.IngredientName);
            }
            string[] favIngArray = userFavoriteIngredientNames.ToArray();

            //compare product ingredients to favorite and avoid lists. Place name and comparison result enum into a new results list.
            List<CompareIngredientResult> resultsList = new List<CompareIngredientResult>();
            foreach (string iName in ingredientNames)
            {
                CompareIngredientResult result = new CompareIngredientResult();
                result.IngredientName = iName;
                result.Result = compResult.Default;
                foreach (string fName in favIngArray)
                {
                    if (iName == fName)
                    {
                        result.Result = compResult.Favorite;
                    }
                }
                foreach (string aName in avoidIngArray)
                {
                    if (iName == aName)
                    {
                        result.Result = compResult.Avoid;
                    }
                }
                resultsList.Add(result);
            }

            // pass results list to view
            return View(resultsList);
        }

    }
}
