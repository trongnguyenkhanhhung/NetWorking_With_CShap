using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace URI_URL_URN
{
    public static class Su_dung_SendAsync
    {
        public static async Task Demo1()
        {
            using HttpClient httpclient = new HttpClient();//Khởi tạo httpclient

            var httpMessageRequest = new HttpRequestMessage();    //Yêu cầu truy vấn 

            httpMessageRequest.Method = HttpMethod.Get;  //lựa chọn phương thức truy vấn http: GET, POST, PATCH, PUT
            httpMessageRequest.RequestUri = new Uri("https://www.google.com.vn");   //Thiết lập địa chỉ truy vấn 
            httpMessageRequest.Headers.Add("User-Age", "Mozilla/5.0");  //thiết lập Headers, ngoài ra ta còn có thể thiết lập thêm nhiều phần khác

            // Và sau đó ta có thể gửi httpMessageRequest này lên server và server sẽ trả về httpMessageReponse
            var httpResponseMessage = await httpclient.SendAsync(httpMessageRequest);    //Tiến hành gởi thông tin và nhận phản hồi từ server

            //Ta có thể gọi phương thúc ShowHeaders
            Truy_van_Https.ShowHeader(httpResponseMessage.Headers);

            var html = await httpResponseMessage.Content.ReadAsStringAsync();   //Đọc dữ liệu từ server trả về
            Console.WriteLine(html);
        }

        
        public static async Task Demo2()
        {
            using HttpClient httpclient=new HttpClient();   //Khởi tạo httpclient

            var httpMessageRequest=new HttpRequestMessage();    //Yêu cầu truy vấn

            httpMessageRequest.Method=HttpMethod.Post;  //Lựa chong phương thức truy vấn

            httpMessageRequest.RequestUri=new Uri("https://postman-echo.com/post");     //Thiết lập địa chỉ truy vấn
            httpMessageRequest.Headers.Add("User-Age", "Mozilla/5.0");


    #region CÁCH UPLOAD DỮ LIỆU LÊN SERVER CÁCH 1: 
            //httpMessageRequest.Content  => FormatException html, File,...
            //Post =>  FORM HTML
            /*
                key1=> value1                       tương tự [INPUT] trong html
                key2=> [value2-1, value2-2]                 [Multi Select] trong html
            */

            // var parameters =new List<KeyValuePair<string, string>>();
            // parameters.Add(new KeyValuePair<string, string>("key1", "value1"));
            // parameters.Add(new KeyValuePair<string, string>("key2", "value2-1"));
            // parameters.Add(new KeyValuePair<string, string>("key2", "value2-2"));

            // var conten=new FormUrlEncodedContent(parameters);

            // httpMessageRequest.Content=conten;
    #endregion 

    #region CÁCH 2: 
            string data=@"{
                ""key1"": ""giatri1"",
                ""key2"": ""giatri2""
            }";

            Console.WriteLine(data);
            var content=new StringContent(data, Encoding.UTF8, "application/json");

            httpMessageRequest.Content=content;
    #endregion

    #region CÁCH 3: Có thể gửi nhiều dạng dữ liệu lên server
            // var contentt=new MultipartFormDataContent();
            
            // //  UPLOAD file 1.txt
            // Stream filestream=File.OpenRead("1.txt");   //Mở file để đọc
            // var fileUpload=new StreamContent(filestream);   //Tạo nội dung đính kèm vào httpRequest
            // contentt.Add(fileUpload, "fileuplad", "abc.xyz");

            // //UPLOAD CHUỖI
            // contentt.Add(new StringContent("value1"), "key1");  
            // httpMessageRequest.Content=contentt;
            

    #endregion


            var httpResponseMessage = await httpclient.SendAsync(httpMessageRequest); //Tiến hành gởi thông tin và nhận phản hồi từ server 

            var html = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(html);

        }
    }
}