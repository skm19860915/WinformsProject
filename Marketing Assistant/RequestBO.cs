using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

using System.Threading;

namespace Marketing_Assistant
{
    public class RequestBO
    {
        public class NewSearchItem
        {
            public string _id { get; set; }
            public string keyword { get; set; }
            public string code { get; set; }
            public string project_id { get; set; }
            public string contact_page { get; set; }
            public string company_name { get; set; }
            public string website { get; set; }
            public string searchStatus { get; set; }
            public Boolean status { get; set; }
            public string mac_version { get; set; }
        }

        public class LoginResponse
        {
            public string status { get; set; }
            public String id { get; set; }
            public string message { get; set; }
        }

        public class ProjectQueueRootobject
        {            
            public ProjectQueue[] nearby { get; set; }            
        }


        public class ProjectQueue
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string code { get; set; }
        }

        public class ProjectQueueSorted
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string code { get; set; }
            public double distance { get; set; }
        }

        public class ProjectResponse
        {

            public string status { get; set; }
            public string project_id { get; set; }

        }
        public class SearchParam
        {

            public Int32 page { get; set; }
            public string htmldoc { get; set; }
            public string searchtxt { get; set; }
            public string website { get; set; }
            public bool url { get; set; }
            public IList<IWebElement> elements { get; set; }
            public IWebDriver driver { get; set; }
            public Int32 rank { get; set; }
            public string innerlink { get; set; }
        }

        public class ReportParam
        {
            public string project_id { get; set; }
            public string google_page { get; set; }
            public string stay_page { get; set; }
            public string visited_link { get; set; }
            public string keyword { get; set; }
            public string status { get; set; }
            public string app_version { get; set; }
            public int rank { get; set; }
        }


        public class Rootobject
        {
            public Project project { get; set; }
            public User[] users { get; set; }
            public bool status { get; set; }
            public Response resp { get; set; }

        }

        public class Project
        {
            public string _id { get; set; }
            public string new_search_item_id { get; set; }
            public string[] keywords { get; set; }
            public string name { get; set; }
            public string website { get; set; }
            public string[] company_name { get; set; }
            public string user_id { get; set; }
            public string code { get; set; }
            public string[] contact_email { get; set; }
            public string project_type { get; set; }
        }

        public class User
        {
            public string _id { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string password { get; set; }
            public string parentId { get; set; }
            public int __v { get; set; }
        }

        public class response
        {
            public string name { get; set; }
            public string email { get; set; }
            public string contact { get; set; }
            public string website { get; set; }
            public string about { get; set; }
            public string contact_name { get; set; }


        }

        public class Rootobject1
        {
            public Response response { get; set; }
            public bool status { get; set; }
        }

        public class Response
        {
            public string name { get; set; }
            public string email { get; set; }
            public string website { get; set; }
            public string contact_1 { get; set; }
            public string about { get; set; }
            public string contact_name { get; set; }
            public string show_custom_message { get; set; }
            public string custom_message { get; set; }
        }


        public class Rootobj
        {
            public bool status { get; set; }
            public string lastReport { get; set; }
            public string report_url { get; set; }
            public float page_load_time { get; set; }
            public string site_status { get; set; }
            public Keywordwise[] KeywordWise { get; set; }
        }

        public class Keywordwise
        {
            public string keyword { get; set; }
            public string date { get; set; }
            public string rank { get; set; }
            public int position { get; set; }
        }


    }
}


