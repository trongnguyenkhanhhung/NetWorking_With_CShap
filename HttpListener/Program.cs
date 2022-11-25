using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;

namespace _HttpListener
{
    class Program
    {
        static async Task Main(string[] args)
         {
             if (HttpListener.IsSupported)
            {
                Console.WriteLine("Supported HttpListener");
            }
            else
            {
                Console.WriteLine("Not Suppor HttpListener");
                throw new Exception("Not Suppported HttpListener");
            }
            // Co_ban cb=new Co_ban();
            // await cb.Demo();

            // Pritical p=new Pritical();
            // await p.Demo();
        }
    }
}
