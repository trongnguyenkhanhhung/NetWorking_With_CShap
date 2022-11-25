
using System;

namespace Dependency_invesion
{

    public class Hor
    {
        int lever=0;
        public Hor(int _lever)=>lever=_lever;
        public void Beep()=>Console.WriteLine("acbsajkhasd");
    }
    public class Car
    {
        public Hor hor;
        public Car(Hor h)
        {
            hor=h;
        }
        public void Beep()
        {
            hor.Beep();
        }
        
    }
    public class Co_ban
    {
        public void Demo()
        {
            //Class B là dependency của class A bởi vì cần có class B thì class A mới hoàn thành tách vụ được
            //Inversion of Control (IoC) - Dependency Inversion (Đảo ngược điều khiển): Các thành phẩn nó dựa vào để làm việc bị đảo ngược quyền điều khiển 
            //khi so sánh với lập trình hướng thủ tục truyền thống
            IClassC1 c = new ClassC2();
            IClassB1 b = new ClassB2(c);
            ClassA a = new ClassA(b);
            a.ActionA();

            Car car=new Car(new Hor(7));
            car.Beep();

            //ServiceCollection sevice=new SevriceCollection();
        }
    }

    public interface IClassC1
    {
        public void ActionC();
    }
    public interface IClassB1
    {
        public void ActionB();
    }
    public class ClassC2: IClassC1
    {
        public void ActionC()
        {
            Console.WriteLine("Action in CLassC2");
        }
    }
    public class ClassB2:IClassB1{
        public IClassC1 c_dependency;
        public ClassB2(IClassC1 c)
        {
            c_dependency=c;
            Console.WriteLine("Created is CLassB2");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB2");
            c_dependency.ActionC();
        }
    }
    public class ClassA
    {
        IClassB1 b_dependency;
        public ClassA(IClassB1 b)
        {
            b_dependency = b;
            Console.WriteLine("Created is ClassA");
        }
        public void ActionA()
        {
            Console.WriteLine("Action ClassA");
            b_dependency.ActionB();
        }
    }
    public class ClassB : IClassB1
    {
        IClassC1 c_dependency;
        public ClassB(IClassC1 c)
        {
            Console.WriteLine("Created is ClassB");
            c_dependency = c;
        }
        public void ActionB()
        {
            Console.WriteLine("Action ClassB");
            c_dependency.ActionC();
        }
    }
    public class ClassC : IClassC1
    {
        public ClassC() => Console.WriteLine("Created is ClassC");
        public void ActionC()
        {
            Console.WriteLine("Action ClassC");
        }
    }
}