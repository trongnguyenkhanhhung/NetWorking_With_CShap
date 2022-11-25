using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
namespace _HttpMessageHandler
{

    public class Su_dung_DelegatingHandler
    {
        public async Task Demo()
        {
            //string url="https://postman-echo.com/post";
            string url="https://Youtube.com";
            var cookies=new CookieContainer();

            var bottomHandler=new MyHttpClientHandler(cookies);
            var changUriHandler=new ChangeUri(bottomHandler);
            var denyAccessFacebook=new DenyAccessFacebook(changUriHandler);

            var httpclient=new HttpClient(denyAccessFacebook);

            HttpRequestMessage httpRequestMessage=new HttpRequestMessage();
            httpRequestMessage.Method=HttpMethod.Get;
            httpRequestMessage.RequestUri=new Uri(url);
            httpRequestMessage.Headers.Add("User-Age", "Mozilla/5.0");

             var parameter = new List<KeyValuePair<string, string>>();   //Tạo nội dung để truy vấn
            parameter.Add(new KeyValuePair<string, string>("key1", "value1"));
            parameter.Add(new KeyValuePair<string, string>("key2", "value2"));
            //var content=new FormUrlEncodedContent(parameter);
            httpRequestMessage.Content = new FormUrlEncodedContent(parameter);

            var response=await httpclient.SendAsync(httpRequestMessage);

            cookies.GetCookies(new Uri(url)).ToList().ForEach(c=>
            {
                Console.WriteLine($"{c.Name} : {c.Value}");
            });

            var html=await response.Content.ReadAsStringAsync();
            Console.WriteLine(html);
        }
    }
    public class MyHttpClientHandler : HttpClientHandler        //Lớp này là lớp thực hiện truy vấn
    {
        public MyHttpClientHandler(CookieContainer cookie_container)
        {
            CookieContainer = cookie_container;   //Thay thế Cookies mặc định
            AllowAutoRedirect = false;    //không cho tự động chuyển hướng (Rediract)
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            UseCookies = true;
        }
        //Phương thức SendAsunc bắt buộc ta phải nạp chồng vì nó được kế thừa từ HttpClientHandler
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //CancellationToken : Nghĩa là nếu ta truy cập vào Url ngoại lệ thì nó sẽ hủy bỏ các hoạt động đó
            Console.WriteLine("Bat dau ket noi: " + request.RequestUri.ToString());
            //Thực hiện truy vấn đến server
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("Hoan thanh tai du lieu");
            return response;
        }   
    }

    public class ChangeUri : DelegatingHandler
    {
        public ChangeUri(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var host = request.RequestUri.Host.ToLower();
            Console.WriteLine($"Check in  ChangeUri - {host}");
            if (host.Contains("google.com"))
            {
                // Đổi địa chỉ truy cập từ google.com sang github
                request.RequestUri = new Uri("https://github.com/");
            }
            // Chuyển truy vấn cho base (thi hành InnerHandler)
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class DenyAccessFacebook:DelegatingHandler
    {
        public DenyAccessFacebook(HttpMessageHandler innerHandler):base(innerHandler){}
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var host=request.RequestUri.Host.ToLower();
            Console.WriteLine("Check in DenyAccessFacebook: "+host);
            if(host.Contains("facebook.com"))
            {
                var response=new HttpResponseMessage(HttpStatusCode.OK);
                response.Content=new ByteArrayContent(Encoding.UTF8.GetBytes("Khong duoc truy cap"));
                return await Task.FromResult<HttpResponseMessage>(response);
            }
            //chuyển truy vấn cho base (thi hành innerHandler)
            return await base.SendAsync(request, cancellationToken);
        }
    }
}   