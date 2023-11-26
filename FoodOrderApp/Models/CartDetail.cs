namespace FoodOrderApp.Models
{
    public class CartDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Food Food { get; set; }
    }
}
