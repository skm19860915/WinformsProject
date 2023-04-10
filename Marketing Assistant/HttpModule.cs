using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using testc = OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Web;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Marketing_Assistant
{
    public class HttpModule
    {
        public ChromeDriver driver;
        public string getAppVersionFromAPI()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = client.PostAsync("https://my.site-pop.com/get_search_item/google", null).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<RequestBO.NewSearchItem>(responseString);
                var version = data.mac_version;

                return version;
            }
            catch (Exception ex)
            {
                return "Not Found";
            }
        }

        public RequestBO.LoginResponse LoginAutho(string email, string project)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                            {
                               { "username", email },
                               { "project_code", project }
                            };

            var contents = new FormUrlEncodedContent(values);
            RequestBO.LoginResponse user=new RequestBO.LoginResponse();
            try
            {
                var response = client.PostAsync("https://my.site-pop.com:8081/auth_login", contents).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                user = JsonConvert.DeserializeObject<RequestBO.LoginResponse>(responseString);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception LoginAutho(), trying again " + ex.ToString());
                Console.WriteLine("Exception LoginAutho(), trying again " + ex.ToString());
                LoginAutho(email, project);

            }
            if (user.status == "true")
                return (user);
            else
                user.status = "false";
            return (user);
        }

        //function to get public IP Address
        static string GetIPAddress()
        {
            String address = "";

            try
            {
                WebRequest request = WebRequest.Create("https://api.ipify.org");
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    address = stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception GetIPAddress(), trying again " + ex.ToString());
                Console.WriteLine("Exception GetIPAddress(), trying again " + ex.ToString());
                GetIPAddress();
            }

            //try
            //{
            //    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            //using (WebResponse response = request.GetResponse())
            //using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            //{
            //    address = stream.ReadToEnd();
            //}
            //}
            //catch (Exception ex)
            //{
            //    Thread.Sleep(2000);
            //    Debug.WriteLine("Exception GetIPAddress(), trying again " + ex.ToString());
            //    Console.WriteLine("Exception GetIPAddress(), trying again " + ex.ToString());
            //    GetIPAddress();
            //}

            //int first = address.IndexOf("Address: ") + 9;
            //int last = address.LastIndexOf("</body>");
            //address = address.Substring(first, last - first);

            return address;
        }


        //function to get public LAT/LONG Address
        static string GetLatLongAddress()
        {
            string address2 = null;
            String address = GetIPAddress();
            try
            {
                String reqString = "http://api.ipstack.com/ip_add?access_key=18b76769194fdbe8b4a1a7f4be38be22&format=1";
            reqString = reqString.Replace("ip_add", address);

            WebRequest request = WebRequest.Create(reqString);
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address2 = stream.ReadToEnd();
            }
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception GetLatLongAddress(), trying again " + ex.ToString());
                Console.WriteLine("Exception GetLatLongAddress(), trying again " + ex.ToString());
                GetLatLongAddress();
            }

            return address2;
        }

        public static String GetLatAddress()
        {
            String address2 = GetLatLongAddress();
            var latLongFormat = JObject.Parse(address2);
            var lat = (string)latLongFormat["latitude"];

            return lat;
        }


        public static String GetLongitudeAddress()
        {
            String address2 = GetLatLongAddress();
            var latLongFormat = JObject.Parse(address2);
            var longitude = (string)latLongFormat["longitude"];

            return longitude;
        }

        //function to get PC Mac Address
        public string GetMacAddress()
        {
            String firstMacAddress = "";
            firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();

            if (String.IsNullOrEmpty(firstMacAddress))
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        nic.OperationalStatus == OperationalStatus.Up)
                    {
                        return nic.GetPhysicalAddress().ToString();
                    }
                }
                return null;
            }

            return firstMacAddress;
        }

        public RequestBO.Rootobject ProjectAuth(string projcode, string id)
        {
            HttpClient client = new HttpClient();


            TimeZoneInfo localZone = TimeZoneInfo.Local;

            Debug.WriteLine("Display Name: " + localZone.DisplayName.Substring(0, 11));
            Console.WriteLine("Display Name: " + localZone.DisplayName.Substring(0, 11));
            String versionURL = "Windows_X";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                versionURL = "Windows_" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            var values = new Dictionary<string, string>
            {
                { "user_id", id },
                {"ip",GetIPAddress() },
                {"type","windows" },
                {"device_id",GetMacAddress() },
                {"device_mode",versionURL },
                {"lat",GetLatAddress() },
                {"long",GetLongitudeAddress() },
                {"timeZone",localZone.DisplayName.Substring(0, 11)},
            };

            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/detailProjectApp/";
            RequestBO.Rootobject user = new RequestBO.Rootobject();
            try
            {
                var response = client.PostAsync(url + projcode, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            user = JsonConvert.DeserializeObject<RequestBO.Rootobject>(responseString);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception ProjectAuth(), trying again " + ex.ToString());
                Console.WriteLine("Exception ProjectAuth(), trying again " + ex.ToString());
                ProjectAuth(projcode, id);
            }
            if (user.project.keywords.ToString() != "")
                return (user);
            else
                return (user);

        }


        //function to get today's project queue list
        public RequestBO.ProjectQueueRootobject getProjectQueue()
        {
            HttpClient client = new HttpClient();

            RequestBO.ProjectQueueRootobject projectQueues = new RequestBO.ProjectQueueRootobject();

            TimeZoneInfo localZone = TimeZoneInfo.Local;

            Debug.WriteLine("Project Queue: " + localZone.DisplayName.Substring(0, 11));
            Console.WriteLine("Project Queue: " + localZone.DisplayName.Substring(0, 11));

            var values = new Dictionary<string, string>
            {
                { "user_id", "" },
            };

            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/get_today_queue";
            try
            {
                var response = client.PostAsync(url, content).Result;

                string responseString = response.Content.ReadAsStringAsync().Result;

                projectQueues = JsonConvert.DeserializeObject<RequestBO.ProjectQueueRootobject>(responseString);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception getProjectQueue(), trying again " + ex.ToString());
                Console.WriteLine("Exception getProjectQueue(), trying again " + ex.ToString());
                getProjectQueue();
            }
            return projectQueues;

        }

        ////function to get project keywords by project code
        //public RequestBO.Project getProjectKeywords(string code)
        //{

        //    HttpClient client = new HttpClient();

        //    RequestBO.Rootobject root = new RequestBO.Rootobject();

        //    TimeZoneInfo localZone = TimeZoneInfo.Local;

        //    Debug.WriteLine("Project Keywords: " + localZone.DisplayName.Substring(0, 11));
        //    Console.WriteLine("Project Keywords: " + localZone.DisplayName.Substring(0, 11));

        //    var values = new Dictionary<string, string>
        //    {
        //        { "user_id", "" },
        //    };

        //    HttpContent content = new FormUrlEncodedContent(values);
        //    String url = "https://my.site-pop.com:8081/projectByCode/" + code;
        //    try
        //    {
        //        var response = client.PostAsync(url, content).Result;

        //        string responseString = response.Content.ReadAsStringAsync().Result;

        //        Debug.WriteLine("Got keywords for project " + code);
        //        Console.WriteLine("Got keywords for project " + code);

        //        root = JsonConvert.DeserializeObject<RequestBO.Rootobject>(responseString);
        //    }
        //    catch (Exception ex)
        //    {
        //        Thread.Sleep(2000);
        //        Debug.WriteLine("Exception getProjectKeywords(), trying again " + ex.ToString());
        //        Console.WriteLine("Exception getProjectKeywords(), trying again " + ex.ToString());
        //        //getProjectKeywords(code);
        //    }
        //    return root.project;

        //}

        //function to run thread in background every15 minutes to hit link
        public void HitLinkEvery15Minutes(string projcode, string id)
        {
            ProjectAuth(projcode, id);
        }

        public RequestBO.Rootobject GetYoutubeData()
        {
            string url = "https://my.site-pop.com/migration/newscheduler/youtube/";

            HttpClient client = new HttpClient();
            try
            {
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic list = JObject.Parse(responseString);
                var data = new RequestBO.Rootobject();
                var project = new RequestBO.Project()
                {
                    company_name = new string[] { list.company_name },
                    keywords = new string[] { list.keyword },
                    website = list.website,
                    project_type = "youtube"
                };
                data.project = project;
                var resp = new RequestBO.Response()
                {
                    contact_name = list.contact_page,
                    website = list.website,
                    contact_1 = list.company_name,
                    show_custom_message = "N",
                };
                data.resp = resp;

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RequestBO.Rootobject GetAmazonData()
        {
            string url = "https://my.site-pop.com/migration/newscheduler/amazon";

            HttpClient client = new HttpClient();
            try
            {
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic list = JObject.Parse(responseString);
                var data = new RequestBO.Rootobject();
                var project = new RequestBO.Project()
                {
                    company_name = new string[] { list.company_name },
                    keywords = new string[] { list.keyword },
                    website = list.website,
                    project_type = "amazon",
                    _id = list.project_id
                };
                data.project = project;
                var resp = new RequestBO.Response()
                {
                    contact_name = list.contact_page,
                    website = list.website,
                    contact_1 = list.company_name,
                    show_custom_message = "N",
                };
                data.resp = resp;

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RequestBO.Rootobject GetYelpData()
        {
            string url = "https://my.site-pop.com/migration/newscheduler/yelp";

            HttpClient client = new HttpClient();
            try
            {
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic list = JObject.Parse(responseString);
                var data = new RequestBO.Rootobject();
                var project = new RequestBO.Project()
                {
                    company_name = new string[] { list.company_name },
                    keywords = new string[] { list.keyword },
                    website = list.website,
                    project_type = "yelp",
                    _id = list.project_id
                };
                data.project = project;
                var resp = new RequestBO.Response()
                {
                    contact_name = list.contact_page,
                    website = list.website,
                    contact_1 = list.company_name,
                    show_custom_message = "N",
                    about = list.rule.initial[4].text // location keyword of yelp
                };
                data.resp = resp;

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RequestBO.Rootobject1 AgenyDetails(string projcode, string id)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {

                { "user_id", id },
                {"project_id",projcode }

            };
            RequestBO.Rootobject1 resp = new RequestBO.Rootobject1();
            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/agency/details/";
            try
            {
                var response = client.PostAsync(url + id, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            resp = JsonConvert.DeserializeObject<RequestBO.Rootobject1>(responseString);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception AgenyDetails(), trying again " + ex.ToString());
                AgenyDetails(projcode, id);
            }
            if (resp.status == true)
                return (resp);
            else
                return (resp);

        }

        
        public void PerformLogout()
        {
           
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
            //{"loginId", marketing_assistant.Properties.Settings.Default.user_device_id},
            };

            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/logout/" + marketing_assistant.Properties.Settings.Default.user_device_id;
            try
            {
                //var response = client.PostAsync(url, content).Result;
                var response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Logout(), trying again " + ex.ToString());
                Console.WriteLine("Exception Logout(), trying again " + ex.ToString());
                //How about instead of qt infinite loop, we just try once more
                //PublishReport(objRep);

                Thread.Sleep(3000);
                try
                {
                    var response = client.PostAsync(url, content).Result;
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                    Console.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                }
            }

        }

        public RequestBO.NewSearchItem GetNewSearchItem()
        {
            RequestBO.NewSearchItem returnItem = null;
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>{};
            values.Add("device_id", GetMacAddress());

            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/get_search_item";

            //content.Headers.Add("Accept", "");
            //content.Headers.Add("Accept-Encoding", "gzip, deflate");
            //content.Headers.Add("Cache-Control", "no-cache");
            //content.Headers.Add("Connection", "keep-alive");
            //content.Headers.Add("Content-Length", "50");
            //content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //content.Headers.Add("Host", "my.site-pop.com:8081");
            //content.Headers.Add("Postman-Token", "3a98438e-d1ba-4b37-8f9a-84adbf107f9b,14a1b233-6546-43e3-9261-6be5e5b0e032");
            //content.Headers.Add("cache-control", "no-cache");

            try
            {
                var response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                //CHECK FOR ERROR INSIDE OF RESPONSE STRING
                //  due to jenky API, errors will always come back with an HTTP 200 - this is the only one being sent with this new
                //  API call, so we'll just check for it here before it blows up on deserialization.
                if (!String.IsNullOrEmpty(responseString) && responseString.Contains("Queue is Empty Right now"))
                {
                    returnItem = new RequestBO.NewSearchItem();
                    returnItem.code = "EMPTY___QUEUE";
                    return returnItem;
                }

                returnItem = JsonConvert.DeserializeObject<RequestBO.NewSearchItem>(responseString);

                if (returnItem != null && !string.IsNullOrEmpty(returnItem.company_name) && returnItem.company_name.Contains(","))
                {
                    string[] companyNames = returnItem.company_name.Split(',');
                    returnItem.company_name = companyNames[0];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception GetNewSearchItem(), trying again " + ex.ToString());
                Console.WriteLine("Exception GetNewSearchItem(), trying again " + ex.ToString());

                Thread.Sleep(3000);
                try
                {
                    var response = client.PostAsync(url, content).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;

                    //CHECK FOR ERROR INSIDE OF RESPONSE STRING
                    //  due to jenky API, errors will always come back with an HTTP 200 - this is the only one being sent with this new
                    //  API call, so we'll just check for it here before it blows up on deserialization.
                    if (!String.IsNullOrEmpty(responseString) && responseString.Contains("Queue is Empty Right now"))
                    {
                        returnItem = new RequestBO.NewSearchItem();
                        returnItem.code = "EMPTY___QUEUE";
                        return returnItem;
                    }

                    returnItem = JsonConvert.DeserializeObject<RequestBO.NewSearchItem>(responseString);
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine("Failed second GetNewSearchItem(), exiting function " + ex2.ToString());
                    Console.WriteLine("Failed second GetNewSearchItem(), exiting function " + ex2.ToString());
                }
            }

            return returnItem;
        }

        public static void MarkNewSearchItemFinished(string url)
        {
            RequestBO.NewSearchItem returnItem = null;
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                //nothing in the body..
            };

            HttpContent content = new FormUrlEncodedContent(values);
            
            //String url = "https://my.site-pop.com:8081/mark_search_finished/?searchItem=" + searchid;

            //NONE of these headers are actually required - all data sent is in query string already appended to URL in call to this function

            //content.Headers.Add("Accept", "");
            //content.Headers.Add("Accept-Encoding", "gzip, deflate");
            //content.Headers.Add("Cache-Control", "no-cache");
            //content.Headers.Add("Connection", "keep-alive");
            //content.Headers.Add("Content-Length", "50");
            //content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //content.Headers.Add("Host", "my.site-pop.com:8081");
            //content.Headers.Add("Postman-Token", "3a98438e-d1ba-4b37-8f9a-84adbf107f9b,14a1b233-6546-43e3-9261-6be5e5b0e032");
            //content.Headers.Add("cache-control", "no-cache");

            try
            {
                var response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("MarkNewSearchItemFinished() complete " + responseString);
                Console.WriteLine("MarkNewSearchItemFinished() complete " + responseString);
                //returnItem = JsonConvert.DeserializeObject<RequestBO.NewSearchItem>(responseString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception MarkNewSearchItemFinished(), trying again " + ex.ToString());
                Console.WriteLine("Exception MarkNewSearchItemFinished(), trying again " + ex.ToString());

                Thread.Sleep(3000);
                try
                {
                    var response = client.PostAsync(url, content).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    returnItem = JsonConvert.DeserializeObject<RequestBO.NewSearchItem>(responseString);
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine("Failed second MarkNewSearchItemFinished(), exiting function " + ex2.ToString());
                    Console.WriteLine("Failed second MarkNewSearchItemFinished(), exiting function " + ex2.ToString());
                }
            }
        }
        
        public RequestBO.ReportParam PublishReport(RequestBO.ReportParam objRep, string project_type)
        {
            int positionNumber = 0;
            RequestBO.ReportParam user = new RequestBO.ReportParam();
            if (objRep.project_id != null)
            {
                if(string.IsNullOrEmpty(project_type))
                    positionNumber = getRankPositionFromGoogle(objRep.visited_link, objRep.keyword, objRep.google_page);
                else
                {
                    switch (project_type.ToLower())
                    {
                        case "google":
                            positionNumber = getRankPositionFromGoogle(objRep.visited_link, objRep.keyword, objRep.google_page);
                            break;
                        case "bing":
                            break;
                        case "yelp":
                            break;
                        case "amazon":
                            break;
                        case "youtube":
                            break;
                        default:
                            positionNumber = getRankPositionFromGoogle(objRep.visited_link, objRep.keyword, objRep.google_page);
                            break;
                    }
                }

                HttpClient client = new HttpClient();

                //String versionURL = "Windows_X";
                //if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                //{
                //    versionURL = "Windows_" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                //}
                
                //TODO - can I change "type"????
                //yes - but we need to implement the regex in node where it's commented out right now.
                var values = new Dictionary<string, string>
                {

                    { "project_id", objRep.project_id},
                    {"found_on_page",objRep.google_page },
                    {"stay_page",objRep.stay_page },
                    {"visited_link",objRep.visited_link },
                    {"keyword",objRep.keyword },
                    {"maxSearchPages","30" },
                    {"ip",GetIPAddress()},
                    {"type","windows" }, //versionURL
                    {"device_id", GetMacAddress()},
                    {"rank", positionNumber.ToString() },
                    {"app_version", objRep.app_version }
                };
                
                HttpContent content = new FormUrlEncodedContent(values);
                String url = "https://my.site-pop.com:8081/report/put";
                try
                {
                    var response = client.PostAsync(url, content).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<RequestBO.ReportParam>(responseString);

                    Debug.WriteLine("----------------------- Report Result -----------------------------");
                    Console.WriteLine("----------------------- Report Result -----------------------------");
                    Debug.WriteLine("Report: keyword - {0}, rank - {1}, found_on_page - {2}, app_version - {3}, website - {4}", 
                        objRep.keyword, positionNumber, objRep.google_page, objRep.app_version, objRep.visited_link);
                    Console.WriteLine("Report: keyword - {0}, rank - {1}, found_on_page - {2}, app_version - {3}, website - {4}",
                        objRep.keyword, positionNumber, objRep.google_page, objRep.app_version, objRep.visited_link);
                    Debug.WriteLine("----------------------- Report Result End -----------------------------");
                    Console.WriteLine("----------------------- Report Result End -----------------------------");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception PublishReport(), trying again " + ex.ToString());
                    Console.WriteLine("Exception PublishReport(), trying again " + ex.ToString());
                    //How about instead of qt infinite loop, we just try once more
                    //PublishReport(objRep);

                    Thread.Sleep(3000);
                    try
                    {
                        var response = client.PostAsync(url, content).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        user = JsonConvert.DeserializeObject<RequestBO.ReportParam>(responseString);
                    }
                    catch (Exception ex2)
                    {
                        Debug.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                        Console.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                    }
                }
            }

            return (user);
        }

        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        private ChromeDriver getInstanceOfChromeDriver()
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                if (!File.Exists(chromePath))
                    chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

                chromeOptions.BinaryLocation = chromePath;
                chromeOptions.AddArguments("--headless");
                chromeOptions.AddArguments("--no-sandbox");
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(appPath);
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;

                // additional
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                chromeOptions.AddArgument("start-maximized");
                chromeOptions.AddArgument("disable-infobars");
                chromeOptions.AddArgument("--disable-extensions");

                driver = new ChromeDriver(service, chromeOptions);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

                return driver;
            }
            catch(Exception e)
            {
                Debug.WriteLine("exception error {0}", e);
                return null;
            }
        }

        public RequestBO.Rootobj RankDetailsOfGoogle(string siteUrl, string projcode)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                {"project_id", projcode}
            };

            RequestBO.Rootobj resp = new RequestBO.Rootobj();
            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/project-statistics/";
            try
            {
                var response = client.PostAsync(url + projcode, content).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                var rankCount = 0;

                while (responseString.IndexOf("N/A") > 0){
                    responseString = ReplaceFirst(responseString, "N/A", rankCount.ToString());
                    //Originally wanted to order these, but this rank # ends up appearing in the UI as the actual rank number. Will want to fix that/change 0 to "unranked" -csv
                    //rankCount += 1;
                }

                resp = JsonConvert.DeserializeObject<RequestBO.Rootobj>(responseString);

                resp = GetResponseWithPositionFromGoogle(siteUrl, resp);
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Debug.WriteLine("Exception RankDetails(), trying again " + ex.ToString());
                Console.WriteLine("Exception RankDetails(), trying again " + ex.ToString());
                RankDetailsOfGoogle(siteUrl, projcode);
            }

            //if (resp.status == true)
            //    return (resp);
            //else
            //    return (resp);

            return (resp);
        }

        private RequestBO.Rootobj GetResponseWithPositionFromGoogle(string siteUrl, RequestBO.Rootobj resp)
        {
            var keywordWise = resp.KeywordWise;
            driver = getInstanceOfChromeDriver();
            foreach(var k in keywordWise)
            {
                var position = getRankPositionFromGoogle(siteUrl, k.keyword, k.rank);
                k.position = position;
                if (k.rank == "0")
                    k.rank = "N/A";
            }
            driver.Quit();

            return resp;
        }

        private int getRankPositionFromGoogle(string url, string keyword, string pageNumber)
        {
            int number = 0;
            try
            {
                number = Int32.Parse(pageNumber);
            }
            catch
            {
                number = 0;
            }
            
            if (number == 0)
                return 0;

            var host = getUrlHost(url);

            int position = 0;
            if (driver == null)
                driver = getInstanceOfChromeDriver();
            
            driver.Navigate().GoToUrl("http://www.google.com/");
            var searchElement = driver.FindElement(By.Name("q"));
            searchElement.SendKeys(keyword);
            searchElement.Submit();

            int clickLinkNumber;

            var isNumber = int.TryParse(pageNumber, out clickLinkNumber);
            if (!isNumber)
                return 0;

            var format = "table.AaVjTc>tbody>tr>td";
            var paginationList = driver.FindElements(By.CssSelector(format));

            if (paginationList != null && paginationList.Count() > 0)
            {
                var selectPageNumber = paginationList.FirstOrDefault(x => x.Text.Equals(clickLinkNumber.ToString()));
                if (selectPageNumber == null)
                    return 0;

                selectPageNumber.Click();
                var elements = driver.FindElements(By.CssSelector("div.yuRUbf>a>div>cite"));
                if (elements != null && elements.Count() > 0)
                {
                    int index = 0;
                    foreach (var e in elements)
                    {
                        if (e.Text.Contains(host))
                        {
                            position = index + 1;
                            break;
                        }
                        index++;
                    }
                }
            }

            return position;
        }

        private string getUrlHost(string url)
        {
            if (!url.Contains("http://") || !url.Contains("https://"))
                url = "https://" + url;

            var uri = new Uri(url);
            var host = uri.Host;

            return host;
        }

        //private int getRankPositionFromGoogle(string url, string keyword, string pageNumber)
        //{
        //    if (!url.Contains("http://") || !url.Contains("https://"))
        //        url = "https://" + url;

        //    var uri = new Uri(url);
        //    var raw = "https://www.google.com/search?q={0}&start={1}";
        //    int startNum = GetPageStartNumberFromGoogle(pageNumber);
        //    var search = string.Format(raw, HttpUtility.UrlEncode(keyword), startNum);

        //    driver.Navigate().GoToUrl(search);
        //    string source = driver.PageSource;

        //    var index = FindPositionFromGoogle(source, uri);

        //    return index < 0 ? 0 : startNum + index;
        //}

        private int GetPageStartNumberFromGoogle(string pageNumber)
        {
            try
            {
                int number = int.Parse(pageNumber);
                if (number > 0)
                    return (number - 1) * 10;
                return 0;
            }
            catch (FormatException e)
            {
                return 0;
            }
        }

        private int FindPositionFromGoogle(string html, Uri uri)
        {
            var lookup = "(<div class=\"yuRUbf\">)(<a href=\"https?://)(.*?)\" data-ved=\"";
            MatchCollection matches = Regex.Matches(html, lookup);

            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i].Groups[3].Value;
                if (match.Contains(uri.Host))
                    return i + 1;
            }

            return -1;
        }

        public RequestBO.Rootobj RankDetailsOfYoutube(string[] keywords, string siteUrl, string contact_page)
        {
            var obj = new RequestBO.Rootobj();
            var keywordwises = new RequestBO.Keywordwise[keywords.Length];
            int index = 0;
            if(keywords != null && keywords.Length > 0)
            {
                if(driver == null)
                    driver = getInstanceOfChromeDriver();

                // step 1
                foreach (var keyword in keywords)
                {
                    var record = new RequestBO.Keywordwise()
                    {
                        position = 0,
                        rank = "0",
                        keyword = keyword
                    };
                    var item = GetRealSearchResultOfYoutube(keyword, siteUrl);

                    if (item > 0)
                    {
                        record.position = item;
                        record.rank = "1";
                        record.keyword = keyword;

                        Debug.WriteLine("Youtube Data - {0}", item);
                        Console.WriteLine("Youtube Data - {0}", item);
                    }

                    keywordwises[index] = record;
                    index++;
                }

                // step 2
                //foreach (var keyword in keywords)
                //{
                //    var item = GetDetailSearchResultOfYoutube(keyword, siteUrl, contact_page);

                //    var record = new RequestBO.Keywordwise();
                //    record.keyword = keyword;
                //    if(item == 0)
                //    {
                //        record.position = 0;
                //        record.rank = "Not Found";
                //    }
                //    else
                //    {
                //        record.position = item;
                //        record.rank = "Found";
                //    }
                //    keywordwises[index] = record;
                //    index++;
                //}
                obj.KeywordWise = keywordwises;
                driver.Quit();

                Debug.WriteLine("close driver---------- youtube");
                Console.WriteLine("close driver---------- youtube");
                return obj;
            }

            return null;
        }

        private int GetRealSearchResultOfYoutube(string keyword, string siteUrl)
        {
            int position = 0;
            if (driver == null)
                driver = getInstanceOfChromeDriver();

            driver.Navigate().GoToUrl("https://www.youtube.com/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1500));
            var elementsWithSearchID = wait.Until((driver) => driver.FindElements(By.Id("search")));
            var search = elementsWithSearchID.Where(e => e.TagName == "input").FirstOrDefault();

            //var typing = keyword + "\n"; // the same like enter function
            //search.SendKeys(typing);

            driver.FindElement(By.Id("search")).SendKeys(keyword);
            driver.FindElement(By.Id("search-icon-legacy")).Click();

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            var list = driver.FindElements(By.XPath("//ytd-video-renderer/div[@id='dismissible']/ytd-thumbnail/a"));

            long initialHeight = (long)jse.ExecuteScript("return document.documentElement.scrollHeight");

            while (true)
            {
                jse.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight)");
                Thread.Sleep(2000);
                long currentHeight = (long)jse.ExecuteScript("return document.documentElement.scrollHeight");
                if (initialHeight == currentHeight)
                    break;

                list = driver.FindElements(By.XPath("//ytd-video-renderer/div[@id='dismissible']/ytd-thumbnail/a"));

                initialHeight = currentHeight;
            }

            if (list != null && list.Count() > 0)
            {
                int index = 0;
                foreach (var e in list)
                {
                    var href = e.GetAttribute("href");
                    if (href.Contains(siteUrl))
                    {
                        position = index + 1;
                        break;
                    }
                    index++;
                }
            }

            return position;
        }

        private int GetDetailSearchResultOfYoutube(string keyword, string siteUrl, string contact_page)
        {
            int position = 0;
            if (driver == null)
                driver = getInstanceOfChromeDriver();

            //driver.Navigate().GoToUrl("http://www.youtube.com/");
            //var searchElement = driver.FindElement(By.Id("search-icon-legacy"));
            //var baseKeyword = keyword + " " + contact_page;
            //searchElement.SendKeys(baseKeyword);
            //searchElement.Click();

            driver.Navigate().GoToUrl("http://www.youtube.com/");
            var baseKeyword = keyword + " " + contact_page;
            driver.FindElement(By.Id("search")).SendKeys(baseKeyword);
            driver.FindElement(By.Id("search-icon-legacy")).Click();

            var format = "ytd-video-renderer>div#dismissible>ytd-thumbnail>a";
            var list = driver.FindElements(By.CssSelector(format));
            if (list != null && list.Count() > 0)
            {
                int index = 0;
                foreach (var e in list)
                {
                    var href = e.GetAttribute("href");
                    if (href.Contains(siteUrl))
                    {
                        position = index + 1;
                        break;
                    }
                    index++;
                }
            }

            return position;
        }

        public RequestBO.Rootobj RankDetailsOfAmazon(string[] keywords, string siteUrl, string contact_page)
        {
            var obj = new RequestBO.Rootobj();
            var keywordwises = new RequestBO.Keywordwise[keywords.Length];
            int index = 0;
            if (keywords != null && keywords.Length > 0)
            {
                if (driver == null)
                    driver = getInstanceOfChromeDriver();

                // step 1
                foreach(var keyword in keywords)
                {
                    var record = new RequestBO.Keywordwise()
                    {
                        position = 0,
                        rank = "0",
                        keyword = keyword,
                        date = "0"
                    };
                    var item = GetRealSearchResultOfAmazon(keyword, siteUrl, 1);

                    if (item == null)
                    {
                        string mixed = "";
                        var strs = siteUrl.Split('/');
                        if(strs.Length > 0)
                            mixed = keyword + " " + strs[0];

                        item = GetRealSearchResultOfAmazon(mixed, siteUrl, 1);
                        if(item != null)
                        {
                            var data = item.Split('-');
                            record.position = Convert.ToInt32(data[0]);
                            record.rank = data[1];
                            record.keyword = keyword; // mixed;
                            record.date = data[2];
                        }
                        else
                        {
                            record.position = 0;
                            record.rank = "0";
                            record.keyword = keyword; // mixed;
                            record.date = "0";
                        }
                    }
                    else
                    {
                        var data = item.Split('-');
                        record.position = Convert.ToInt32(data[0]);
                        record.rank = data[1];
                        record.keyword = keyword;
                        record.date = data[2];
                    }

                    Debug.WriteLine("Amazon Data - {0}", item);
                    Console.WriteLine("Amazon Data - {0}", item);

                    keywordwises[index] = record;
                    index++;
                }

                // step 2
                //foreach (var keyword in keywords)
                //{
                //    var item = GetDetailSearchResultOfAmazon(keyword, siteUrl, contact_page, 1);

                //    var record = new RequestBO.Keywordwise();
                //    record.keyword = keyword;
                //    if (item == 0)
                //    {
                //        record.position = 0;
                //        record.rank = "Not Found";
                //    }
                //    else
                //    {
                //        record.position = item;
                //        record.rank = "Found";
                //    }
                //    keywordwises[index] = record;
                //    index++;
                //}
                obj.KeywordWise = keywordwises;
                driver.Quit();

                Debug.WriteLine("close driver---------- amazon");
                Console.WriteLine("close driver---------- amazon");
                return obj;
            }

            return null;
        }

        private string GetRealSearchResultOfAmazon(string keyword, string siteUrl, int pageNumber)
        {
            var startTime = DateTime.Now;
            string result = null;
            int position = 1;
            bool errorFound = false;

            if (driver == null)
                driver = getInstanceOfChromeDriver();

            if (pageNumber == 1)
            {
                driver.Navigate().GoToUrl("http://www.amazon.com/");
                driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(keyword);
                driver.FindElement(By.Id("nav-search-submit-text")).Click();
                Thread.Sleep(RandomNumber(1000, 1500));
            }

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight)");
            Thread.Sleep(2000);

            var elements = driver.FindElements(By.XPath("//*[@class='a-link-normal a-text-normal']"));

            if (elements != null && elements.Count() > 0)
            {
                foreach (var e in elements)
                {
                    var href = e.GetAttribute("href");
                    if (href.Contains(siteUrl) || href.Contains(Uri.EscapeDataString(siteUrl)))
                    {
                        TimeSpan timeElapsed = DateTime.Now - startTime;
                        result = position + "-" + pageNumber + "-" + timeElapsed.TotalMilliseconds.ToString("00000");
                        break;
                    }
                    position++;
                }
            }

            if(result == null)
            {
                elements = driver.FindElements(By.XPath("//*[@class='a-link-normal s-underline-text s-underline-link-text a-text-normal']"));
                position = 1;

                if (elements != null && elements.Count() > 0)
                {
                    foreach (var e in elements)
                    {
                        var href = e.GetAttribute("href");
                        if (href.Contains(siteUrl) || href.Contains(Uri.EscapeDataString(siteUrl)))
                        {
                            TimeSpan timeElapsed = DateTime.Now - startTime;
                            result = position + "-" + pageNumber + "-" + timeElapsed.TotalMilliseconds.ToString("00000");
                            break;
                        }
                        position++;
                    }
                }
            }

            if (result != null)
                return result;

            try
            {
                if (driver != null)
                {
                    //Thread.Sleep(RandomNumber(5000, 10000));
                    driver.FindElement(By.XPath("//*[@class='a-pagination']/li[last()]")).Click();
                    Thread.Sleep(2000);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Step1 : Next Page is not found in amazon search-----------{0}", pageNumber);
                Console.WriteLine("Step1 : Next Page is not found in amazon search-----------{0}", pageNumber);
                errorFound = true;
            }

            if (errorFound)
            {
                try
                {
                    errorFound = false;

                    if (driver != null)
                    {
                        //Thread.Sleep(RandomNumber(5000, 10000));
                        driver.FindElement(By.XPath("//*[@class='a-pagination']/li[@class='a-last']")).Click();
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Step1 : Next Page is not found in amazon search-----------{0}", pageNumber);
                    Console.WriteLine("Step1 : Next Page is not found in amazon search-----------{0}", pageNumber);
                    errorFound = true;
                }
            }

            if (errorFound)
                return null;

            pageNumber++;
            Thread.Sleep(RandomNumber(2000, 5000));

            if (pageNumber > 10)
                return null;

            return GetRealSearchResultOfAmazon(keyword, siteUrl, pageNumber);
        }

        private int GetDetailSearchResultOfAmazon(string keyword, string siteUrl, string contact_page, int pageNumber)
        {
            int position = 0;
            if (driver == null)
                driver = getInstanceOfChromeDriver();

            if(pageNumber == 1)
            {
                driver.Navigate().GoToUrl("http://www.amazon.com/");
                var baseKeyword = keyword + " " + contact_page;
                driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(baseKeyword);
                driver.FindElement(By.Id("nav-search-submit-text")).Click();
                Thread.Sleep(1000);
            }

            var elements = driver.FindElements(By.XPath("//*[@class='a-link-normal a-text-normal']"));
            if (elements == null || elements.Count() == 0)
                return 0;

            foreach(var e in elements)
            {
                var href = e.GetAttribute("href");
                if (href.Contains(siteUrl))
                {
                    position = 1;
                    break;
                }
            }

            if (position > 0)
                return position;

            try
            {
                if(driver != null)
                {
                    driver.FindElement(By.XPath("//*[@class='a-pagination']/li[last()]")).Click();
                }
            }
            catch
            {
                driver.FindElement(By.ClassName("a-last")).Click();
            }
            Thread.Sleep(1000);
            pageNumber++;

            if (pageNumber > 10)
                return 0;

            GetDetailSearchResultOfAmazon(keyword, siteUrl, contact_page, pageNumber);

            return 0;
        }

        public RequestBO.Rootobj RankDetailsOfYelp(string[] keywords, string siteUrl, string contact_page, string location)
        {
            var obj = new RequestBO.Rootobj();
            var keywordwises = new RequestBO.Keywordwise[keywords.Length];
            int index = 0;
            if (keywords != null && keywords.Length > 0)
            {
                if (driver == null)
                    driver = getInstanceOfChromeDriver();

                // step 1
                foreach (var keyword in keywords)
                {
                    var record = new RequestBO.Keywordwise()
                    {
                        position = 0,
                        rank = "0",
                        keyword = keyword
                    };
                    var item = GetRealSearchResultOfYelp(keyword, siteUrl, location, 1);

                    if (item != null)
                    {
                        var data = item.Split('-');
                        record.position = Convert.ToInt32(data[0]);
                        record.rank = data[1];
                        record.keyword = keyword;

                        Debug.WriteLine("Yelp Data - {0}", item);
                        Console.WriteLine("Yelp Data - {0}", item);
                    }

                    keywordwises[index] = record;
                    index++;
                }

                // step 2
                //foreach (var keyword in keywords)
                //{
                //    var item = GetDetailSearchResultOfYelp(keyword, siteUrl, contact_page, location, 1);

                //    var record = new RequestBO.Keywordwise();
                //    record.keyword = keyword;
                //    if (item == 0)
                //    {
                //        record.position = 0;
                //        record.rank = "Not Found";
                //    }
                //    else
                //    {
                //        record.position = item;
                //        record.rank = "Found";
                //    }
                //    keywordwises[index] = record;
                //    index++;
                //}
                obj.KeywordWise = keywordwises;
                driver.Quit();

                Debug.WriteLine("close driver---------- yelp");
                Console.WriteLine("close driver---------- yelp");
                return obj;
            }

            return null;
        }

        private string GetRealSearchResultOfYelp(string keyword, string siteUrl, string location, int pageNumber)
        {
            string result = null;
            int position = 0;

            if (driver == null)
                driver = getInstanceOfChromeDriver();

            var value1 = Uri.EscapeDataString(keyword);
            var value2 = Uri.EscapeDataString(location);
            var value3 = (pageNumber - 1) * 10;
            var url = String.Format("https://www.yelp.com/search?find_desc={0}&find_loc={1}&ns=1&start={2}", value1, value2, value3);
            driver.Navigate().GoToUrl(url);

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight)");
            Thread.Sleep(2000);

            var elements = driver.FindElements(By.XPath("//*[@class='css-1l5lt1i']/span/a"));
            if (elements != null && elements.Count() > 0)
            {
                foreach (var e in elements)
                {
                    var href = e.GetAttribute("href");
                    if (href.Contains(siteUrl))
                    {
                        var index = position + 1;
                        result = index + "-" + pageNumber;
                        break;
                    }
                    position++;
                }
            }

            if (result != null)
                return result;

            pageNumber++;
            Thread.Sleep(RandomNumber(2000, 5000));

            if (pageNumber > 10)
                return null;

            return GetRealSearchResultOfYelp(keyword, siteUrl, location, pageNumber);
        }

        private int GetDetailSearchResultOfYelp(string keyword, string siteUrl, string contact_page, string location, int pageNumber)
        {
            int position = 0;
            if (driver == null)
                driver = getInstanceOfChromeDriver();

            if (pageNumber == 1)
            {
                try
                {
                    driver.Navigate().GoToUrl("http://www.yelp.com/");
                    var baseKeyword = keyword + " " + contact_page;
                    var inputElements = driver.FindElements(By.XPath("//*[@class='pseudo-input_field business-search-form_input-field']"));
                    inputElements[0].SendKeys(keyword);
                    inputElements[1].SendKeys(location);
                    driver.FindElement(By.XPath("//*[@class='ybtn ybtn--primary ybtn--small business-search-form_button']")).Submit();
                    Thread.Sleep(1000);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var elements = driver.FindElements(By.XPath("//*[@class='css-1l5lt1i']/span/a"));
            if (elements == null || elements.Count() == 0)
                return 0;

            foreach (var e in elements)
            {
                var href = e.GetAttribute("href");
                if (href.Contains(siteUrl))
                {
                    position = 1;
                    break;
                }
            }

            if (position > 0)
                return position;
            return 0;
        }

        public RequestBO.Rootobj RankDetailsOfBing(string[] keywords, string siteUrl)
        {
            var obj = new RequestBO.Rootobj();
            var keywordwises = new RequestBO.Keywordwise[keywords.Length];
            int index = 0;
            if (keywords != null && keywords.Length > 0)
            {
                if (driver == null)
                    driver = getInstanceOfChromeDriver();

                foreach (var keyword in keywords)
                {
                    var record = new RequestBO.Keywordwise()
                    {
                        position = 0,
                        rank = "0",
                        keyword = keyword
                    };
                    var item = GetRealSearchResultOfBing(keyword, siteUrl, 1);

                    if (item != null)
                    {
                        var data = item.Split('-');
                        record.position = Convert.ToInt32(data[0]);
                        record.rank = data[1];
                        record.keyword = keyword;

                        Debug.WriteLine("Bing Data - {0}", item);
                        Console.WriteLine("Bing Data - {0}", item);
                    }

                    keywordwises[index] = record;
                    index++;
                }

                obj.KeywordWise = keywordwises;
                driver.Quit();

                Debug.WriteLine("close driver---------- bing");
                Console.WriteLine("close driver---------- bing");
                return obj;
            }

            return null;
        }

        private string GetRealSearchResultOfBing(string keyword, string siteUrl, int pageNumber)
        {
            string result = null;
            int position = 0;
            bool errorFound = false;

            if (driver == null)
                driver = getInstanceOfChromeDriver();

            if (pageNumber == 1)
            {
                driver.Navigate().GoToUrl("http://www.bing.com/");
                driver.FindElement(By.Id("sb_form_q")).SendKeys(keyword);
                driver.FindElement(By.Id("search_icon")).Click();
                Thread.Sleep(RandomNumber(1000, 1500));
            }

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight)");
            Thread.Sleep(2000);

            var elements = driver.FindElements(By.XPath("//li[@class='b_algo']//cite"));

            if (elements != null && elements.Count() > 0)
            {
                foreach (var e in elements)
                {
                    var txt = e.Text;
                    if (txt.Contains(siteUrl))
                    {
                        result = position + "-" + pageNumber;
                        break;
                    }
                    position++;
                }
            }

            if (result != null)
                return result;

            try
            {
                if (driver != null)
                {
                    Thread.Sleep(RandomNumber(5000, 6999));
                    driver.FindElement(By.XPath("//ol[@id='b_results']/li[@class='b_pag']/nav/ul/li[(last)]")).Click();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Step1 : Next Page is not found in bing search-----------{0}", pageNumber);
                Console.WriteLine("Step1 : Next Page is not found in bing search-----------{0}", pageNumber);
                errorFound = true;
            }

            if (errorFound)
            {
                Thread.Sleep(2000);
                try
                {
                    if (driver != null)
                    {
                        var els = driver.FindElements(By.XPath("(//nav[@role='navigation']/ul)[2]/li/a"));
                        var ele = els.FirstOrDefault(a => a.Text == (pageNumber + 1).ToString());
                        ele.Click();
                    }
                    errorFound = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Step2 : Next Page is not found in bing search-----------{0}", pageNumber);
                    Console.WriteLine("Step2 : Next Page is not found in bing search-----------{0}", pageNumber);
                    errorFound = true;
                }
            }

            if (errorFound)
            {
                Thread.Sleep(2000);
                try
                {
                    if (driver != null)
                    {
                        Thread.Sleep(RandomNumber(5000, 6999));
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        var ele = driver.FindElement(By.XPath("//ul[@class='sb_pagF']/li[last()]"));
                        jse.ExecuteScript("arguments[0].click();", ele);
                    }
                    Thread.Sleep(5000);
                    errorFound = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Step2 : Next Page is not found in bing search-----------{0}", pageNumber);
                    Console.WriteLine("Step2 : Next Page is not found in bing search-----------{0}", pageNumber);
                    errorFound = true;
                }
            }

            if (errorFound)
                return null;

            pageNumber++;
            Thread.Sleep(RandomNumber(2000, 5000));

            if (pageNumber > 10)
                return null;

            return GetRealSearchResultOfBing(keyword, siteUrl, pageNumber);
        }

        public RequestBO.Rootobj RankDetailsOfBing2(string[] keywords, string siteUrl)
        {
            var obj = new RequestBO.Rootobj();
            var keywordwises = new RequestBO.Keywordwise[keywords.Length];
            int index = 0;

            foreach(var keyword in keywords)
            {
                var item = GetDetailSearchResultOfBing(keyword, siteUrl, 0);

                var record = new RequestBO.Keywordwise
                {
                    keyword = keyword,
                    rank = item != null ? item.First().Key : "N/A",
                    position = item != null ? item.First().Value : 0
                };
                keywordwises[index] = record;
                index++;
            }
            obj.KeywordWise = keywordwises;
            return obj;
        }

        int limitLoopCount = 0;

        private Dictionary<string, int> GetDetailSearchResultOfBing(string keyword, string siteUrl, int linkNumber)
        {
            if (!siteUrl.Contains("http://") || !siteUrl.Contains("https://"))
                siteUrl = "https://" + siteUrl;

            var uri = new Uri(siteUrl);
            string raw = string.Empty;
            string search = string.Empty;

            if(linkNumber == 0)
            {
                raw = "https://www.bing.com/search?q={0}";
                search = string.Format(raw, HttpUtility.UrlEncode(keyword));
            }
            else
            {
                raw = "https://www.bing.com/search?q={0}&first={1}";
                search = string.Format(raw, HttpUtility.UrlEncode(keyword), linkNumber);
            }

            var driver = getInstanceOfChromeDriver();
            driver.Navigate().GoToUrl(search);
            string source = driver.PageSource;

            var result = FindPositionFromBing(source, uri, linkNumber);
            var isFounded = result.First().Key;
            var linkCount = result.First().Value;

            if (isFounded)
            {
                var data = new Dictionary<string, int>();
                var rank = (limitLoopCount + 1).ToString();
                var position = linkCount;
                data.Add(rank, position);
                return data;
            }

            limitLoopCount++;
            if (limitLoopCount == 3)
            {
                limitLoopCount = 0;
                return null;
            }
            else
            {
                GetDetailSearchResultOfBing(keyword, siteUrl, linkCount + 1);
            }
            
            return null;
        }

        private Dictionary<bool, int> FindPositionFromBing(string html, Uri uri, int linkNumber)
        {
            Dictionary<bool, int> result = new Dictionary<bool, int>();
            var lookup = "(<li class=\"b_algo\"><h2>)(<a href=\"https?://)(.*?)\" h=\"";
            MatchCollection matches = Regex.Matches(html, lookup);

            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i].Groups[3].Value;
                if (match.Contains(uri.Host))
                {
                    result.Add(true, i + 1);
                    return result;
                }
            }

            if(linkNumber == 0)
                result.Add(false, matches.Count + linkNumber);
            else
                result.Add(false, 14 + linkNumber);

            return result;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
