using System;
using Microsoft.Extensions.Options;
using  Microsoft.Extensions.DependencyInjection;
namespace Dependency_invesion
{
    public class Su_dung_IOptions
    {
        public void Demo()
        {
            var service =new ServiceCollection();
            service.AddSingleton<Myservice>();  //Đăng ký dependency
            service.Configure<MyserviceIOPtions>((MyserviceIOPtions option)=>
            {
                option.data1="Xin chao cac ban";
                option.data2="2323";
            });
            var provider=service.BuildServiceProvider();
            var a=provider.GetService<Myservice>();
            a.Pints();
        }
    }
    public class Myservice
    {
        public string data1 { get; set; }
        public string data2 { get; set; }
        public Myservice(IOptions<MyserviceIOPtions> options)
        {
            var _options=options.Value;
            data1=_options.data1;
            data2=_options.data2;
        }
        public void Pints()
        {
            Console.WriteLine($"{data1} : {data2}");
        }

    }
    public class MyserviceIOPtions
    {
        public string data1 { get; set; }
        public string data2 { get; set; }

    }
}