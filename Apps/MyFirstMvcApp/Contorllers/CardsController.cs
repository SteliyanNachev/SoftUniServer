﻿using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMvcApp.Contorllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add(HttpRequest request)
        {
            return this.View();
        }
        public HttpResponse All(HttpRequest request)
        {
            return this.View();
        }
        public HttpResponse Collection(HttpRequest request)
        {
            return this.View();
        }
    }
}
