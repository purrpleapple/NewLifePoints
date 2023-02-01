using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using LifePoints.Admin.Database;

namespace LifePoints.Admin.Database
{
    public class Database_Connections
    {
        public static string connectionType = "GCloud"; //Local or GCloud or SmarterASP
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

        //For bloodbank Login
        public bloodbank BloodbankLogin(string query)
        {
            bloodbank bb = new bloodbank();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                if(rdr.Read() && !rdr.IsDBNull(0))
                {
                    bb.BB_ID = rdr["ADMIN_ID"].ToString();
                    bb.BB_USERNAME = rdr["ADMIN_EMAIL"].ToString();
                    bb.BB_PASSWORD = rdr["ADMIN_PASSWORD"].ToString();
                   
                }
                rdr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Login Error : " + ex.Message);
            }
            return bb;
        }

        //For Inserting Data on Bloodbank Logs Table
        public bool InsertBloodBankLogs(string query)
        {
            bool res = false;

            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                int x = cmd.ExecuteNonQuery();
                if(x > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch(Exception ex )
            {
                Debug.Print("Action Logs Error : " + ex.Message);
            }

            return res;
        }

        //If pang populate sa GridView DataTable jud ang datatype nga gamiton
        public DataTable GetBloodBankLogsTableData()
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from activity_logs order by ACT_DATE desc;";
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get Action Logs Error : " + ex.Message);
            }
            return dt;
        }

        //Update BloodBank Password
                //public int UpdateProfileInfo(string uname, string npword, string opword)
                //{
                //    //1 if Success
                //    //-2 if Old Password is not the one on record
                //    //-1 if Database

                //    int res = -1;
                //    try
                //    {
                //        DB_Connect();
                //        con.Open();
                //        cmd = con.CreateCommand();
                //        //Check Old Password 
                //        cmd.CommandText = string.Format("select count(*) as CountRow from admin where binary ADMIN_EMAIL='{0}' and binary ADMIN_PASSWORD='{1}';", uname, opword);
                //        int check = Convert.ToInt32(cmd.ExecuteScalar());
                //        if(check >= 1)
                //        {
                //            //Meaning the Old Password is right
                //            //Try to Update
                //            cmd.CommandText = string.Format("update admin set ADMIN_PASSWORD='{0}' where binary ADMIN_EMAIL='{1}' and binary ADMIN_PASSWORD='{2}';", npword, uname, opword);
                //            int x = cmd.ExecuteNonQuery();
                //            if(x > 0)
                //            {
                //                //Successful Update
                //                res = 1;
                //            }
                //        }
                //        else
                //        {
                //            //Old Password does not match
                //            res = -2;
                //        }
                //        con.Close();
                //    }
                //    catch(Exception ex)
                //    {
                //        Debug.Print("Information Update Error : " + ex.Message);
                //    }
                //    return res;
                //}

        //Get Blood Request 
        public DataTable GetBloodTransactionTableData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get Blood Request : " + ex.Message);
            }
            return dt;
        }

        //Search Blood Request on grid table row selected
        public blood_request SearchBloodRequest(string id)
        {
            blood_request br = new blood_request();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from blood_request where BREQ_ID={0};", id);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    br.BREQ_ID = rdr["BREQ_ID"].ToString();
                    br.BREQ_UACC_ID = rdr["BREQ_UACC_ID"].ToString();
                    br.BREQ_JSON_SURVEY_FORM = rdr["BREQ_JSON_SURVEY_FORM"].ToString();
                    br.BREQ_SURVEY_STATUS = Convert.ToBoolean(rdr["BREQ_SURVEY_STATUS"]);
                    br.BREQ_BLOOD_STATUS = Convert.ToBoolean(rdr["BREQ_BLOOD_STATUS"]);
                    br.BREQ_REQ_STATUS = Convert.ToBoolean(rdr["BREQ_REQ_STATUS"]);
                    br.BREQ_DATE = rdr["BREQ_DATE"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Search Blood request Error : " + ex.Message);
            }
            return br;
        }

        //Update Blood Request Status
        public bool UpdateBloodRequestStatus(string query)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                int x = cmd.ExecuteNonQuery();
                if(x > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Update Blood Request Status Error : " + ex.Message);
            }
            return res;
        }

        //Get User Blood Donation
        public DataTable GetuserBloodDonation(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get User Blood Donation : " + ex.Message);
            }
            return dt;
        }

        //Search Blood Request on grid table row selected
        public blood_donation SearchBloodDonation(string id)
        {
            blood_donation bd = new blood_donation();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from blood_donation where BD_ID={0};", id);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    bd.BD_ID = rdr["BD_ID"].ToString();
                    bd.BD_UACC_ID = rdr["BD_UACC_ID"].ToString();
                    bd.BD_JSON_SURVEY_FORM = rdr["BD_JSON_SURVEY_FORM"].ToString();
                    bd.BD_SURVEY_STATUS = Convert.ToBoolean(rdr["BD_SURVEY_STATUS"]);
                    bd.BD_BLOOD_STATUS = Convert.ToBoolean(rdr["BD_BLOOD_STATUS"]);
                    bd.BD_REQ_STATUS = Convert.ToBoolean(rdr["BD_REQ_STATUS"]);
                    bd.BD_DATE = rdr["BD_DATE"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Search Blood Donation Error : " + ex.Message);
            }
            return bd;
        }

        //Get COunter accepts string query
        public int GetCount(string query)
        {
            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                res = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get Count Error : " + ex.Message);
            }
            return res;
        }


        public DataTable SampleReapeater()
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select *, concat(UI_FNAME, ' ', UI_LNAME) as BLOG_UACC_NAME, ACC_EMAIL as BLOG_UACC_EMAIL from blog_post join user_info on BLOG_UACC_ID=UI_ID join account on ACC_ID=UI_ID where BLOG_STATUS=true order by BLOG_DATE desc;";
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Sample Repeater Error : " + ex.Message);
            }
            return dt;
        }


        public DataTable GetUserTableData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get User : " + ex.Message);
            }
            return dt;
        }

    }
}