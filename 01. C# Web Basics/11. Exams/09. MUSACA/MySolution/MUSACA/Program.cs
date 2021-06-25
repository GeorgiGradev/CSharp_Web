using SUS.MvcFramework;
using System.Threading.Tasks;

namespace MUSACA
{
    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup(), 80);
        }
    }
}
