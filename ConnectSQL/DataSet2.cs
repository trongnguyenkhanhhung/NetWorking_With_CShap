using System.Reflection;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
namespace ConnectSQL
{
    public class DataSet2
    {
        public void Su_Dung_DataSet2()
        {
            Console.InputEncoding = Encoding.Unicode;

            var stringBuilder = new DbConnectionStringBuilder();
            stringBuilder["server"] = @"DESKTOP-OJA04UG\SQLEXPRESS";
            stringBuilder["database"] = "QuanLySinhVien";
            stringBuilder["UID"] = "sa";
            stringBuilder["Trusted_Connection"] = "True";
            var connection = stringBuilder.ToString();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            var dataset = new DataSet();
            var table =  new DataTable("MyTable");

            dataset.Tables.Add(table);
            table.Columns.Add("STT");
            table.Columns.Add("HoTen");
            table.Columns.Add("Tuoi");

            table.Rows.Add(1, "Nguyen", 20);
            table.Rows.Add(2, "Dan", 22);
            table.Rows.Add(3, "Vy", 23);
            ShowDataTable(table);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.TableMappings.Add("Table", "QLSV"); // Lúc này table QLSV sẽ tồn tại trong dataset nên khi gọi ta cần gọi nó qua key

            // SelectCommand
            adapter.SelectCommand = new SqlCommand("select * from SinhVien", conn);

            // InsertCommand
            adapter.InsertCommand = new SqlCommand("insert into SinhVien values (@masv, @tensv, @namsinh, @dantoc, @malop)", conn);
            adapter.InsertCommand.Parameters.Add("@masv", SqlDbType.VarChar, 10, "MaSV");
            adapter.InsertCommand.Parameters.Add("@tensv", SqlDbType.NVarChar, 100, "Ho_TenSV");
            adapter.InsertCommand.Parameters.Add("@namsinh", SqlDbType.Int, 4, "Nam_Sinh");
            adapter.InsertCommand.Parameters.Add("@dantoc", SqlDbType.NVarChar, 20, "Dan_Toc");
            adapter.InsertCommand.Parameters.Add("@malop", SqlDbType.VarChar, 10, "Ma_Lop");

            // UpdateCommand
            adapter.UpdateCommand = new SqlCommand(
                "update SinhVien set Ho_TenSV = @hoten, Nam_Sinh = @namsinh, Dan_Toc = @dantoc, Ma_Lop = @malop where MaSV = @masv",
                 conn);
            var pr = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@masv", SqlDbType.VarChar));
            pr.SourceColumn = "MaSV";
            pr.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand.Parameters.Add("@hoten", SqlDbType.VarChar, 10, "Ho_TenSV");
            adapter.UpdateCommand.Parameters.Add("@namsinh", SqlDbType.Int, 4, "Nam_Sinh");
            adapter.UpdateCommand.Parameters.Add("@dantoc", SqlDbType.VarChar, 20, "Dan_Toc");
            adapter.UpdateCommand.Parameters.Add("@malop", SqlDbType.VarChar, 10, "Ma_Lop");


            // Delete Command
            adapter.DeleteCommand =  new SqlCommand("delete SinhVien where MaSV = @masv", conn);
            var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@masv", SqlDbType.VarChar));
            pr1.SourceColumn = "MaSV";
            pr1.SourceVersion = DataRowVersion.Original;

            DataSet dataset2 = new DataSet();
            adapter.Fill(dataset2);
            DataTable table2 = dataset2.Tables["QLSV"];     // Gọi table thông qua key
            ShowDataTable(table2);


            // Thêm sinh viên mới
            // Cách 1: 
            //var newsv = table2.Rows.Add("20113021", "Nguyễn Như Hảo", 2002,  "Kinh", "TH2002/01");
            // Cách 2: 
            // var newsv2 = table2.Rows.Add();
            // newsv2["MaSV"] = "20113022";
            // newsv2["Ho_TenSV"] = "Nguyễn Như Ý";
            // newsv2["Nam_Sinh"] = 2003;
            // newsv2["Dan_Toc"] = "Kinh";
            // newsv2["Ma_Lop"] = "VL2003/01";
            

            // Update SinhVien
            // var sv1 = table2.Rows[6];
            //  sv1["Ho_TenSV"] = "Nguyễn Như Hảo";
            //  sv1["Nam_Sinh"] = 2004;
            //  sv1["Dan_Toc"] = "Muong";
            // //  sv1["Ma_Lop"] = "VL2003/01";



            // Delete Sinh Vien
            // var sv2 = table2.Rows[6];
            // sv2.Delete();


            adapter.Update(dataset2);
            conn.Close();
        }

        public void ShowDataTable(DataTable table)
        {
            Console.WriteLine("Ten bang: "+ table.TableName);
            Console.Write("Index");
            foreach(DataColumn c in table.Columns)
            {
                Console.Write($"{c, 15}");
            }
            Console.WriteLine();
            int index =0;
            int number_columns = table.Columns.Count;
            foreach(DataRow r in table.Rows)
            {
                Console.Write(index);
                for(int i=0;i< number_columns; i++)
                {
                    Console.Write($"{r[i], 15}");
                }
                index++;
                Console.WriteLine();
            }
        }
    }
}