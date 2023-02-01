using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifePoints.Admin.Database
{
    [Serializable]
    public class bloodbank
    {
        public string BB_ID { get; set; }
        public string BB_USERNAME { get; set; }
        public string BB_PASSWORD { get; set; }
       
    }

    [Serializable]
    public class bloodbanklogs
    {
        public string BL_ID { get; set; }
        public string BL_DESCRIPTION { get; set; }
        public string BL_BB_ID { get; set; }
        public string BL_DATE { get; set; }
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

    //blog post
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
}