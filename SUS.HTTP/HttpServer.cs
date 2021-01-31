using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        List<Route> routeTable;
        public HttpServer(List<Route> routeTable)
        {
            foreach (var item in routeTable)
            {
                this.routeTable = routeTable;
            }
        }

        
           
       

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback,port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    //TODO : Reading Request and  making it string
                    int position = 0;
                    byte[] buffer = new byte[4096];
                    //TODO : research for faster data structure for array of bytes
                    List<byte> data = new List<byte>();
                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;

                        if (count < buffer.Length)
                        {
                            var bufferWithData = new byte[count];
                            Array.Copy(buffer, bufferWithData, count);
                            data.AddRange(bufferWithData);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }

                    }

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());
                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine(request.Method + " " + request.Path + " " + request.Headres.Count + "headers");

                    HttpResponse response;
                    var route = this.routeTable.FirstOrDefault(x => string.Compare(x.Path, request.Path,true)==0
                            && x.Method == request.Method);
                    if (route != null)
                    {
                        response = route.Action(request);
                    }
                    else
                    {
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    response.Header.Add(new Header("Server", "SUS Server 1.0"));
                    var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);
                }
                tcpClient.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            
        }
    }
}
