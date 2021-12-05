﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;
//using ASP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KnowBetter_WebApp.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly KnowBetter_WebAppContext _context;
        private readonly int userId = 1;

        public IngredientsController(KnowBetter_WebAppContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredient.ToListAsync());
        }

        [HttpPost, ActionName("DeleteFavoriteIngredient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFavoriteIngredient(int id)
        {
            var ingredient =
                await _context.UserFavoriteIngredient.FirstOrDefaultAsync(favIng =>
                    favIng.IngredientId == id && favIng.UserId == userId);

            _context.UserFavoriteIngredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FavoriteIngredient));
        }

        //Displays fav ingredient list on page
        public async Task<IActionResult> FavoriteIngredient()
        {
            List<Ingredient> usersFavoriteIngredients = new List<Ingredient>();
            List<UserFavoriteIngredient> favIngredients =
                await _context.UserFavoriteIngredient.Where(fav => fav.UserId == userId).ToListAsync();

            foreach (UserFavoriteIngredient ufi in favIngredients)
            {
                Ingredient ingredient =
                    await _context.Ingredient.FirstOrDefaultAsync(ing => ing.IngredientId == ufi.IngredientId);
                usersFavoriteIngredients.Add(ingredient);
            }

            return View(usersFavoriteIngredients);
        }

        [HttpPost, ActionName("DeleteAvoidIngredient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAvoidIngredient(int id)
        {
            var ingredient =
                await _context.UserAvoidIngredient.FirstOrDefaultAsync(avoidIng =>
                    avoidIng.IngredientId == id && avoidIng.UserId == userId);

            _context.UserAvoidIngredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvoidIngredient));
        }

        public async Task<IActionResult> AvoidIngredient()
        {
            List<Ingredient> usersAvoidIngredients = new List<Ingredient>();
            List<UserAvoidIngredient> avoidIngredients =
                await _context.UserAvoidIngredient.Where(avoid => avoid.UserId == userId).ToListAsync();

            foreach (UserAvoidIngredient uai in avoidIngredients)
            {
                Ingredient ingredient =
                    await _context.Ingredient.FirstOrDefaultAsync(ing => ing.IngredientId == uai.IngredientId);
                usersAvoidIngredients.Add(ingredient);
            }

            return View(usersAvoidIngredients);
        }

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
