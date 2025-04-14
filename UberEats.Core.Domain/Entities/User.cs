
namespace UberEats.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = false;
        //public string Token {  get; set; }

        //public ICollection<Order> Orders { get; set; } = new List<Order>();s
    }
}
