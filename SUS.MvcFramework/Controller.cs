using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
       
        public HttpResponse View([CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/shared/_Layout.cshtml");

            var viewContent = System.IO.File.ReadAllText("Views/" 
                +this.GetType().Name.Replace("Controller",string.Empty) 
                + "/" +viewPath + ".cshtml");

            var responseHtml = layout.Replace("@RenderBody()", viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }

        public HttpResponse File(string filePath, string contentType)
        {
            var fileByte = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileByte);
            return response;
        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Header.Add(new Header("Location", url));
            return response;
        }
    }
}
