// using System.Text;
// using System.Threading.Tasks;
// using System;
// using System.Net;
// namespace _HttpListener
// {
//     public class Co_ban
//     {
//         public async Task Demo()
//         {
//             if (HttpListener.IsSupported)
//             {
//                 Console.WriteLine("Supported HttpListener");
//             }
//             else
//             {
//                 Console.WriteLine("Not Suppor HttpListener");
//                 throw new Exception("Not Suppported HttpListener");
//             }

//             var server = new HttpListener();

//             //server.Prefixes.Add("http://+:8888/");   //thiết lập Uri cho server, chấp nhận mọi kết nối, thông qua cổng 8080
//             server.Prefixes.Add("http://127.0.0.1:8081/");
//             server.Start(); //bắt đầu chạy server

//             Console.WriteLine("Server http Start...");
//             //Và các client kết nối đến server thông qua cổng 8080 thì nó sẽ chấp nhận và thực hiện lệnh bên dưới
//             do
//             {
//                 var context = await server.GetContextAsync();//phương thức này chờ và tạo kết nối từ client đến server
//                 Console.WriteLine("Client connected...");

//                 var response = context.Response;  //thông điệp HttpResponse trả về cho client
//                 var outputStream = response.OutputStream;    //Đây là thuộc tính viết dữ liệu ra và client sẽ nhận được

//                 response.Headers.Add("content-type", "text/html");//cho biết kiểu nội dung trả về là dạng text/html

//                 var html = "<h1>Hello Work</h1>";
//                 var bytes = Encoding.UTF8.GetBytes(html);   //convert một chuỗi thành mảng byte
//                 await outputStream.WriteAsync(bytes, 0, bytes.Length);    // Đổ tất cả mảng bytes ra ouputStream
//                 outputStream.Close();   //Sau khi viết ra thì ta cần đóng lại

//             } while (server.IsListening); //Server lắng nghe các kết nối từ client đến server được duy trì trong vòng lặp

//         }
//     }
// }