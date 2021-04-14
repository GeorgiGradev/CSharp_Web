using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;  // => за четене на кирилица
            const string NewLine = "\r\n"; // => създаване на нов ред, независимо от вида на насрещния сървър или клиент

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);  // => избираме на кой порт от нашия сървър да работим и да очакваме клиентска заявка
            tcpListener.Start();  // => активираме очакването на заявка

            while (true)  // => докато заявката е правилна
            { 
                var client = tcpListener.AcceptTcpClient(); // => изграждаме клиент
                using (var stream = client.GetStream())  // => отваряме stream, кпйто клиента да използва  ===> винаги ползваме USING, когато имаме STREAM
                {
                    byte[] buffer = new byte[1000000]; // => създаваме масив от bytes, което да бъде цялата получена информация
                    var lenght = stream.Read(buffer, 0, buffer.Length); // => изчисляваме дълкината на масива

                    string requestString = Encoding.UTF8.GetString(buffer, 0, lenght);  // => превръщаме масива от bytes във четим формат(string)
                    Console.WriteLine(requestString); // => прочитаме изпратената от клиента информация

                    string html = $"<h1>Hello from Georgi_Server {DateTime.Now}</h1>" +
                        $"<form action=/tweet method=post><input name=username /><input name=password />" +
                        $"<input type=submit /></form>";  // => подготвяме отговор към клиента под формата на четим формат (string)

                    string response = "HTTP/1.1 200 OK" // => създаваме конструкцията на reponse-a
                        + NewLine + "Server: NikiServer 2020" 
                        + NewLine +
                        //"Location: https://www.google.com" + NewLine +
                        "Content-Type: text/html; charset=utf-8" 
                        + NewLine +
                        // "Content-Disposition: attachment; filename=niki.txt" + NewLine +
                        "Content-Lenght: " + html.Length 
                        + NewLine 
                        + NewLine + html // => тук добавяме по-горе подготвеният отговор към клиента
                        + NewLine; // накрая трябва да има нов ред, за да не цикли браузъра

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response); // => преобразуваме отговора в масив от bytes
                    stream.Write(responseBytes); // изпращаме го 

                    Console.WriteLine(new string('=', 70)); // => слагаме разделител "=======" между отделните заявки
                }
            }
        }

        public static async Task ReadData()
        {
            string url = "https://softuni.bg/courses/csharp-web-basics";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine, response.Headers.Select(x => x.Key + ": " + x.Value.First())));

            // var html = await httpClient.GetStringAsync(url);
            // Console.WriteLine(html);
        }
    }
}
