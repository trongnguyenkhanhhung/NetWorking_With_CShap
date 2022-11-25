// using System.Text;
// using System.Net;
// using System;
// using Newtonsoft.Json;
// using System.IO;
// using System.Threading.Tasks;
// using System.Linq;

// namespace _HttpListener
// {
//     public class Pritical
//     {
//         public class MyHttpServer
//         {
//             // Tạo nội dung HTML trả về cho Client (HTML chứa thông tin về Request)
           
//             private HttpListener listener;
//             public MyHttpServer(string[] prefixes)//Mảng chuỗi này chứa các câu hình để khởi tạo nên Listener
//             {
//                 if (!HttpListener.IsSupported)   //Kiểm tra xem HttpListener có hỗ trợ hay không
//                 {
//                     throw new System.Exception("HttpListener is not supported");
//                 }
//                 listener = new HttpListener();    //Nếu có hỗ trợ thì tạo ra cổng HttpListener
//                 //Sau đó duyệt qua tất cả các prifiexes để cấu thành nên địa chỉ
//                 foreach (string prefix in prefixes)
//                 {
//                     listener.Prefixes.Add(prefix);
//                 }
//             }
//             public async Task Start() //Phương thức cho phép chạy server
//             {
//                 // Bắt đầu lắng nghe kết nối HTTP
//                 listener.Start();
//                 Console.WriteLine("Server Start...");
//                 do
//                 {
//                     var context = await listener.GetContextAsync();   //Phương thức này chờ kết nối từ client đến server

//                     await ProcesRequest(context);

//                     //Viết ra dòng chờ có một client kết nối và lấy thời gian kết nối
//                     Console.WriteLine(DateTime.Now.ToLongTimeString() + "Waiting a client conneted...");

//                 } while (listener.IsListening);
//             }

//             //Phương thức xử lí các response của server trả về cho client
//             public async Task ProcesRequest(HttpListenerContext context)
//             {
//                 //tạo một đối tượng của client chứa dữ liệu như địa chỉ Url, dữ liệu Post gửi cho server
//                 HttpListenerRequest request = context.Request;

//                 //tạo một đối tượng của server chứa dữ liệu phản hồi lại cho client
//                 HttpListenerResponse response = context.Response;

//                 //Viết ra thông báo truy cập đến bằng phương thức gì Get, Post, Pup: request.HttpMethod
//                 //Địa chỉ Url khi client truy cập đến: request.RawUrl
//                 //Đường dẫn cảu tài nguyên mà request này nó yêu cầu requesr.Url.AbsolutePath
//                 Console.WriteLine($"{request.HttpMethod} | {request.RawUrl} | {request.Url.AbsolutePath}");

//                 //XỬ LÍ NỘI DUNG TRẢ VỀ CHO CLIENT
//                 var outputStream = response.OutputStream; // Lấy stream / gửi dữ liệu về cho client

//                 switch (request.Url.AbsolutePath)
//                 {
                   
//                     case "/":
//                         {
//                             // Gửi thông tin về cho Client
//                             var buffer = Encoding.UTF8.GetBytes("Xin chao cac ban");  //Convert một chuỗi thành mảng bytes
//                             response.ContentLength64 = buffer.Length; //Thiết lập độ dài của content để cho client biết 
//                             await outputStream.WriteAsync(buffer, 0, buffer.Length);  //Đổ toàn bộ mảng bytes vào outputStream

//                         }
//                         break;
//                     case "/json":
//                         {
//                             response.Headers.Add("content-Type", "application/json");
//                             var product = new
//                             {
//                                 Name = "Macbook Pro",
//                                 Price = 2000
//                             };

//                             var json = JsonConvert.SerializeObject(product);  //Convert một object thành chuỗi dạng json

//                             var buffer = Encoding.UTF8.GetBytes(json);
//                             response.ContentLength64 = buffer.Length;
//                             await outputStream.WriteAsync(buffer, 0, buffer.Length);

//                         }
//                         break;
//                     case "/anh2.png":
//                         {
//                             response.Headers.Add("content-Type", "image/png");
//                             var buffer = File.ReadAllBytes("D://temp//tem.png");
//                             response.ContentLength64 = buffer.Length;
//                             await outputStream.WriteAsync(buffer, 0, buffer.Length);

//                         }
//                         break;
//                         case "/stop":
//                         {
//                             listener.Stop();
//                             Console.WriteLine("Server Stop...");
//                         }break;
//                         default:
//                         {
//                             response.StatusCode=(int)HttpStatusCode.NotFound;
//                             response.Headers.Add("content-Type", "text/html");
//                             var buffer=Encoding.UTF8.GetBytes("NOT FOUND!");
//                             response.ContentLength64=buffer.Length;
//                             await outputStream.WriteAsync(buffer, 0, buffer.Length);

//                         }break;
//                 }
//                 outputStream.Close();//Khi đóng thì kết nối sẽ được đóng và client sẽ nhận được thông tin


//             }
//         }

//         public async Task Demo()
//         {
//             Console.OutputEncoding=Encoding.UTF8;
//             MyHttpServer server = new MyHttpServer(new string[] { "http://127.0.0.1:8081/" });
//             await server.Start();
//         }
//     }
// }