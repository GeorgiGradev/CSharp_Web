using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest // клас, който съхранява информацията, която е дошла от клиента
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            string[] lines = requestString.Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);

            // GET /somepage HTTP/1.1
            string headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1; 
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();

            while(lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }


                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }

            if (this.Headers.Any(x=>x.Name == HttpConstants.RequestCookieHeader))
            {
                string cookiesAsString = this.Headers.FirstOrDefault(x => x.Name == HttpConstants.RequestCookieHeader).Value;
                string[] cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }
            }


            this.Body = bodyBuilder.ToString(); 
        }
         
        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}
