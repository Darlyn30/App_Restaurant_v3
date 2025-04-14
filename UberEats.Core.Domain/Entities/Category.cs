
namespace UberEats.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Productos { get; set; } = new List<Product>();
    }
}
