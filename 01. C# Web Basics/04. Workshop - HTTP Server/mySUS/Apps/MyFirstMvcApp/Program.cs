using SUS.HTTP;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp

{   //ЗАПОЧВАМЕ ИЗГРАЖДАНЕТО ОТ ТУК

    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();  // 01. създаваме сървър

            server.AddRoute("/", HomePage); // 02. подаваме му ROUTE-ове, което значи да му закачим адрес и към него да се изпълнява зададен код
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);
            //Route-a трябва да получи REQUEST, за да може да върне RESPONSE
            //методите, които приемат HttpRequest и връщат HttpResponce наричаме ACTION-и (тези по-горе)


           await server.StartAsync(80);  // 04. подаваме му порт, за да стартира

        }

        static HttpResponse Favicon(HttpRequest reques)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes); 
            return response;
        }

        static HttpResponse HomePage (HttpRequest request)
        {
            var responseHtml = "<h1>Welcome!</h1>" + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        static HttpResponse About (HttpRequest request)
        {
            var responseHtml = "<h1>About...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        static HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

    }
}
