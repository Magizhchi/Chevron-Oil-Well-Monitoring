using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataScheduling
{
    class Program
    {
        public void execStoredProc()
        {
            SqlConnection conn = new SqlConnection("Server=tcp:chevrondata.database.windows.net,1433;Initial Catalog=DataSimulator;Persist Security Info=False;User ID=chaincoders;Password=EveryDayImCoding0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            try 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("create_data", conn);               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Minutes", SqlDbType.Int).Value = 5;
                cmd.ExecuteNonQuery();
               
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
        }
      
        static void Main(string[] args)
        {
            Program p = new Program();
            p.execStoredProc();
        }
    }
}
