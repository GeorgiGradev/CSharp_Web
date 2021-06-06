using SUS.MvcFramework;
using System.Threading.Tasks;

namespace Panda
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup(), 80);
        }
    }
}
