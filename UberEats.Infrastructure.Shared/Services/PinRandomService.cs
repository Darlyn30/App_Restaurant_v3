
namespace UberEats.Infrastructure.Shared.Services
{
    public class PinRandomService
    {   
        /*
        Esta clase se encarga de generar un pin random 
        */
        Random random = new Random();
        private static PinRandomService _instance;

        private PinRandomService()
        {
            //
        }

        public static PinRandomService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PinRandomService();
                }
                return _instance;
            }
        }

        public string pinRandom()
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string pin = nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)];

            Console.WriteLine(pin);

            return pin;
        }
    }
}
