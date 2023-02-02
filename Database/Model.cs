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
        public string UI_BTYPE { get; set; }
        public string UI_DOB { get; set; }
        public string UI_HOME { get; set; }
        public string UI_MOBILE { get; set; }
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


    public class user_logs
    {
        public string ULOG_ID { get; set; }
        public string ULOG_EVENT { get; set; }
        public string ULOG_UACC_ID { get; set; }
        public string ULOG_DATE { get; set; }
    }

    public class blog_post
    {
        public string BLOG_ID { get; set; }
        public string BLOG_CONTENT { get; set; }
        public string BLOG_UACC_ID { get; set; }
        public string BLOG_REPORT { get; set; }
        public string BLOG_DATE { get; set; }
        public bool BLOG_STATUS { get; set; }
        public string BLOG_REPORTER { get; set; }

        //Mag agmit ug join query para ani niya nga fields
        public string BLOG_UACC_EMAIL { get; set; }
        public string BLOG_UACC_NAME { get; set; }

    }

    [Serializable]
    public class notifications
    {
        public string NTF_ID { get; set; }
        public string NTF_SUBJECT { get; set; }
        public string NTF_MESSAGE { get; set; }
        public string NTF_RECEIVER_ID { get; set; }
        public string NTF_SENDER_ID { get; set; }
        public bool NTF_STATUS { get; set; }
        public string NTF_DATE { get; set; }
    }

    [Serializable]
    public class blood_request
    {
        public string BREQ_ID { get; set; }
        public string BREQ_UACC_ID { get; set; }
        public string BREQ_JSON_SURVEY_FORM { get; set; }
        public bool BREQ_SURVEY_STATUS { get; set; }
        public bool BREQ_BLOOD_STATUS { get; set; }
        public bool BREQ_REQ_STATUS { get; set; }
        public string BREQ_CONSENT { get; set; }
        public string BREQ_VISIT_DATE { get; set; }
        public string BREQ_DATE { get; set; }
        public string BREQ_DEMAND_DATE { get; set; }
        public string BREQ_BLOOD_TYPE { get; set; }
        public string BREQ_NO_BLOOD { get; set; }

    }

    [Serializable]
    public class blood_donation
    {
        public string BD_ID { get; set; }
        public string BD_UACC_ID { get; set; }
        public string BD_JSON_SURVEY_FORM { get; set; }
        public bool BD_SURVEY_STATUS { get; set; }
        public bool BD_BLOOD_STATUS { get; set; }
        public bool BD_REQ_STATUS { get; set; }
        public string BD_DATE { get; set; }
    }

    [Serializable]
    public class request_survey_form
    {
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }

        public string barangay { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
        public string homenum { get; set; }
        public string mobilenum { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string demand_date { get; set; }
        public string bloodtype { get; set; }
        public string no_blood { get; set; }

    }

    [Serializable]
    public class DonorSurvey
    {
        public PersonalInfo personalInfo { get; set; }
        public HealthAssessment healthAssessment { get; set; }
        public RiskAssessment riskAssessment { get; set; }

        public DonorSurvey()
        {
            personalInfo = new PersonalInfo();
            healthAssessment = new HealthAssessment();
            riskAssessment = new RiskAssessment();
        }
    }

    [Serializable]
    public class PersonalInfo
    {
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
    }

    [Serializable]
    public class HealthAssessment
    {
        public string N11 { get; set; }
        public string N12 { get; set; }
        public string N13 { get; set; }
        public string N14 { get; set; }
        public string N15 { get; set; }
        public string N16a { get; set; }
        public string N16b { get; set; }
        public string N16c { get; set; }
        public string N16d { get; set; }
        public string N17 { get; set; }
        public string N18a { get; set; }
        public string N18b { get; set; }
        public string N19a { get; set; }
        public string N19b { get; set; }
        public string N19c { get; set; }
        public string N110 { get; set; }
        public string N111 { get; set; }
        public string N112 { get; set; }
        public string N113 { get; set; }
        public string N114a { get; set; }
        public string N114b { get; set; }
        public string N115 { get; set; }
    }

    [Serializable]
    public class RiskAssessment
    {
        public string N21 { get; set; }
        public string N22 { get; set; }
        public string N23 { get; set; }
        public string N24 { get; set; }
        public string N25 { get; set; }
        public string N26 { get; set; }
        public string N27a { get; set; }
        public string N27b { get; set; }
        public string N27c { get; set; }
        public string N28 { get; set; }
        public string N29 { get; set; }
        public string N210 { get; set; }
        public string N211 { get; set; }
    }
}