using System.Text;
using System.Data.Common;
using System.Runtime.InteropServices;
using System;
using System.Data.SqlClient;
using System.Data;

namespace ConnectSQL
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.Unicode;
            
            //DataSett db = new DataSett();
            //db.Su_Dung_DataSet();
            DataSet2 db2 = new DataSet2();
            db2.Su_Dung_DataSet2();
        }
    }
}