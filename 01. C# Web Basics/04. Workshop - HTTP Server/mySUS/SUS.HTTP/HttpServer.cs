using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        IDictionary<string, Func<HttpRequest, HttpResponse>> routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action) // метод при който по даден адрес се подава дадена фунционалност 
        {
            if (routeTable.ContainsKey(path)) // ако нашата колекция съдържа пътя, й добавяваме нова фунция
            {
                routeTable[path] = action;
            }
            else
            {
                routeTable.Add(path, action);
            }
        }
         
        public async Task StartAsync(int port)  // стартира MVC Server и започва да обработва всяка заявка
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            // създаваме процес очакващ заяква, който да работи на локалния ни адрес. Това ограничава отварянето да става само вътре в компютъра (на Port 80)

            tcpListener.Start();  // стартираме горният процес

            while (true) // докато имаме заявки изпълнявай следното
            { 
               TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync(); // изчакай докато някой се закачи и като се закачи приеми заявката му
                // по-горе правим метода ASYNC TASK, защото искаме да AWAIT-нем и да изчакаме резултат преди да подадем информацията надолу.

                ProcessClientAsync(tcpClient); // закачи ли се клиент този метод започва да обработва заяквата му
                // не се AWAIT-ва, защото пускаме всички клиенти да пращат заявки паралелно и не изчакваме да свърши един, за да започне друг
            }
          
        }

        private async Task ProcessClientAsync(TcpClient tcpClient) // остава PRIVATE, за да не се вижда отвън
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream()) // извиквае STREAM(поток на данни)
                {
                    int position = 0; // при приемането на заявката започваме броенето на byte-овете от 0
                    byte[] buffer = new byte[HttpConstants.BufferSize]; // създавме буфер (4 kb)
                    List<byte> data = new List<byte>(); // тук събираме информацията от буфера ТУК СА ВСИЧКИ ДАННИ

                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;

                        if (count < buffer.Length) // тук ще влезне единствено в края, ако буферът не е запълнен
                        {
                            var partialBuffer = new byte[count];
                            Array.Copy(buffer, partialBuffer, count);
                            data.AddRange(partialBuffer);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }

                    string requestAsString = Encoding.UTF8.GetString(data.ToArray());   // Тук получаваме REQUEST-а под формата на STRING
                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine($"{request.Method} {request.Path} => {request.Headers.Count} headers"); // прочитаме REQUEST-a 

                    HttpResponse response;
                    if (this.routeTable.ContainsKey(request.Path))
                    {
                        var action = this.routeTable[request.Path]; 
                        response = action(request);
                    }
                    else
                    {
                        // Not Found 404
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    response.Headers.Add(new Header("Server", "SUS Server 1.0"));
                    response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

                    byte[] responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString()); // превръшаме го в BYTE-ове
 
                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length); // връшаме HEADER-a
                    await stream.WriteAsync(response.Body, 0, response.Body.Length); //  връщаме BODY-то
                }

                tcpClient.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
  