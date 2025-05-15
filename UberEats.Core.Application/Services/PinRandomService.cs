
namespace UberEats.Infrastructure.Shared.Services
{
    public class PinRandomService
    {
        /*
        Esta clase se encarga de generar un pin random 
        */
        Random random = new Random();
        private static PinRandomService instance;

        private PinRandomService()
        {
            //
        }

        public static PinRandomService Instance()
        {
            if (instance == null)
            {
                instance = new PinRandomService();
            }

            return instance;
        }

        public string pinRandom()
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string pin = nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)] +
             nums[random.Next(nums.Length)];

            Console.WriteLine(pin);

            return pin;
        }
    }
}
