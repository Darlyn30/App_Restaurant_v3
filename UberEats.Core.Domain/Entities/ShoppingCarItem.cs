
namespace UberEats.Core.Domain.Entities
{
    public class ShoppingCarItem
    {
        public int Id { get; set; }
        public int ShoppingCarId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
    }
}
