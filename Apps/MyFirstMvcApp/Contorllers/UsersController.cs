using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFirstMvcApp.Contorllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
            
        }
        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
            
        }

        public HttpResponse DoLogin(HttpRequest arg)
        {
            //TODO: Read data, checked user , log user, 
            return this.Redirect("/");

        }
    }
}
