namespace KnowBetter_WebApp.Models
{
    public class UserFavoriteIngredient
    {
        public int UserFavoriteIngredientId { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
    }
}
