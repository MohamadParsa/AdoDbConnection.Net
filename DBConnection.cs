using System;
using System.Data.SqlClient;
using System.Data;

namespace AdoDbConnection
{
    public class DBConnection
    {
        /// <summary>
        /// you don't need to set connection string elements for every time.
        /// just set it at class before use and call methods.
        /// also, you can use express connections.
        /// </summary>
        #region Connection String Items 
        /// <summary>
        /// server address with the instance name. EX: dbserve\sql2019
        /// </summary>
        static public string ServerName { get; set; }
        /// <summary>
        /// database name on the server
        /// </summary>
        static public string DataBbaseName { get; set; }
        /// <summary>
        /// username for login into sql engine
        /// </summary>
        static public string Username { get { return ""; } set { _Username = value; } }
        /// <summary>
        /// password for login into sql engine
        /// </summary>
        static public string Password { get { return ""; } set { _Password = value; } }
        /// <summary>
        /// if you want to use sql express as true
        /// </summary>
        static public bool IsExpress { get; set; }
        #endregion

        #region Variable Definition
        /// <summary>
        /// return error code and error type
        /// </summary>
        public string ErorrString { get; set; }
        /// <summary>
        /// return error detail you can use for log system.
        /// </summary>
        public string InternalErorrString { get; set; }

        static private string _Username;
        static private string _Password;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        #endregion

        /// <summary>
        /// make sql connection to run commands
        /// </summary>
        private void Connect()
        {
            ErorrString = "";
            InternalErorrString = "";
            con = new SqlConnection();
            cmd = new SqlCommand();
            da = new SqlDataAdapter();
            cmd.Connection = con;
            da.SelectCommand = cmd;
            try
            {
                string cs = "";
                if (IsExpress)
                {
                    cs = @"Data source=.\SQLEXPRESS;Attachdbfilename=|DataDirectory|\" + DataBbaseName + ".mdf;Integrated security=true;user Instance=true";
                }
                else
                {
                    cs = @"server=" + @ServerName + ";database=" + DataBbaseName + ";uid=" + _Username + ";pwd=" + _Password + ";";
                    cs = string.Format(cs, DataBbaseName);
                }
                con.ConnectionString = cs;
                con.Open();
            }
            catch (Exception ex)
            {
                ErorrString += "Erorr NO. : 100" + ", connection error.";
                InternalErorrString += ex.Message;
            }
        }
        /// <summary>
        /// disconnect connection 
        /// </summary>
        private void Disconnect()
        {
            con.Close();
        }

        /// <summary>
        /// for run tsql command and get results in a dataset
        /// </summary>
        /// <param name="sql">command text like select or exec something</param>
        /// <returns></returns>
        public DataSet RunAndGet(string sql)
        {
            //first, make a connection
            Connect();
            //to hold and return results
            DataSet dataSet = new DataSet();
            try
            {
                //set command
                cmd.CommandText = sql;
                //run and fill results into the dataset
                da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                ErorrString += "Erorr NO. : 101" + ", internal error.";
                InternalErorrString += ex.Message;

            }
            //finally closes connection
            Disconnect();
            return dataSet;
        }
        /// <summary>
        /// for run tsql command without result
        /// </summary>
        /// <param name="sql">command text such as exec something</param>
        /// <returns></returns>
        public void Run(string sql)
        {
            //first, make connection
            Connect();
            try
            {
                //set command
                cmd.CommandText = sql;
                //run command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErorrString += "Erorr NO. : 102" + ", enternal error.";
                InternalErorrString += ex.Message;
            }
            //finally closes connection
            Disconnect();
        }
    }
}
