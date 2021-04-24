using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Demo_State_Management
{
    class Program
    {
        static Dictionary<string, int> SessionStorage = new Dictionary<string, int>(); // създаваме този речник, за да броим колко заявки е направила към нас всяка една сесия
        //Този SessionStorage работи със сесийни ID-та

        const string NewLine = "\r\n"; // => създаване на нов ред

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;  // => за четене на кирилица
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);  // => избираме на кой порт от нашия сървър да работим и да очакваме клиентска заявка
            tcpListener.Start();  // => активираме очакването на заявка

            while (true) // => докато заявката е правилна
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync(); // => създаваме клиент
                await ProcessClientAsync(client);
            }

        }

        private static async Task ProcessClientAsync(TcpClient client)
        {
           await using (var stream = client.GetStream())  // => отваряме stream, кпйто клиента да използва  ===> винаги ползваме USING, когато имаме STREAM
            {
                byte[] buffer = new byte[1000000]; // => създаваме масив от bytes, което да бъде цялата получена информация
                var lenght = stream.Read(buffer, 0, buffer.Length); // => изчисляваме дълкината на масива

                string requestString = Encoding.UTF8.GetString(buffer, 0, lenght);  // => превръщаме масива от bytes във четим формат(string)
                Console.WriteLine(requestString); // => прочитаме изпратената от клиента информация



                var sid = Guid.NewGuid().ToString(); // сесийното ID го приемаме за нов GUID
                var match = Regex.Match(requestString, @"sid=[^\n]*\r\n]");  // чрез регекс можем да търсим по абстрактни изрази в текст
                //=> търсим нещо, което започва със SID= един или много пъти, не е на нов ред, и завършва с нов ред
                //=> ако намерим нещо, значи това е COOKIE, която ни е изпратена от сесийното ID
                if (match.Success) // => ако намерим такова ID го присвояваме на SID
                {
                    sid = match.Value.Substring(4);
                }

                if (!SessionStorage.ContainsKey(sid))
                {
                    SessionStorage.Add(sid, 0);
                }

               SessionStorage[sid]++;

                bool sessionSet = false;
                if (requestString.Contains("sid ="))
                {
                    sessionSet = true;
                }


                string html = $"<h1>Hello from Georgi_Server {DateTime.Now} for the {SessionStorage[sid]} time</h1>" +
                    $"<form action=/tweet method=post><input name=username /><input name=password />" +
                    $"<input type=submit /></form>";  // => подготвяме отговор към клиента под формата на четим формат (string)

                string response = "HTTP/1.1 200 OK" // => създаваме конструкцията на reponse-a
                    + NewLine + "Server: GradevServer 2020"
                    + NewLine +
                    //"Location: https://www.google.com" + NewLine +
                    "Content-Type: text/html; charset=utf-8"
                    + NewLine +
                    "X-Server-Version: 1.0"
                    + NewLine +
                    (!sessionSet ? ($"Set-Cookie: sid={sid}; lang=en; Expires: " + DateTime.UtcNow.AddHours(1).ToString("R")) : string.Empty)  // => създаваме Cookie само при условие, че вече не е създадено
                    + NewLine +

                    // "Content-Disposition: attachment; filename=niki.txt" + NewLine +
                    "Content-Lenght: " + html.Length
                    + NewLine
                    + NewLine + html // => тук добавяме по-горе подготвеният отговор към клиента
                    + NewLine; // накрая трябва да има нов ред, за да не цикли браузъра

                byte[] responseBytes = Encoding.UTF8.GetBytes(response); // => преобразуваме отговора в масив от bytes
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                Console.WriteLine($"sid={sid}");
                Console.WriteLine(new string('=', 70)); // => слагаме разделител "=======" между отделните заявки
            }
        }






        //public static async Task ReadData()
        //{
        //    string url = "https://softuni.bg/courses/csharp-web-basics";
        //    HttpClient httpClient = new HttpClient();
        //    var response = await httpClient.GetAsync(url);
        //    Console.WriteLine(response.StatusCode);
        //    Console.WriteLine(string.Join(Environment.NewLine, response.Headers.Select(x => x.Key + ": " + x.Value.First())));

        //    // var html = await httpClient.GetStringAsync(url);
        //    // Console.WriteLine(html);
        //}
    }
}
