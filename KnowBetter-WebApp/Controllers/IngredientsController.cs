using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ASP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;

namespace KnowBetter_WebApp.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly KnowBetter_WebAppContext _context;

        public IngredientsController(KnowBetter_WebAppContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredient.ToListAsync());
        }

        //public async Task<IActionResult> GetUserAvoidIngredients(string userId)
        //{
        //    List<UserAvoidIngredient> avoidIngredient = _context.UserAvoidIngredient.Where(uai => uai.UserId == userId).toList();


        //}

        //public async Task<IActionResult> AvoidIngredients()
        //{
        //    string userId = "1";
        //    List<Ingredient> ingredients = _context.Ingredient.ToList();
        //    if (ingredients != null)
        //    {
        //        ViewBag.data = ingredients;
        //    }

        //    return View(userId);
        //}

        // POST: Ingredients/AddAvoid
        // Adds avoid ingredient to a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAvoid(int userId, [Bind("IngredientId,IngredientName")] Ingredient ingredient)
        {
            
            if (ModelState.IsValid)
            {
                UserAvoidIngredient uai = new UserAvoidIngredient() {
                    IngredientId = ingredient.IngredientId,
                    UserId = userId
                };
                _context.UserAvoidIngredient.Add(uai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        //public async Task<IActionResult> FavoriteIngredient(int? userId)
        //{
        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userId);
        //}

        //public async Task<IActionResult> AvoidIngredient(int? userId)
        //{
        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userId);
        //}

        public IActionResult IngredientLibrary()
        {
            return View();
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId,IngredientName")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IngredientId,IngredientName")] Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.IngredientId))
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
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);
            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredient.Any(e => e.IngredientId == id);
        }
    }
}
