using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SqlHelper
    {
        public string Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
        private SqlConnection _conn;
        public SqlHelper()
        {
            Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
        }
        public SqlHelper(string strCollegeCode)
        {
            if (strCollegeCode == "A")
                Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
            else if (strCollegeCode == "B")
                Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
            else if (strCollegeCode == "C")
                Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
            else if (strCollegeCode == "D")
                Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
            else if (strCollegeCode == "E")
                Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
        }
        #region Reader

        /// <summary>
        ///     Returns a datareader for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string cmdText, SqlParameter[] prms, CommandType type)
        {


            _conn = new SqlConnection(Constrg);
           
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    cmd.CommandType = type;
                    cmd.CommandTimeout = 0;
                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                            cmd.Parameters.Add(p);
                    }
                    if (_conn.State == ConnectionState.Closed)
                        _conn.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            
            
        }

        /// <summary>
        ///     Returns a datareader for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string cmdText, CommandType type)
        {
            return ExecuteReader(cmdText, null, type);
        }
        #endregion

        #region DataSet

        /// <summary>
        ///     Returns a dataset for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prms"></param>
        /// <param name="type" />
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdText, SqlParameter[] prms, CommandType type)
        {
            using (_conn = new SqlConnection(Constrg))
            {
                var ds = new DataSet();
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    cmd.CommandType = type;
                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    var adpt = new SqlDataAdapter(cmd);
                    adpt.Fill(ds);
                    return ds;
                }
            }
        }


        /// <summary>
        ///     Returns a dataset for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdText, CommandType type)
        {
            return ExecuteDataSet(cmdText, null, type);
        }

        #endregion

        #region DataTable

        /// <summary>
        ///     Returns a datatable for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string cmdText, SqlParameter[] prms, CommandType type)
        {
            using (_conn = new SqlConnection(Constrg))
            {
                var dt = new DataTable();
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    cmd.CommandType = type;
                    cmd.CommandTimeout = 0;
                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    var adpt = new SqlDataAdapter(cmd);
                    adpt.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        ///     Returns a datatable for the sql command
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string cmdText, CommandType type)
        {
            return ExecuteDataTable(cmdText, null, type);
        }

        #endregion

        #region NonQuery

        /// <summary>
        ///     Executes a non query
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, SqlParameter[] prms, CommandType type)
        {
            try
            {
                using (_conn = new SqlConnection(Constrg))
                {
                    using (var cmd = new SqlCommand(cmdText, _conn))
                    {
                        cmd.CommandType = type;
                        cmd.CommandTimeout = 0;
                        if (prms != null)
                        {
                            foreach (SqlParameter p in prms)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        if (_conn.State == ConnectionState.Closed)
                            _conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _conn.Close();
                return 0;
            }
            finally
            {
                _conn.Close();
            }
        }

        //public int ExecuteNonQuery(string cmdText, SqlParameter[] prms, CommandType Type,int ArrayCount)
        //{

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(constrg))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        //                {
        //                    cmd.CommandType = Type;
        //                    cmd.BindByName = true;

        //                    cmd.ArrayBindCount = ArrayCount;
        //                    if (prms != null)
        //                    {
        //                        foreach (SqlParameter p in prms)
        //                        {
        //                            cmd.Parameters.Add(p);
        //                        }
        //                    }
        //                    conn.Open();
        //                    return cmd.ExecuteNonQuery();
        //                }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        /// <summary>
        ///     Executes a non query
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, CommandType type)
        {
            return ExecuteNonQuery(cmdText, null, type);
        }

        #endregion

        /// <summary>
        ///     Returns the scalar object of the query
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prms"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, SqlParameter[] prms, CommandType type)
        {
            try
            {
                using (_conn = new SqlConnection(Constrg))
                {
                    using (var cmd = new SqlCommand(cmdText, _conn))
                    {
                        cmd.CommandType = type;
                        if (prms != null)
                        {
                            foreach (SqlParameter p in prms)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        if (_conn.State == ConnectionState.Closed)
                            _conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            finally
            {
                _conn.Close();
            }
        }


        /// <summary>
        ///     Returns the scalar object of the query
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, CommandType type)
        {
            return ExecuteScalar(cmdText, null, type);
        }

        #region transaction

        public string Execute_Transaction(ArrayList query)
        {
            var exStr = "yes";
            _conn= new SqlConnection(Constrg);
            if (_conn.State ==ConnectionState.Closed )
                _conn.Open();
            using (SqlTransaction sqlTrans = _conn.BeginTransaction())
            {
                try
                {
                    var cmd = new SqlCommand {Connection = _conn, Transaction = sqlTrans};
                    foreach (object t in query)
                    {
                        cmd.CommandText = t.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    sqlTrans.Commit();
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    exStr = ex.Message;
                }
                finally
                {
                    _conn.Close();
                }
            }
            return exStr;
        }

        #endregion
    }
}