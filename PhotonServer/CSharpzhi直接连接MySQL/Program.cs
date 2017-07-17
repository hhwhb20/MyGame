using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CSharpzhi直接连接MySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectStr = "server = 127.0.0.1;port=3306;database=mygame;user=root;password=fengxue_366";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string sql = "select * from users";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.ExecuteReader(); //执行一些查询
                //cmd.ExecuteNonQuery(); //插入，删除
                //cmd.ExecuteScalar();   //执行一些查询，返回一个单个的值

                MySqlDataReader reader = cmd.ExecuteReader();
//                 reader.Read();
//                 Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());

                while(reader.Read())
                {
                    Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                }

                Console.WriteLine("已经建立连接");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            Console.ReadKey();
        }
    }
}
