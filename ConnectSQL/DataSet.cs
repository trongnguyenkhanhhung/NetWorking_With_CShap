using System.Data.SqlTypes;
using System.Collections.ObjectModel;
using System.Data;
using System;
using System.Data.Common;
using System.Data.SqlClient;
namespace ConnectSQL
{
    public class DataSett
    {
        public void Su_Dung_DataSet()
        {
            var stringBuilder = new DbConnectionStringBuilder();
            stringBuilder["server"]= @"DESKTOP-OJA04UG\SQLEXPRESS";
            stringBuilder["database"] = "LICHBAYNHANVIEN";
            stringBuilder["UID"] = "sa";
            stringBuilder["Trusted_Connection"] = "true";
            
            string sqlConnect = stringBuilder.ToString();
            
            SqlConnection conn = new SqlConnection(sqlConnect);
            conn.Open();

            var dataset = new DataSet();
        
            var table = new DataTable("MyTable");   // Khởi tạo một đối tượng table 
            dataset.Tables.Add(table);

            // Tiến hành thiết kế cấu trúc của bảng
            table.Columns.Add("STT");
            table.Columns.Add("Hoten");
            table.Columns.Add("Tuoi");

            //Thêm các dữ liệu cho dòng
            table.Rows.Add(1, "Nguyen Van A", 21);
            table.Rows.Add(2, "Nguyen Van B", 22);
            table.Rows.Add(3, "Nguyen Van C", 23);

           ShowDataTable(table);


           // Sử dụng Adapter
           var adapter = new SqlDataAdapter();      // Tạo một đối tượng adapter để ánh xạ xuống dữ liệu database
           adapter.TableMappings.Add("Table", "NhanVien");  
           //select command giống mấy bài trước         --> // tiến hành lấy dữ liệu và đổ vào dataSet
           adapter.SelectCommand = new SqlCommand("select maNV, ten, dThoai from NhanVien", conn);

           //insert command
           adapter.InsertCommand = new SqlCommand("insert into NhanVien (maNV, ten, dThoai) values (@maNV, @ten, @dThoai)", conn);
           adapter.InsertCommand.Parameters.Add("@maNV", SqlDbType.Char, 4, "maNV");   // Lấy dữ liệu từ cột maNV của dataTable
           adapter.InsertCommand.Parameters.Add("@ten",SqlDbType.Char, 15, "ten");
           adapter.InsertCommand.Parameters.Add("@dThoai", SqlDbType.Char, 12, "dThoai");

            // delete command
            adapter.DeleteCommand = new SqlCommand("delete NhanVien where maNV = @manv", conn);
            var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@manv", SqlDbType.Char));    // Thêm thông số có kiểu dlieu là Char(4)
            pr1.SourceColumn = "maNV";  // Nguồn để lấy dữ liệu xóa Nhân viên
            pr1.SourceVersion = DataRowVersion.Original;  // lấy phiên bản nào của dữ liệu cập nhật

            // Update command
            adapter.UpdateCommand = new SqlCommand("update NhanVien set ten = @ten, dThoai = @dThoai where maNV = @manv", conn);
            var pr2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@manv", SqlDbType.Char));
            pr2.SourceColumn = "maNV";
            pr2.SourceVersion = DataRowVersion.Original;
            // Dữ liệu update
            adapter.UpdateCommand.Parameters.Add("@ten", SqlDbType.Char, 15, "ten"); 
            adapter.UpdateCommand.Parameters.Add("@dThoai", SqlDbType.Char, 12, "dThoai");

           var dataSet2 = new DataSet();
           adapter.Fill(dataSet2);  // Lúc này trong dataSet2 sẽ chứa dữ liệu bảng NhânVien
           DataTable table2 = dataSet2.Tables["NhanVien"];   // Đổ dữ liệu sang datatable để xuất ra màn hình

           ShowDataTable(table2);

           // Tiến hành tạo dòng mới và thêm dữ liệu KHI NÀO INSERT THÌ MỞ COMMENT
        //    var row = table2.Rows.Add();
        //    row["maNV"] = "1011";    
        //    row["ten"] = "Vy";
        //    row["dThoai"] = "87966578";


        // Xóa Nhân Viên    KHI NÀO XÓA THÌ MỞ COMMENT
        // var row8 = table2.Rows[8];
        // row8.Delete();


        // Update NhanVien    KHI NÀO UDATE THÌ MỞ COMMENT
        // var row7 = table2.Rows[7];
        // row7["ten"] = "TrongNguyen";
        // adapter.Update(dataSet2);

        }

        public void ShowDataTable(DataTable table)
        {
             Console.WriteLine("Ten cua table: "+ table.TableName);
            Console.Write("Index", 15);
            int index =0;
            int number_columns = table.Columns.Count;
            // Lấy ra tên của các cột
            foreach(DataColumn c in table.Columns)
            {
                Console.Write($"{c.ColumnName, 15}");
            }
            Console.WriteLine();
            // Lấy ra dữ liệu các dòng (sử dụng chỉ số hoặc tên chỉ số)
            foreach(DataRow r in table.Rows)
            {
                Console.Write(index);
                for(int i=0; i<number_columns;i++)
                {
                    Console.Write($"{r[i], 15}");
                    // Console.Write($"{r["STT"]} {r["Hoten"] {r["Tuoi"]}}");
                }
                index++;
                Console.WriteLine();
            }
        }
    }
}