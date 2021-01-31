using MyFirstMvcApp.Contorllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {      
            await Host.CreateHostAsync(new StartUp(),80);
        }

        

        
       

        
    }
}
