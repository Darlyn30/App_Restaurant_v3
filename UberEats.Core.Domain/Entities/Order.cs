
namespace UberEats.Core.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }  // Order status (e.g., "preparing", "delivered", "Cancelled")
        public DateTime OrderDate { get; set; }
    }
}
