using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;
using System.IO;
using System.Net;
using System.Xml.Serialization;


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
        public ActionResult Details(string query)
        {
            var model = new APIResultModel();
            model.APILinks = GetLinks(query);
            model.IngredientName = query;
            return View(model); 
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

        private List<APIResult> GetLinks(string ingredient)
        {
            string query = ingredient;
            List<APIResult> links = new List<APIResult>();
            int numberOfResults = 5;
            var webRequest =
                WebRequest.Create(
                        "https://en.wikipedia.org/w/api.php?action=opensearch&format=xml&namespace=0&limit="+numberOfResults+"&search="+query)
                    as HttpWebRequest;
            var stream = webRequest.GetResponse().GetResponseStream();
            XmlSerializer serializer = new XmlSerializer(typeof(APIDeserializer.SearchSuggestion));
            APIDeserializer.SearchSuggestion ssJ;
            using (var streamReader = new StreamReader(stream))
            {
                ssJ = (APIDeserializer.SearchSuggestion)serializer.Deserialize(streamReader);

                foreach (APIDeserializer.SearchSuggestionItem item in ssJ.Section)
                {
                    APIResult aRes = new APIResult();
                    aRes.LinkName = item.Text.Value;
                    aRes.LinkUrl = item.Url.Value;
                    if (item.Image != null)
                    {
                        aRes.LinkImage = item.Image.source;
                    }
                    else
                    {
                        aRes.LinkImage = "";
                    }
                    links.Add(aRes);   
                }
            }
            return links;
        } 
    }   
    }
