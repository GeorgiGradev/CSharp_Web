using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static async Task Main(string[] args)  //Main meyhod-а го правим ASYNC
        {

            HttpClient httpClient = new HttpClient(); //създаваме HttpClient, за можем да работим с мрежата
            string url = "https://softuni.bg"; // Адресът, към който отиваме
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url); //поискваме търсената иформация
            string result = await httpResponse.Content.ReadAsStringAsync(); // получаваме поисканата инфоормация
            Console.WriteLine(result);  // тук се печата резултата


            Console.WriteLine("serfsedgsgsgrt");  // този код е ВЪЗМОЖНО да се изпълни преди резултата от предишния
        }
    }
}
