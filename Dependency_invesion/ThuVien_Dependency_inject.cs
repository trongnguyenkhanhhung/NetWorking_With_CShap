
using  Microsoft.Extensions.DependencyInjection;
namespace Dependency_invesion
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    public class DK_inversion
    {
        public void Demo()
        {
            //chính là sử dụng Dependency Container: ServeiceCollection
            var service= new ServiceCollection();
            service.AddSingleton<ClassA, ClassA>(); 

            // service.AddSingleton<IClassB1, ClassB3>(pro=>
            // {
            //     var b3=new ClassB3(pro.GetService<IClassC1>(),"Thuc hien trong class B2");
            //     return b3;
            // });
            service.AddSingleton<IClassB1>(Created);

            service.AddSingleton<IClassC1, ClassC2>();

            var provider=service.BuildServiceProvider();

            //cách 1: Khi chúng ta lấy ra dịch vụ của classA thì nó sẽ tự động tạo ra đối tượng của ClassA và do nó truy cập đến Class B
            //nên nó cũng tự động tạo ra đối tượng của classB, và ClassB cũng truy cập đến ClassC nên nó cũng sẽ tự động tạo ra 
            // đối tượng của classC
            var a=provider.GetService<ClassA>(); //Gọi bao nhiêu lần nó cũng chỉ trả về một đối tượng do sử dụng AddSingleton
            a.ActionA();

            //cách 2: 
            // using (var scope =provider.CreateScope())   //Ra khỏi scope thì đối tượng sẽ bị hủy
            // {
            //     var provider1=scope.ServiceProvider;
            //     var a=provider1.GetService<ClassA>();
            //     a.ActionA();
            // }

        }
        //Sử dụng Factory
        public IClassB1 Created(IServiceProvider pro)
        {
            var b3 = new ClassB3(pro.GetService<IClassC1>(), "Thuc hien trong class B2");
            return b3;
        }
    }
    
    public class ClassB3 : IClassB1
    {
        public string mgf;
        public IClassC1 c_dependency;
        public ClassB3(IClassC1 c, string m)
        {
            mgf = m;
            c_dependency = c;
        }
        public void ActionB()
        {
            Console.WriteLine(mgf);
            Console.WriteLine("Action in ClassB3");
        }
    }
}