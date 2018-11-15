using STOMS.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace STOMS.DA
{
    public class Config
    {
       public Config()
        {

        }
        public int SaveConfig(ConfigurationBO configurationBO)
        {
            int rtnvalue = 0;
            using (SqlConnection con = new SqlConnection(Common.Constant.DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sspSaveConfig"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", configurationBO.TenantID));
                    cmd.Parameters.Add(new SqlParameter("@ConfigValue", configurationBO.ConfigValue));
                    cmd.Parameters.Add(new SqlParameter("@ConfigName", configurationBO.ConfigName));
                    cmd.Parameters.Add(new SqlParameter("@ConfigType", configurationBO.ConfigType));
                    cmd.Parameters.Add(new SqlParameter("@PrefixYear", configurationBO.PrefixYear));
                    cmd.Parameters.Add(new SqlParameter("@ConfigID", configurationBO.ConfigID));
                    cmd.Parameters.Add(new SqlParameter("@SrNumber", configurationBO.SrNumber));

                    con.Open();
                    rtnvalue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            return rtnvalue;
        }
    }
}
