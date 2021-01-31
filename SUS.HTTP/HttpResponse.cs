using System;
using System.Collections.Generic;
using System.Text;

namespace SUS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Header = new List<Header>();
            this.Cookies = new List<Cookie>();
        }
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode=HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new AccessViolationException(nameof(body));
            }
            this.StatusCode = statusCode;
            this.Body = body;
            this.Header = new List<Header>
            {
                {new Header("Content-Type",contentType)},
                {new Header("Content-Lenght",body.Length.ToString())}
            };
            this.Cookies = new List<Cookie>();
        }
        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);
            foreach (var header in this.Header)
            {
                responseBuilder.Append(header.ToString()+ HttpConstants.NewLine);
            }
            foreach (var cookie in Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }
            responseBuilder.Append(HttpConstants.NewLine);
            return responseBuilder.ToString();
        }

        public ICollection<Header> Header { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public byte[] Body { get; set; }

        public ICollection<Cookie> Cookies { get; set; }
    }
}
