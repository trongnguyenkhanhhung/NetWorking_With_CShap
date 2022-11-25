using System;
using System.Threading.Tasks;

namespace _HttpMessageHandler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // SocketsHandler sk=new SocketsHandler();
            // await sk.Demo();
            Su_dung_DelegatingHandler d = new Su_dung_DelegatingHandler();
            await d.Demo();
            Console.WriteLine("Hello World!");
        }
    }
}
