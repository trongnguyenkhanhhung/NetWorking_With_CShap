using System.IO;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace URI_URL_URN
{
    public static class Truy_van_Https
    {
        //using System.Net.Http.Headers;
        public static void ShowHeader(HttpResponseHeaders header)
        {
            Console.WriteLine("Cac header: ");
            foreach (var head in header)
            {
                Console.WriteLine($"{head.Key} : {head.Value}");
            }
        }



        // using System.Net.Http;
        // using System.Threading.Tasks;
        public static async Task<string> GetWebCotent(string url)   // LẤY NỘI DUNG CỦA MỘT TRANG WEB
        {
            //Khởi tạo http client, sử dụng using để đối tượng tự động hủy khi thoát ra khỏi HttpCLient()
            using var httpclient = new HttpClient();
            try
            {
                //Thiết lập thêm Header
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                //Từ Heaers này thì máy chủ sẽ biết trình duyệt hay client chúng ta đang truy cập nó là cái gì thì nó sẽ trả về nội dung tương ứng
                //Ta có thể thêm bao nhiêu Header cũng được, những Header này phụ thuộc vào máy chủ, máy chủ nó chấp nhận và phân tích những Header nào

                //Thực hiện truy vấn GET
                //Phương thức GetAsync trả về HttpResponseMessage, và là phương thức bất đồng bộ nên sử dụng await để trả về kết quả
                HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(url);

                //Ngoài nội dung httpResponeMesage nó trả về ta có thể đọc các thông tin Heard do nó trả về
                ShowHeader(httpResponseMessage.Headers);

                //Đọc nội dung trả về từ HttpResponseMessage
                string html = await httpResponseMessage.Content.ReadAsStringAsync();//cũng là phương thức bất đồng bộ nên phải sử dụng await
                return html;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Loi";
            }
        }

        // using System.Net.Http;
        // using System.Threading.Tasks;
        public static async Task<byte[]> DowloadDataBytes(string url)
        {
            using var httpclient = new HttpClient();  //B1: Khởi tạo HttpCLient

            try
            {
                //Thiết lập thêm Header
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                //Từ Heaers này thì máy chủ sẽ biết trình duyệt hay client chúng ta đang truy cập nó là cái gì thì nó sẽ trả về nội dung tương ứng
                //Ta có thể thêm bao nhiêu Header cũng được, những Header này phụ thuộc vào máy chủ, máy chủ nó chấp nhận và phân tích những Header nào

                HttpResponseMessage httpReponseMessage = await httpclient.GetAsync(url); //B2: Thực hiện truy vấn

                ShowHeader(httpReponseMessage.Headers);

                var bytes = await httpReponseMessage.Content.ReadAsByteArrayAsync();  //B3: Đọc nội dung truy vấn (content)
                return bytes;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public static async Task Downloadstream(string url, string filename)
        {
            HttpClient httpclient = new HttpClient(); //B1: Khởi tạo HttpCLient
            try
            {
                var htttpReponseMessage = await httpclient.GetAsync(url);    //B2: Thực hiện truy vấn

                using var stream = await htttpReponseMessage.Content.ReadAsStreamAsync();    //B3: Đọc nội dung truy vấn

                using var streamfile = File.OpenWrite(filename);//tạo một file khác để tiếp tục đọc nếu đọc chưa thành công

                int SIZEBUFFER = 500;//tạo ra bộ đệm có kích thước là 500byte
                var bytes = new byte[SIZEBUFFER];//Tạo ra một vùng đệm

                bool endread = false;
                do
                {
                    int numByte = await stream.ReadAsync(bytes, 0, SIZEBUFFER);   // phương thức ReadAsync trả về kiểu int 
                    //nếu nó trả về 0 byte tức là nó đang ở cuối stream, là đọc thành công, ngược lại thì ta phải tạo một file khác để đọc từ file 
                    // đang đọc hiện tại qua file đó
                    if (numByte == 0)
                    {
                        endread = true;
                    }
                    else
                    {
                        await streamfile.WriteAsync(bytes, 0, numByte);
                    }

                } while (!endread);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}