using System.Net;
using System.Linq;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.IO;
namespace URI_URL_URN
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Su_dung_Lop.Demo();


            //string url = "https://www.google.com/search?q=xuanthulab";
            //Do phương thức GetWebContent trả về cũng là bất đồng bộ nên ta cần sử dụng await
            //var task = Truy_van_Https.GetWebCotent(url);
            //cách 1: 
            // task.Wait();
            // var html=task.Result;
            // Console.WriteLine(html);

            //cách 2: chuyển hàm Main thành phương thức bấ đồng bộ 
            // var html = await Truy_van_Https.GetWebCotent(url);
            // Console.WriteLine(html);


    #region - Sử dụng phương thức ReadAsByteArrayAsync
                //string url2="https://raw.githubusercontent.com/xuanthulabnet/jekyll-example/master/images/jekyll-01.png";

                // var bytes=await Truy_van_Https.DowloadDataBytes(url2);

                // string path=@"D:/temp/tem.png";
                // using var stream=new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                // stream.Write(bytes, 0, bytes.Length);


                // await Truy_van_Https.Downloadstream(url2, "2.png");
                // Console.WriteLine("Hello World!");
    #endregion

            await Su_dung_SendAsync.Demo2();
           
        }
    }
}
