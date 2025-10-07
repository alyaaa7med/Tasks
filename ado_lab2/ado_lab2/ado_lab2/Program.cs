using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ado_lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Data Source=.;Initial Catalog=MyDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";


            SqlDataAdapter adapter;
            DataTable dt = new DataTable();

            //insert 
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                adapter = new SqlDataAdapter("select * from department", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(dt);

              
                DataRow dr = dt.NewRow();
                dr["Name"] = "Open Source";
                dt.Rows.Add(dr);
                adapter.Update(dt);
                Console.WriteLine("inserted to real db successfully");
            }

          
            //retrieve //no need to open conn
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Id"]}: {row["Name"]}");
            }

            //update 
            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["Id"] == 3) 
                {
                    row["Name"] = "Updated Open Source";
                    break;
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                adapter.Update(dt);
                Console.WriteLine("updated in real db successfully");
            }


            //delete 
            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["Id"] == 4) 
                {
                    row.Delete();
                    break;
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                adapter.Update(dt);
                Console.WriteLine("deleted from real db successfully");
            }
        }
    }
}
