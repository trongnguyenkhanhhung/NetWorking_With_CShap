
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Microsoft.CSharp;
using Microsoft.VisualBasic;
namespace Dependency_invesion
{
    public class File_cauhinh_DI
    {
        public void Demo()
        {
            
            IConfigurationRoot configurationRoot;

            ConfigurationBuilder confiBuilderr = new ConfigurationBuilder();

            confiBuilderr.SetBasePath(Directory.GetCurrentDirectory());
            
            confiBuilderr.AddJsonFile("cauhinh.json");

            configurationRoot = confiBuilderr.Build();

            // var a=configurationRoot.GetSection("section").GetSection("data1").Value;
            // Console.WriteLine(a);


            //tên biến trong Myservice và tên biến trong MyserviceIOption và tên biến trong file json phải giống nhau data1, data2
            var sectionMyserviceOption = configurationRoot.GetSection("MyserviceOptions"); //Dọc dữ liệu
            var service = new ServiceCollection();
            service.AddSingleton<Myservice>();
            service.Configure<MyserviceIOPtions>(sectionMyserviceOption);
            var provider = service.BuildServiceProvider();
            var a = provider.GetService<Myservice>();
            a.Pints();

        }
    }
}