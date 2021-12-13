using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;

namespace KnowBetter_WebApp.Controllers
{

    public class IngredientsController : Controller
    {
        private readonly KnowBetter_WebAppContext _context;
        //user id
        public const string SessionKeyId = "_Id";

        public IngredientsController(KnowBetter_WebAppContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> AddFavoriteIngredient(int id)
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            UserFavoriteIngredient ufi = new UserFavoriteIngredient()
            {
                IngredientId = id,
                UserId = Convert.ToInt32(userId)
            };

            _context.UserFavoriteIngredient.Add(ufi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FavoriteIngredient));
        }

        public async Task<IActionResult> AddAvoidIngredient(int id)
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            UserAvoidIngredient ufi = new UserAvoidIngredient()
            {
                IngredientId = id,
                UserId = Convert.ToInt32(userId)
            };

            _context.UserAvoidIngredient.Add(ufi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvoidIngredient));
        }

        [HttpPost, ActionName("DeleteFavoriteIngredient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFavoriteIngredient(int id)
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var ingredient =
                await _context.UserFavoriteIngredient.FirstOrDefaultAsync(favIng =>
                    favIng.IngredientId == id && favIng.UserId == Convert.ToInt32(userId));

            _context.UserFavoriteIngredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FavoriteIngredient));
        }

        //Displays fav ingredient list on page
        public async Task<IActionResult> FavoriteIngredient()
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            List<Ingredient> allIngredients = await _context.Ingredient.ToListAsync();
            List<Ingredient> usersFavoriteIngredients = new List<Ingredient>();
            List<UserFavoriteIngredient> favoriteIngredients =
                await _context.UserFavoriteIngredient.Where(fav => fav.UserId == Convert.ToInt32(userId)).ToListAsync();

            foreach (UserFavoriteIngredient uai in favoriteIngredients)
            {
                Ingredient ingredient =
                    await _context.Ingredient.FirstOrDefaultAsync(ing => ing.IngredientId == uai.IngredientId);
                usersFavoriteIngredients.Add(ingredient);
            }

            IngredientResultModel irm = new IngredientResultModel()
            {
                AllIngredients = allIngredients,
                UsersFavOrAvoidIngredients = usersFavoriteIngredients
            };

            return View(irm);
        }

        [HttpPost, ActionName("DeleteAvoidIngredient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAvoidIngredient(int id)
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var ingredient =
                await _context.UserAvoidIngredient.FirstOrDefaultAsync(avoidIng =>
                    avoidIng.IngredientId == id && avoidIng.UserId == userId);

            _context.UserAvoidIngredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvoidIngredient));
        }

        public async Task<IActionResult> AvoidIngredient()
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            List<Ingredient> allIngredients = await _context.Ingredient.ToListAsync();
            List<Ingredient> usersAvoidIngredients = new List<Ingredient>();
            List<UserAvoidIngredient> avoidIngredients =
                await _context.UserAvoidIngredient.Where(avoid => avoid.UserId == Convert.ToInt32(userId)).ToListAsync();

            foreach (UserAvoidIngredient uai in avoidIngredients)
            {
                Ingredient ingredient =
                    await _context.Ingredient.FirstOrDefaultAsync(ing => ing.IngredientId == uai.IngredientId);
                usersAvoidIngredients.Add(ingredient);
            }

            IngredientResultModel irm = new IngredientResultModel()
            {
                AllIngredients = allIngredients,
                UsersFavOrAvoidIngredients = usersAvoidIngredients
            };

            return View(irm);
        }

        public IActionResult IngredientLibrary()
        {
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            return View();
        }

        public ActionResult Details(string query)
        {
            var model = new APIResultModel();
            model.APILinks = GetLinks(query);
            model.IngredientName = query;
            return View(model); 
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
                        string rawImgURL = item.Image.source;
                        int start = rawImgURL.LastIndexOf('/');
                        string urlSubString = rawImgURL.Substring(start);
                        int indexOfSlash = urlSubString.IndexOf('/') + 1;
                        int indexOfFirstDash = urlSubString.IndexOf('-');
                        int lengthOfPictureSize = indexOfFirstDash - indexOfSlash;
                        string pictureSize = urlSubString.Substring(indexOfSlash, lengthOfPictureSize);
                        rawImgURL = rawImgURL.Replace(pictureSize, "300px");
                        aRes.LinkImage = rawImgURL;
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
