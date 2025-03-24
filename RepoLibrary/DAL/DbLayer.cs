using Microsoft.Extensions.Configuration;
using RepoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RepoLibrary.DAL
{
    public abstract class DbLayer
    {
        private readonly string constr;
        public DbLayer(IConfiguration configuration)
        {
            this.constr = configuration["ConnectionStrings:Constr"];
        }
        public async Task<DataSet> Datasetwithsp(string spname, Dictionary<string, object> spParams)
        {
            DataSet ds = new DataSet();
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection con = new SqlConnection(this.constr))
                    {

                        using (SqlCommand cmd = new SqlCommand(spname, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (spParams != null && spParams.Count > 0)
                            {
                                foreach (var item in spParams)
                                {
                                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                                }
                            }
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {

            }
            return ds;
        }

        public async Task<int> ExecuteNonquerywithSp(string spName, Dictionary<string, object> spParams)
        {
            string SqlconString = this.constr;
            int result = 0;
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection sqlCon = new SqlConnection(SqlconString))
                    {
                        sqlCon.Open();
                        SqlCommand cmd = new SqlCommand(spName, sqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (spParams != null && spParams.Count > 0)
                        {
                            foreach (var item in spParams)
                            {
                                cmd.Parameters.AddWithValue(item.Key, item.Value);
                            }
                        }
                        result = cmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                });

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<string> ExecutescalarwithSp(string spName, Dictionary<string, object> spParams)
        {
            string result = string.Empty;
            await Task.Run(() =>
             {
                 using (SqlConnection con = new SqlConnection(this.constr))
                 {
                     using (SqlCommand cmd = new SqlCommand(spName, con))
                     {
                         con.Open();
                         cmd.CommandType = CommandType.StoredProcedure;
                         if (spParams != null && spParams.Count > 0)
                         {
                             foreach (var item in spParams)
                             {
                                 cmd.Parameters.AddWithValue(item.Key, item.Value);
                             }
                         }
                         object res = cmd.ExecuteScalar();
                         con.Close();
                         if (res != null)
                         {
                             result = (res == null) ? "" : res.ToString();
                         }
                     }
                 }

             });

            return result;
        }
    }
}
