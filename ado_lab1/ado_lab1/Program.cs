using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ado_lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(
                "Data Source =.; Initial Catalog = MyDB;" +
                " Integrated Security = True; Trust Server Certificate = True"
                );

            //SqlCommand cmd = new SqlCommand("select * from department", con);
            //con.Open();

            //SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[0] + "  " + reader[1]);
            //}
            //con.Close();


            //task : make crud operations 
            //insert 
            string insert_sql = "insert into department  (Name) values ('Finance')";

            using (SqlCommand cmd2 = new SqlCommand(insert_sql, con))
            {
                con.Open();
                cmd2.ExecuteNonQuery();
                Console.WriteLine($"done inserted");
            }



            //reterive
            insert_sql = "select * from department";

            using (SqlCommand cmd2 = new SqlCommand(insert_sql, con))
            {
                using (SqlDataReader reader = cmd2.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        string name = (string)reader[1];

                        Console.WriteLine($"Id: {id}, Name: {name}");
                    }
                }
                con.Close(); 
            }

            //update
            insert_sql = "update  department set name = 'dot net' where id = 3 ";
            using (SqlCommand cmd2 = new SqlCommand(insert_sql, con))
            {
                con.Open();
                cmd2.ExecuteNonQuery();
                Console.WriteLine($"done updated");
                con.Close();

            }

            //delete
            insert_sql = "delete from  department where  id = 4";
            using (SqlCommand cmd2 = new SqlCommand(insert_sql, con))
            {
                con.Open();
                cmd2.ExecuteNonQuery();
                Console.WriteLine($"done deleted");
                con.Close();

            }


        }
    }
}
