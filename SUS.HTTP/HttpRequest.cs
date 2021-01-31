using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headres = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString.Split(new string[] { HttpConstants.NewLine },StringSplitOptions.None);
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod),headerLineParts[0],true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
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
                    this.Headres.Add(new Header(line));
                }
                else
                { 
                    //read body
                    bodyBuilder.AppendLine(line);
                }

               

            }

            if (this.Headres.Any(x=>x.Name=="Cookie"))
            {
                var cookiesAsString = this.Headres.FirstOrDefault(x => x.Name == "Cookie").Value;
                var cookies = cookiesAsString.Split(new string[] { "; " },StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookiesAsString));
                }
            }

            this.Body = bodyBuilder.ToString();
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headres { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}
