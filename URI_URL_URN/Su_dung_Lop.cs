using System.Net;
using System.Linq;
using System;
using System.Net.NetworkInformation;
namespace URI_URL_URN
{
    public static class Su_dung_Lop
    {
        public static void Demo()
        {
            // //Lấy ra các thông tin về Host, Scheme, Fragment, Query
            // string url = "https://xuanthulab.net/lap-trinh/csharp/?page=3#acff";
            // var uri=new Uri(url);
            // var uritype=typeof(Uri);
            // uritype.GetProperties().ToList().ForEach(property=>
            // {
            //     Console.WriteLine($"{property.Name, 15} {property.GetValue(uri)}");
            // });
            // Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");



            //Lấy ra HostName của máy Local
            var hotname=Dns.GetHostName();
            Console.WriteLine(hotname);

            
            string url1="https://www.bootstrapcdn.com/";
            var urii=new Uri(url1);
            Console.WriteLine(urii.Host);//in ra tên host của đường link

            var iphostentry=Dns.GetHostEntry(urii.Host);//lấy ra iphostentry
            Console.WriteLine("Name: "+iphostentry.HostName);
            //Một tên miền có thể trỏ đến nhiều server
            iphostentry.AddressList.ToList().ForEach(ip=>   //lấy ra các địa chỉ ip trỏ đến trang web đó
            {
                Console.WriteLine(ip);
            });

            // //Lớp Ping (System.Net.NetworkInformation.Ping), lớp này cho phép ứng dụng xác định một
            // // máy từ xa (như server, máy trong mạng ...) có phản hồi không.
            //using System.Net.NetworkInformation;
            var ping =new Ping();
            // string domain="google.com.vn";
            var pingReply=ping.Send("216.24.57.253");//tham số của Send có thể là domain hoặc là ip

            Console.WriteLine(pingReply.Status);
            if(pingReply.Status==IPStatus.Success)
            {
                Console.WriteLine(pingReply.RoundtripTime);
                Console.WriteLine(pingReply.Address);
            }
        }
    }
}