using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LifePoints.BloodBank.Database
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
                    bb.BB_ID = rdr["BB_ID"].ToString();
                    bb.BB_USERNAME = rdr["BB_USERNAME"].ToString();
                    bb.BB_PASSWORD = rdr["BB_PASSWORD"].ToString();
                    bb.BB_RANK = Convert.ToBoolean(rdr["BB_RANK"]);
                    bb.BB_STATUS = Convert.ToBoolean(rdr["BB_STATUS"]);
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


        public DataTable GetTransactionLogsTableData()
        {

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select TL_ID,TL_TRANSACTION_ID,TL_ACC_ID, TL_BLOOD_TYPE, TL_TRANSACTION_AMOUNT,TLTRANSACTION_DATE,    
if(TL_TRANSACTION = false, 'Blood Request',  if(TL_TRANSACTION = true, 'Blood Donation', 'Blood Donation')) as TL_TRANSACTION
from transaction_logs;");
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();


               
            }
            catch (Exception ex)
            {
                Debug.Print("Get Transactions Logs Error : " + ex.Message);
            }
            return dt;
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
                cmd.CommandText = "select * from activity_logs where ACT_UNAME='BloodBank' order by ACT_DATE desc;";
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

        public DataTable GetInventoryTableData()
        {

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from inventory;";
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get Inventory Logs Error : " + ex.Message);
            }
            return dt;
        }

        //Update BloodBank Password
        public int UpdateProfileInfo(string uname, string npword, string opword)
        {
            //1 if Success
            //-2 if Old Password is not the one on record
            //-1 if Database

            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                //Check Old Password 
                cmd.CommandText = string.Format("select count(*) from account where binary ACC_EMAIL='{0}' and binary ACC_PASSWORD='{1}';", uname, opword);
                int check = Convert.ToInt32(cmd.ExecuteScalar());
                if(check >= 1)
                {
                    //Meaning the Old Password is right
                    //Try to Update
                    cmd.CommandText = string.Format("update account set ACC_PASSWORD='{0}' where binary ACC_EMAIL='{1}' and binary ACC_PASSWORD='{2}';", npword, uname, opword);
                    int x = cmd.ExecuteNonQuery();
                    if(x > 0)
                    {
                        //Successful Update
                        res = 1;
                    }
                }
                else
                {
                    //Old Password does not match
                    res = -2;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Information Update Error : " + ex.Message);
            }
            return res;
        }

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
        public LifePoints.Database.blood_request SearchBloodRequest(string id)
        {
            LifePoints.Database.blood_request br = new LifePoints.Database.blood_request();
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
                    br.BREQ_CONSENT= rdr["BREQ_CONSENT"].ToString();
                    br.BREQ_VISIT_DATE = rdr["BREQ_VISIT_DATE"].ToString();
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


        //Update Blood Inventory - Donor

        public bool BD_UpdateInventory(int number, string type)
        {
            bool res = false;
            int n = 0;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from inventory where inv_blood_type='{0}';", type);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    string str = rdr["inv_numbers"].ToString();
                    n = int.Parse(str);

                }

                con.Close();

                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"update inventory set inv_numbers={0} where inv_blood_type='{1}';", n + number, type);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    res = true;
                }
                con.Close();


            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message);
            }




            return res;
        }

        public bool TransactionLogs(string query)
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
                Debug.Print("Transactions Logs  Error : " + ex.Message);
            }
            return res;
        }

        //Update Blood Inventory - Request
        public bool BR_UpdateInventory(int number, string type)
        {
            bool res = false;
            int n = 0;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from inventory where inv_blood_type='{0}';", type);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    string str = rdr["inv_numbers"].ToString();
                    n = int.Parse(str);

                }

                con.Close();

                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"update inventory set inv_numbers={0} where inv_blood_type='{1}';", n - number, type);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    res = true;
                }
                con.Close();


            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message);
            }




            return res;
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


        //Get Notification in List
        public List<notifications> GetNotifications(string query)
        {
            List<notifications> nList = new List<notifications>();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read() && !rdr.IsDBNull(0))
                {
                    notifications n = new notifications();
                    n.NTF_ID = rdr["NTF_ID"].ToString();
                    n.NTF_SUBJECT = rdr["NTF_SUBJECT"].ToString();
                    n.NTF_MESSAGE = rdr["NTF_MESSAGE"].ToString();
                    n.NTF_SENDER_ID = rdr["NTF_SENDER_ID"].ToString();
                    n.NTF_RECEIVER_ID = rdr["NTF_RECEIVER_ID"].ToString();
                    n.NTF_STATUS = Convert.ToBoolean(rdr["NTF_STATUS"]);
                    n.NTF_DATE = rdr["NTF_DATE"].ToString();
                    nList.Add(n);
                }
                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get Notification List error : " + ex.Message);
            }
            return nList;
        }

        //Get Notification Unread count
        public int GetUnreadNotificationCount(string query)
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
            catch (Exception ex)
            {
                Debug.Print("Get Unread Notification COunt Error : " + ex.Message);
            }
            return res;
        }


        //Insert into Notification Table
        public bool InsertToNotification(string query)
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
                Debug.Print("Insert To Notification Error : " + ex.Message);
            }
            return res;
        }

        public DataTable GetNotificationTableData(account ua)
        {

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select NTF_ID, NTF_SUBJECT, NTF_DATE, if(NTF_STATUS=true, 'READ', 'UNREAD') as NTF_STATUS from notifications where NTF_RECEIVER_ID={0} order by NTF_STATUS desc, NTF_DATE desc;", ua.ACC_ID);
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get Action Logs Error : " + ex.Message);
            }
            return dt;
        }



        //Get Notification Details
        public notifications SearchNotification(string id)
        {
            notifications res = new notifications();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                Debug.Print(string.Format(@"select * from notifications where NTF_ID={0};", id));
                cmd.CommandText = string.Format(@"select * from notifications where NTF_ID={0};", id);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        res.NTF_ID = rdr["NTF_ID"].ToString();
                        res.NTF_SUBJECT = rdr["NTF_SUBJECT"].ToString();
                        res.NTF_MESSAGE = rdr["NTF_MESSAGE"].ToString();
                        res.NTF_SENDER_ID = rdr["NTF_SENDER_ID"].ToString();
                        res.NTF_RECEIVER_ID = rdr["NTF_RECEIVER_ID"].ToString();
                        res.NTF_STATUS = Convert.ToBoolean(rdr["NTF_STATUS"]);
                        res.NTF_DATE = rdr["NTF_DATE"].ToString();
                    }
                }
                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Search Notification Error : " + ex.Message);
            }
            return res;
        }

        //Updat eNotification Status
        public bool UpdateNotificationStatus(string id)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                Debug.Print(string.Format(@"update notifications set NTF_STATUS=true where NTF_ID={0};", id));
                cmd.CommandText = string.Format(@"update notifications set NTF_STATUS=true where NTF_ID={0};", id);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    Debug.Print("Success");
                    res = false;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Update Notification Status Error : " + ex.Message);
            }
            return res;
        }



    }
}