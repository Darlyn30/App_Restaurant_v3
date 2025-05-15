
namespace UberEats.Core.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
        public bool DeliveryAvailable { get; set; }
        public string ImgUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
