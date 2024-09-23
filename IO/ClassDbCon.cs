using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace IO
{
    public class ClassDbCon
    {
        private string _connectionString;
        protected SqlConnection _con;
        private SqlCommand _command;

        public ClassDbCon()
        {
            //_connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=FamilyDB2024;Trusted_Connection=True;";
            //_con = new SqlConnection(_connectionString);

        }
        public ClassDbCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            _con = new SqlConnection(_connectionString);
        }
        protected void SetCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            _con = new SqlConnection(_connectionString);
        }
        protected void CloseDB()
        {
            try
            {
                _con.Close();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        protected void OpenDB()
        {
            try
            {
                if (this._con != null && this._con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
                else
                {
                    if(this._con == null)
                    {
                        this._con = new SqlConnection(_connectionString);
                        OpenDB();
                    }
                    else
                    {
                        CloseDB();
                        OpenDB();
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Denne metode benyttes til Query der ikke skal returnere et bestemt datasæt, men 
        /// metoden vil altid returnere det antal rows som dette query har påvirket idatabasen/tabellen.
        /// Denne metode benyttes til querys af typen: INSERT, UPDATE, CREATE og DELETE
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>int</returns>
        protected int NonResQuery(string sqlQuery)
        {
            int res = 0;

            try
            {
                OpenDB();
                using (_command = new SqlCommand(sqlQuery, this._con))
                {
                    res = _command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                
                throw ex;
            }
            finally
            {
                CloseDB();
            }

            return res;
        }

        /// <summary>
        /// Denne metode benyttes når det forventes, at resultatet af forspørgelsen vil give rækker og kolloner (tabel)
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>DataTable</returns>
        protected DataTable DBReturnDataTable(string sqlQuery)
        {
            DataTable dtRes = new DataTable();
            try
            {
                OpenDB();
                using (_command = new SqlCommand(sqlQuery, this._con))
                {
                    using (var adapter = new SqlDataAdapter(_command))
                    {
                        adapter.Fill(dtRes);
                    }
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return dtRes;
        }

        protected string DBReturnString(string sqlQuery)
        {
            string res = "";
            bool foundOne = false;
            try
            {
                OpenDB();
                using (_command = new SqlCommand(sqlQuery, this._con))
                {
                    using(SqlDataReader reader = _command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res  = reader.GetString(0);
                            foundOne = true;

                        }
                        if (!foundOne)
                        {
                            res = "DATA NOT FOUND !!!!";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally { CloseDB(); }
            return res;
        }
        protected decimal DBReturnDecimal(string sqlQuery)
        {
            decimal res = 0M;
            
            try
            {
                OpenDB();
                using (_command = new SqlCommand(sqlQuery, this._con))
                {
                    using (SqlDataReader reader = _command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res  = reader.GetDecimal(0);

                        }
                        
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally { CloseDB(); }
            return res;
        }
    }
}
