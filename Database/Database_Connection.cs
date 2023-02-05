using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        //Login user returns account
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

        public int UpdateUserAccount(user_info ui, string emailNew, string emailOld, string pass)
        {


            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                if (emailOld == emailNew)
                {
                    cmd.CommandText = string.Format(@"UPDATE user_info SET UI_LNAME ='{0}', UI_FNAME= '{1}', UI_MNAME='{2}', UI_GENDER={3}, UI_DOB='{4}', UI_ADDRESS='{5}', UI_BTYPE= '{6}', UI_HOME='{7}', UI_MOBILE='{8}' WHERE UI_ID = {9};", ui.UI_LNAME, ui.UI_FNAME, ui.UI_MNAME, ui.UI_GENDER, ui.UI_DOB, ui.UI_ADDRESS, ui.UI_BTYPE, ui.UI_HOME, ui.UI_MOBILE,ui.UI_ID); ;

                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        //Successful Update
                        res = 1;
                    }


                }
                else
                {
                    cmd.CommandText = "select count(*) as duplicate from account where ACC_EMAIL='" + emailNew + "';";
                    int x = Convert.ToInt32(cmd.ExecuteScalar());
                    if (x <= 0)
                    {
                        cmd.CommandText = string.Format(@"UPDATE user_info SET UI_LNAME ='{0}', UI_FNAME= '{1}', UI_MNAME='{2}', UI_GENDER={3}, UI_DOB='{4}', UI_ADDRESS='{5}', UI_BTYPE= '{6}', UI_HOME='{7}', UI_MOBILE='{8}' WHERE UI_ID = {9};", ui.UI_LNAME, ui.UI_FNAME, ui.UI_MNAME, ui.UI_GENDER, ui.UI_DOB, ui.UI_ADDRESS, ui.UI_BTYPE, ui.UI_HOME, ui.UI_MOBILE, ui.UI_ID); ;
                        


                        int y = cmd.ExecuteNonQuery();
                        if (y > 0)
                        {
                            //Successful Update
                            cmd.CommandText = string.Format(@"UPDATE account SET ACC_EMAIL='{0}', ACC_PASSWORD='{1}' WHERE ACC_ID={2};", emailNew, pass, ui.UI_ID);
                            int z = cmd.ExecuteNonQuery();
                            if (z > 0)
                            {
                                res = 1;
                            
                            }
                               
                        }
                    }
                    else
                    {
                        res = -2;
                    }
                }

                con.Close();


            }
            catch (Exception ex)
            {
                Debug.Print("Update User Account Error : " + ex.Message);
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
                cmd.CommandText = string.Format(@"insert into user_info(UI_ID, UI_LNAME, UI_FNAME, UI_MNAME, UI_GENDER, UI_DOB, UI_ADDRESS,UI_BTYPE, UI_HOME, UI_MOBILE) 
values({0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}', '{9}');", ui.UI_ID, ui.UI_LNAME, ui.UI_FNAME, ui.UI_MNAME, ui.UI_GENDER, ui.UI_DOB, ui.UI_ADDRESS, ui.UI_BTYPE, ui.UI_HOME, ui.UI_MOBILE);
                if (cmd.ExecuteNonQuery() > 0)
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

        public user_info GetUserInfo(string id)
        {
            user_info ui = new user_info();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select * from user_info where UI_ID={0};", id);
                rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        ui.UI_ID = id;
                        ui.UI_LNAME = rdr["UI_LNAME"].ToString();
                        ui.UI_FNAME = rdr["UI_FNAME"].ToString();
                        ui.UI_MNAME = rdr["UI_MNAME"].ToString();
                        ui.UI_GENDER = Convert.ToBoolean(rdr["UI_GENDER"]);
                        ui.UI_BTYPE = rdr["UI_BTYPE"].ToString();
                        ui.UI_DOB = rdr["UI_DOB"].ToString();
                        ui.UI_HOME = rdr["UI_HOME"].ToString();
                        ui.UI_MOBILE = rdr["UI_MOBILE"].ToString();
                        ui.UI_ADDRESS = rdr["UI_ADDRESS"].ToString();
                    }
                }
                rdr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get User Info Error : " + ex.Message);
            }
            return ui;
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


        //Send Blog Post
        public int PostBlogPost(string content, string id)
        {
            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                //query to insert post
                string query = string.Format(@"insert into blog_post(BLOG_CONTENT, BLOG_UACC_ID) values('{0}', {1});", content, id);
                cmd.CommandText = query;
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    //Success fully Inserted Post
                    //get Post ID for Logs
                    cmd.CommandText = string.Format("select BLOG_ID from blog_post where BLOG_UACC_ID={0} and BLOG_CONTENT='{1}';", id, content);
                    res = Convert.ToInt32(cmd.ExecuteScalar());

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Post Blog Post Error : " + ex.Message);
            }
            return res;
        }

        //public bool Report Post
        public bool ReportPost(int blogid, string uid)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                //Check if User Already Reported Post
                cmd.CommandText = string.Format("select BLOG_REPORT from blog_post where BLOG_ID={0};", blogid);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                count++;
                if (count >= 3)
                {
                    cmd.CommandText = string.Format("update blog_post set BLOG_REPORT=3, BLOG_REPORTER='{0}', BLOG_STATUS=false where BLOG_ID={1};", uid, blogid);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        res = true;
                    }
                }
                else
                {
                    cmd.CommandText = string.Format("update blog_post set BLOG_REPORT={0}, BLOG_REPORTER='{1}' where BLOG_ID={2};", count, uid, blogid);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        res = true;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Report Post : " + ex.Message);
            }
            return res;
        }

        //Insert Blood Request
        public bool InsertBloodrequest(blood_request br)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) from blood_request where BREQ_UACC_ID={0} and ((BREQ_SURVEY_STATUS = false or BREQ_BLOOD_STATUS = false) and BREQ_REQ_STATUS=true);", br.BREQ_UACC_ID);
                int chck = Convert.ToInt32(cmd.ExecuteScalar());
                if (chck <= 0)
                {
                    //walay existing
                    cmd.CommandText = string.Format("insert into blood_request(BREQ_JSON_SURVEY_FORM, BREQ_UACC_ID, BREQ_CONSENT, BREQ_DEMAND_DATE, BREQ_BLOOD_TYPE, BREQ_NO_BLOOD) values('{0}', {1}, '{2}', '{3}','{4}', {5} );", br.BREQ_JSON_SURVEY_FORM, br.BREQ_UACC_ID, br.BREQ_CONSENT, br.BREQ_DEMAND_DATE, br.BREQ_BLOOD_TYPE, br.BREQ_NO_BLOOD);
                    int x = cmd.ExecuteNonQuery();


                    if (x > 0)
                    {
                        cmd.CommandText = string.Format(@"update account set ACC_REQUESTOR=true where ACC_ID={0};", br.BREQ_UACC_ID);
                        int y = cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Insert Blood request Error : " + ex.Message);
            }
            return res;
        }

        //Update Blood Request
        public bool UpdateBloodRequest(blood_request br)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"update blood_request set BREQ_JSON_SURVEY_FORM='{0}', BREQ_CONSENT='{1}', BREQ_DEMAND_DATE='{2}', BREQ_BLOOD_TYPE='{3}', BREQ_NO_BLOOD='{4}' where BREQ_ID={5};",
                                                    br.BREQ_JSON_SURVEY_FORM, br.BREQ_CONSENT, br.BREQ_DEMAND_DATE, br.BREQ_BLOOD_TYPE, br.BREQ_NO_BLOOD, br.BREQ_ID);
                int x = cmd.ExecuteNonQuery();
                if(x > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Update Blood Request Error : " + ex.Message);
            }
            return res;
        }

        public DataTable GetUserLogsTableData()
        {

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from activity_logs where ACT_UNAME='User' order by ACT_DATE desc;";
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

        public bool ClickBloodrequest(blood_request br)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) from blood_request where BREQ_UACC_ID={0} and BREQ_BLOOD_STATUS=false", br.BREQ_UACC_ID);
                int chck = Convert.ToInt32(cmd.ExecuteScalar());
                if (chck <= 0)
                {

                    res = true;

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Blood request Error : " + ex.Message);
            }
            return res;
        }

        //Get User Blood Requests
        public DataTable GetuserBloodRequests(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select BREQ_ID, BREQ_UACC_ID, BREQ_JSON_SURVEY_FORM, BREQ_REQ_STATUS, BREQ_DATE,BREQ_VISIT_DATE,BREQ_DEMAND_DATE, BREQ_BLOOD_TYPE, BREQ_NO_BLOOD,
                                                    if(BREQ_SURVEY_STATUS = false && BREQ_REQ_STATUS = true, 'PENDING', 
                                                    if(BREQ_SURVEY_STATUS = true && BREQ_REQ_STATUS = true, 'APPROVED', 
                                                    if(BREQ_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BREQ_SURVEY_STATUS,
                                                    if(BREQ_BLOOD_STATUS = false && BREQ_REQ_STATUS = true, '--', 
                                                    if(BREQ_BLOOD_STATUS = true && BREQ_REQ_STATUS = true, 'YES', 
                                                    if(BREQ_REQ_STATUS = false, 'NO', 'REJECTED'))) as BREQ_BLOOD_STATUS
                                                     from blood_request where BREQ_UACC_ID={0} order by BREQ_DATE desc;", id);
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Get User Blood Requests : " + ex.Message);
            }
            return dt;
        }

        //Check if there is Existing Un Approved Requests by user
        public bool CheckUserBloodRequests(string id)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) from blood_request where BREQ_UACC_ID={0} and ((BREQ_BLOOD_STATUS=true and BREQ_REQ_STATUS=true) or (BREQ_BLOOD_STATUS=false and BREQ_REQ_STATUS=false));", id);
                int chck = Convert.ToInt32(cmd.ExecuteScalar());
                if (chck > 0)
                {
                    res = true;
                    Debug.Print("Result : " + res);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Check User Blood request Error : " + ex.Message);
            }
            return res;
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
                    br.BREQ_CONSENT = rdr["BREQ_CONSENT"].ToString();
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

        //Insert Blood Donation Survey
        public bool InsertBloodDonationSurvey(blood_donation bd)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) from blood_donation where BD_UACC_ID={0} and (BD_SURVEY_STATUS = false or BD_BLOOD_STATUS=false) and BD_REQ_STATUS=true", bd.BD_UACC_ID);
                int chck = Convert.ToInt32(cmd.ExecuteScalar());
                if (chck <= 0)
                {
                    //walay existing
                    cmd.CommandText = string.Format("insert into blood_donation(BD_JSON_SURVEY_FORM, BD_UACC_ID) " +
                        "values('{0}', {1});", bd.BD_JSON_SURVEY_FORM, bd.BD_UACC_ID);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        res = true;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Insert Blood donation Error : " + ex.Message);
            }
            return res;
        }
        public int ClickDonationrequest(blood_donation br)
        {
            int res = -1;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select count(*) from blood_donation where BD_UACC_ID={0} and ((BD_SURVEY_STATUS=false or BD_BLOOD_STATUS=false) and BD_REQ_STATUS=true)", br.BD_UACC_ID);
                int chck = Convert.ToInt32(cmd.ExecuteScalar());
                if (chck <= 0)
                {
                    cmd.CommandText = "select count(*) from blood_donation where BD_UACC_ID=" + br.BD_UACC_ID;
                    int row = Convert.ToInt32(cmd.ExecuteScalar());
                    if (row > 0)
                    {
                        cmd.CommandText = "select * from blood_donation where BD_UACC_ID=" + br.BD_UACC_ID + " order by BD_DATE desc;";
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                            {
                                DateTime t = Convert.ToDateTime(rdr["BD_DATE"]);
                                TimeSpan limit = TimeSpan.FromDays(90);
                                Debug.Print("Date Res : " + (DateTime.Now - t));
                                Debug.Print("Date Res BOOl : " + ((DateTime.Now - t) >= limit));
                                if (DateTime.Now - t >= limit)
                                {
                                    res = 1;
                                }
                                else
                                {
                                    res = -2;
                                }
                            }
                        }
                        rdr.Close();
                    }
                    else
                    {
                        res = 1;
                    }



                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Blood request Error : " + ex.Message);
            }
            return res;
        }

        //Get User Blood Donation
        public DataTable GetuserBloodDonation(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select BD_ID, BD_UACC_ID, BD_JSON_SURVEY_FORM, BD_REQ_STATUS, BD_DATE,BD_VISIT_DATE,
                                                    if(BD_SURVEY_STATUS = false && BD_REQ_STATUS = true, 'PENDING', 
                                                    if(BD_SURVEY_STATUS = true && BD_REQ_STATUS = true, 'APPROVED', 
                                                    if(BD_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BD_SURVEY_STATUS,
                                                    if(BD_BLOOD_STATUS = false && BD_REQ_STATUS = true, '---', 
                                                    if(BD_BLOOD_STATUS = true && BD_REQ_STATUS = true, 'YES', 
                                                    if(BD_REQ_STATUS = false, 'REJECTED', 'NO'))) as BD_BLOOD_STATUS
                                                     from blood_donation where BD_UACC_ID={0} order by BD_DATE desc;", id);
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
                if (x > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Insert To Notification Error : " + ex.Message);
            }
            return res;
        }

        public DataTable GetNotificationTableData(user_info ua)
        {

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("select NTF_ID, NTF_SUBJECT, NTF_DATE, if(NTF_STATUS=true, 'READ', 'UNREAD') as NTF_STATUS from notifications where NTF_RECEIVER_ID={0} order by NTF_STATUS desc, NTF_DATE desc;", ua.UI_ID);
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

        public bool InsertUserMessage(user_info ub, int RecID, string message)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();

                //walay existing
                cmd.CommandText = string.Format(@"insert into message(MESSAGE_NAME,MESSAGE_SENDER_ID, MESSAGE_RECEIVER_ID,MESSAGE_CONTENT) values('{0}',{1},{2}, '{3}');", ub.UI_FNAME, ub.UI_ID, RecID, message);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    res = true;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Insert Message Error : " + ex.Message);
            }
            return res;

        }


        public DataTable MessageReapeater(int RecID, int SendID, string emailR)
        {
            // cmd.CommandText = string.Format(@"select * from message join user_account on MESSAGE_SENDER_ID=UACC_ID  or MESSAGE_SENDER_ID={0} where MESSAGE_RECEIVER_ID=UACC_ID or MESSAGE_RECEIVER_ID={1} ;", RecID, RecID);

            DataTable dt = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select * from message  where MESSAGE_SENDER_ID={0} and MESSAGE_RECEIVER_ID={1} or  MESSAGE_SENDER_ID={2} and MESSAGE_RECEIVER_ID={3};", SendID, RecID, RecID, SendID);
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


        public int[] InsertUserConvo(string email, string id)
        {
            int[] res = new int[2];

            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select ACC_ID from account where ACC_EMAIL='{0}' and ACC_STATUS=true;", email);
                int check = Convert.ToInt32(cmd.ExecuteScalar());
                if (check > 0)
                {
                    Debug.Print("Check : " + check);

                    cmd.CommandText = string.Format(@"select CV_ID from user_convo where 
(CV_ACC1_ID={0} and CV_ACC2_ID={1}) or (CV_ACC1_ID={2} and CV_ACC2_ID={3});", id, check, check, id);

                    int checkPrev = Convert.ToInt32(cmd.ExecuteScalar());
                    if(checkPrev > 0)
                    {
                        Debug.Print("Exists");
                        //Convo Already Exists
                        res[0] = -3;
                        res[1] = checkPrev;
                    }
                    else
                    {
                        Debug.Print("Does not Exists");
                        cmd.CommandText = string.Format(@"insert into user_convo(CV_ACC1_ID, CV_ACC2_ID) values({0}, {1});", id, check);

                        int i = cmd.ExecuteNonQuery();

                        if (i > 0)
                        {
                            cmd.CommandText = string.Format(@"select CV_ID from user_convo where (CV_ACC1_ID={0} and CV_ACC2_ID={1}) or (CV_ACC1_ID={2} and CV_ACC2_ID={3});", id, check, check, id);
                            int cv_id = Convert.ToInt32(cmd.ExecuteScalar());
                            Debug.Print("Inserted ID : " + cv_id);
                            if (cv_id > 0)
                            {
                                res[0] = 1;
                                res[1] = cv_id;
                            }
                        }
                    }

                }
                else
                {
                    res[0] = -2;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Insert User Convo Error : " + ex.Message);
                res[0] = -1;
            }
            return res;
        }


        public DataTable GetUserConvo(string id)
        {
            DataTable res = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select CV_ID, if(CV_ACC1_ID={0}, CV_ACC2_ID, CV_ACC1_ID) as CV_ACC_ID, concat(UI_FNAME, ' ', UI_LNAME) as CV_NAME, ACC_EMAIL as CV_EMAIL from user_convo 
join account on ACC_ID=if(CV_ACC1_ID={1}, CV_ACC2_ID, CV_ACC1_ID) 
join user_info on UI_ID=if(CV_ACC1_ID={2}, CV_ACC2_ID, CV_ACC1_ID) 
where CV_ACC1_ID={3} or CV_ACC2_ID={4};", id, id, id, id, id);
                da = new MySqlDataAdapter(cmd);
                da.Fill(res);
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get User Convo Error : " + ex.Message);
            }
            return res;
        }


        public DataTable GetUserMessage(string id, string cv_id)
        {
            DataTable res = new DataTable();
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"select CM_ID, CM_SENDER, CM_MESSAGE, CM_DATE, if(CM_SENDER={0}, 'Outgoing-Main', 'Incoming-Main') as CM_TYPE, 
if(CM_SENDER={1}, 'Outgoing-Design', 'Incoming-Design') as CM_DESIGN from convo_message where CM_CV_ID={2};", id, id, cv_id);
                da = new MySqlDataAdapter(cmd);
                da.Fill(res);
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Get User Message Error : " + ex.Message);
            }
            return res;
        }

        public bool InsertUserMessage(string cv_id, string msg, string id)
        {
            bool res = false;
            try
            {
                DB_Connect();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"insert into convo_message(CM_MESSAGE, CM_SENDER, CM_CV_ID) values('{0}', {1}, {2});", MySqlHelper.EscapeString(msg), id, cv_id);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    res = true;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Debug.Print("Insert User Message Error : " + ex.Message);
            }
            return res;
        }
    }
}