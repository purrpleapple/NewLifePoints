using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Diagnostics;

namespace LifePoints.Database
{
    public class Database_Connection
    {
        public static string connectionType = "GCloud";
        readonly string path = ConfigurationManager.ConnectionStrings[connectionType].ConnectionString;

        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader rdr;
        private MySqlDataAdapter da;

        public void DB_Connect()
        {
            try
            {
                con = new MySqlConnection(path);
            }
            catch (Exception ex)
            {
                Debug.Print("Connection Error : " + ex.Message);
            }
        }

        public account LoginUser(string email, string pword) 
        {
            account acc = new account();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from account where ACC_EMAIL='{0}' and binary ACC_PASSWORD='{1}' and ACC_STATUS=true;", email, pword);
                if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    //User Found
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        if(!rdr.IsDBNull(0))
                        {
                            acc.ACC_ID = rdr["ACC_ID"].ToString();
                            acc.ACC_EMAIL = rdr["ACC_EMAIL"].ToString();
                            acc.ACC_PASSWORD = rdr["ACC_PASSWORD"].ToString();
                            acc.ACC_TYPE = rdr["ACC_TYPE"].ToString();
                            acc.ACC_REQUESTOR = Convert.ToBoolean(rdr["ACC_REQUESTOR"]);
                            acc.ACC_DONOR = Convert.ToBoolean(rdr["ACC_DONOR"]);
                            acc.ACC_STATUS = Convert.ToBoolean(rdr["ACC_STATUS"]);
                        }
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                Debug.Print("Error Login User : " + ex.Message);
            }
            return acc;
        }

        public int RegisterAccount(account acc)
        {
            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) as EMAILCOUNT from account where ACC_EMAIL='{0}';", acc.ACC_EMAIL);
                if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    //Email Already Exists
                    res = -2;
                }
                else
                {
                    cmd.CommandText = string.Format("insert into account(ACC_EMAIL, ACC_PASSWORD) values('{0}', '{1}');", acc.ACC_EMAIL, acc.ACC_PASSWORD);
                    if(cmd.ExecuteNonQuery() >  0)
                    {
                        //Success insert. Get ID
                        cmd.CommandText = string.Format("select ACC_ID from account where ACC_EMAIL='{0}';", acc.ACC_EMAIL);
                        res = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Register Account Error : " + ex.Message);
            }
            return res;
        }

        public bool InsertUserInfo(user_info ui)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"insert into user_info(UI_ID, UI_LNAME, UI_FNAME, UI_MNAME, UI_GENDER, UI_DOB, UI_ADDRESS) 
values({0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}');", ui.UI_ID, ui.UI_LNAME, ui.UI_FNAME, ui.UI_MNAME, ui.UI_GENDER, ui.UI_DOB, ui.UI_ADDRESS);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    //Success Insert
                    res = true;
                }
                con.Close();

            }
            catch (Exception ex)
            {
                Debug.Print("Insert User Info Error : " + ex.Message);
            }
            return res;
        }

        //Insert Into User Logs
        public bool InsertToUserLogs(string query)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Insert To User Logs Error : " + ex.Message);
            }
            return res;
        }




    }
}