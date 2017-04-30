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

            //Query the Rules and measurements tables
            //Run the UpdateEvents Stored Procedure for each rule
            using (conn)
            {
                using (var rulesCommand = conn.CreateCommand())
                using(var measurementsCommand = conn.CreateCommand())
                {
                    rulesCommand.CommandText = "SELECT Value, FK_RuleTypeId FROM Rules";
                    measurementsCommand.CommandText = "SELECT tagName FROM Measurments";

                    try
                    {
                        conn.Open();
                        using (var reader = rulesCommand.ExecuteReader())
                        using (var mreader = measurementsCommand.ExecuteReader())
                        {
                            var indexOfValue = reader.GetOrdinal("Value");
                            var indexOfRuleId = reader.GetOrdinal("FK_RuleTypeId");
                            var indexOfTag = mreader.GetOrdinal("tagName");

                            while (reader.Read())
                            {
                                var compareVal = reader.GetValue(indexOfValue);
                                var ruleId = reader.GetValue(indexOfRuleId);
                                var tag = mreader.GetValue(indexOfTag);

                                SqlCommand cmd = new SqlCommand("Update_Events", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@CompareValue", SqlDbType.Int).Value = compareVal;
                                cmd.Parameters.Add("@ruleid", SqlDbType.Int).Value = ruleId;
                                cmd.Parameters.Add("@tag", SqlDbType.NVarChar).Value = tag;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
         
        }
      
        static void Main(string[] args)
        {
            Program p = new Program();
            p.execStoredProc();
        }
    }
}
