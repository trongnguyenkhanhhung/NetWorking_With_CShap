using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ConnectSQL
{
    public class Su_Dung_ConnectSQL
    {
        public void SqlConnection()
        {
             var stringBuilder = new DbConnectionStringBuilder();
                stringBuilder["server"] = @"DESKTOP-OJA04UG\SQLEXPRESS";
                stringBuilder["database"] = "LICHBAYNHANVIEN";
                stringBuilder["UID"] = "sa";
                stringBuilder["Trusted_Connection"]= "True";
                string sqlStringConnect = stringBuilder.ToString();
             // Địa chỉ SQL, (Cổng truy cập); Tên database; User; Passwork;  
            //string sqlStringConnect = @"Data Source = DESKTOP-OJA04UG\SQLEXPRESS; Initial Catalog = LICHBAYNHANVIEN; User ID=sa;Trusted_Connection=true";
            using var connection = new SqlConnection(sqlStringConnect);

            // Các trạng thái kết nối: Close 0, Open 1, Connecting 2 

            Console.WriteLine(connection.State);
            connection.Open();
            Console.WriteLine(connection.State);

            using DbCommand command = new SqlCommand();
            command.Connection = connection;

            #region Dùng select bình thường
            //     ===== DÙNG EXECUTEREADER  ======
            // command.CommandText = "select ten, dThoai from NHANVIEN";
            // using var dataReader = command.ExecuteReader();      // --> Dùng khi kết quả trả về có nhiều dòng
            // while(dataReader.Read())
            // {
            //     string tennv = dataReader["ten"].ToString();
            //     string dt = dataReader["dThoai"].ToString();
            //     Console.WriteLine($"Ten NV: {tennv}  SDT: {dt}");
            // }



            //Cách 2:
            // using var dataReader = command.ExecuteReader();  
            // var dataTable = new DataTable();
            // dataTable.Load(dataReader);
            // dataTable.Columns();    //| rows  lấy dữ liệu từ dòng hoặc cột




            //      ===== DÙNG EXECYTESCALAR --> Trả về dữ liệu ở dòng 1, cột 1 thuộc dạng kiểu Obejct, thích hợp cho trả về SL dùng count()  ====== 
            // command.CommandText = "select count(1) from NHANVIEN";
            // //command.CommandText = "select ten, dThoai from NHANVIEN";
            //   var returnvalue = command.ExecuteScalar(); 
            //   Console.WriteLine(returnvalue);
            // // #endregion





            // // #region Dùng Store Procedure
            command.CommandText = "[dbo].[Tim_NV]";
            command.CommandType = CommandType.StoredProcedure;
            var MaNV = new SqlParameter("MaNV", "1005");
            command.Parameters.Add(MaNV);
            var ResultReader = command.ExecuteReader();
            if(ResultReader.HasRows)
            {  
                ResultReader.Read();
                Console.WriteLine($"Ten NV: {ResultReader["ten"].ToString()}  SDT: {ResultReader["dThoai"].ToString()}");
            }
            #endregion
            //Console.WriteLine(returnvalue);
            connection.Close();
        }
    }
}