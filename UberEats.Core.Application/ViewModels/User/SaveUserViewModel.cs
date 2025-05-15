
namespace UberEats.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
        public bool isActive {  get; set; }
    }
}
