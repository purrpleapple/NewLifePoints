using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifePoints.Database
{
    [Serializable]
    public class account
    {
        public string ACC_ID { get; set; }
        public string ACC_EMAIL { get; set; }
        public string ACC_PASSWORD { get; set; }
        public string ACC_TYPE { get; set; } //1 for Admin, 2 for Blood Bank, 3 for User
        public bool ACC_REQUESTOR { get; set; }
        public bool ACC_DONOR { get; set; }
        public bool ACC_STATUS { get; set; }
    }
    [Serializable]
    public class user_info
    {
        public string UI_ID { get; set; }
        public string UI_LNAME { get; set; }
        public string UI_FNAME { get; set; }
        public string UI_MNAME { get; set; }
        public bool UI_GENDER { get; set; }
        public string UI_DOB { get; set; }
        public string UI_ADDRESS { get; set; }

    }
    [Serializable]
    public class user_info_address
    {
        public string street { get; set; }
        public string baranggay { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
    }

    [Serializable]
    public class activity_logs
    {
        public string ACT_ID { get; set; }
        public string ACT_DESCRIPTION { get; set; }
        public string ACT_UACC_ID { get; set; }
        public string ACT_UNAME { get; set; }
        public string ACT_DATE { get; set; }
    }





}