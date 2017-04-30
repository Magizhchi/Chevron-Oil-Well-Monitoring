using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataScheduling
{
    class UpdateEvents
    {
        public void execStoredProc()
        {
            SqlConnection conn = new SqlConnection("Server=tcp:chevrondata.database.windows.net,1433;Initial Catalog=DataSimulator;Persist Security Info=False;User ID=chaincoders;Password=EveryDayImCoding0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            try 
            {
                conn.Open();               

                cmd = new SqlCommand("UpdateEvents", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompareValue", SqlDbtype.Int).Value = 90;
                cmd.Parameters.Add("@ruleid", SqlDbtype.Int).Value = 2;
                cmd.Parameters.Add("@tag", SqlDbtype.NVarChar).Value = 90;
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
            UpdateEvents p = new UpdateEvents();
            p.execStoredProc();
        }
    }
}
