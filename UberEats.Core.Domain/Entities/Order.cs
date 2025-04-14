
namespace UberEats.Core.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public DateTime DateOrder { get; set; }
        public string Status { get; set; }

        public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }
}
