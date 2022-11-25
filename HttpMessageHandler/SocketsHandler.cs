using System.Net;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace _HttpMessageHandler
{
    public class SocketsHandler
    {
        public async Task Demo()
        {
            string url = "https://postman-echo.com/post";

            var cookies = new CookieContainer();  //các UseCookies được lưu vào đây

            using var handler = new SocketsHttpHandler(); //Khởi tạo một đói tượng SocketHttpHandler
            //Thiết lập các thuộc tính của handler
            handler.AllowAutoRedirect = true; //Cho phép tự động chuyển hướng khi mã trả về là 301, chuyển hướng sang Url mới
            handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;  //Cho phép giải nén
            handler.UseCookies = true;    //cho phép lưu Cookies
            handler.CookieContainer = cookies;    //Lưu Các UseCookies

            HttpClient httpclient = new HttpClient(handler);    //truyền tham số cho httpclient

            using var httpRequestMessage = new HttpRequestMessage();    //thiết lập yêu cầu truy vấn
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Headers.Add("User-Age", "Mozilla/5.0");

            var parameter = new List<KeyValuePair<string, string>>();   //Tạo nội dung để truy vấn
            parameter.Add(new KeyValuePair<string, string>("key1", "value1"));
            parameter.Add(new KeyValuePair<string, string>("key2", "value2.1"));
            parameter.Add(new KeyValuePair<string, string>("key2", "value2.2"));
            //var content=new FormUrlEncodedContent(parameter);
            httpRequestMessage.Content = new FormUrlEncodedContent(parameter);

            HttpResponseMessage response = await httpclient.SendAsync(httpRequestMessage);

            //Duyệt qua các Cookies xem server có trả về các cookies hay không
            //Phương thức này trả về các danh sách các cookies
            // Hiển thị các cookie (các cookie trả về có thể sử dụng cho truy vấn tiếp theo)
            cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie =>
            {
                Console.WriteLine($"NameCookies: {cookie.Name} | ValueCookies: {cookie.Value}\n");
            });
            var html = await response.Content.ReadAsStringAsync();
            Console.WriteLine(html);
        }
    }
}