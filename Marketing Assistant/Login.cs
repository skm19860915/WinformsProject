using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using testc = OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.Deployment;
using System.Deployment.Application;
using System.Dynamic;
using System.Timers;
using Microsoft.Win32;
using System.Globalization;
using System.Device.Location;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace Marketing_Assistant
{
    public partial class Login : Form
    {
        //private static System.Timers.Timer aTimer;
        private static RequestBO.Rootobject obj = new RequestBO.Rootobject();
        private static RequestBO.Rootobj rank = new RequestBO.Rootobj();
        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\MarketingAssistant");

        private static string app_version = "-";

        List<RequestBO.ProjectQueueSorted> lstProjectQueueSorted;
        //string[] lstKeywordsToSearch;

        RequestBO.Project localproject;
        RequestBO.ProjectQueueRootobject projectQueueRootobject;

        //bool useDevQueue = false;

        //int projectIndex = 0;
        //int searchCount = 0;

        ChromeDriver driver = null;

        private bool InvokedRequired = false;

        //opening the subkey          
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public Login()
        {
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            var index = Application.ExecutablePath.ToString().IndexOf(".exe");
            if(index >= 0)
                reg.SetValue("Marketing Assistant", Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().IndexOf(".exe")) + ".appref-ms");
            else
                reg.SetValue("Marketing Assistant", Application.ExecutablePath.ToString() + ".appref-ms");

            InitializeComponent();
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;

            txtEmail.Text = Environment.MachineName;
            customeMessagePanel.Hide();

            //timer1 = new System.Windows.Forms.Timer();
            //timer1.Interval = 18000;
            //timer1.Enabled = true;

            //fetchQueueTimer = new System.Windows.Forms.Timer();
            //fetchQueueTimer.Interval = 39600000;
            //fetchQueueTimer.Enabled = true;

            paneldetails.Hide();

            //TestUrl();
        }

        private void TestUrl()
        {
            string url = "bipicar.com/es/es/";
            if (!url.Contains("http://") || !url.Contains("https://"))
                url = "https://" + url;

            var uri = new Uri(url);
            var host = uri.Host;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Login_Load");
            lstProjectQueueSorted = new List<RequestBO.ProjectQueueSorted>();
            localproject = new RequestBO.Project();

            if (key != null && key.GetValue("is_login") != null)
            {
                if (!(string.IsNullOrEmpty((key.GetValue("email").ToString()))))
                {
                    try
                    {
                        //RequestBO.Project obj = new RequestBO.Project();
                        txtEmail.Text = key.GetValue("email").ToString();
                        txtProj.Text = key.GetValue("project").ToString();

                        if (key.GetValue("is_login").ToString() == "Yes")
                        {
                            obj = LoginAndGetData();
                            if (obj == null)
                                return;

                            DisplayDetailInformationUI(obj);
                            //populateUI(obj);
                            //fetchQueueTimer.Tick += new System.EventHandler(this.fetchQueueTimer_Tick);
                            //fetchQueueTimer.Start();
                            //fetchQueue();

                            //Thread.Sleep(3000);
                            //ShowRankGridInformation(obj);

                            Thread loadRankDataThread = new Thread(LoadRankDataAsync);
                            loadRankDataThread.Start();
                        }
                    }
                    catch
                    {
                        lbstatus.Text = "Please try again";
                        lbstatus.Refresh();
                    }
                }
                else
                {
                    paneldetails.Hide();
                    this.AutoSize = true;
                    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                }
            }
            else
            {
                paneldetails.Hide();
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
        }

        public class Response
        {
            public string status { get; set; }
        }

        private void button1_Click(object sende, EventArgs arg)
        {
            try
            {
                button1.Text = "Please Wait";
                //RequestBO.Project obj = new RequestBO.Project();
                obj = LoginAndGetData();
                if (obj == null)
                    return;

                DisplayDetailInformationUI(obj);
                //populateUI(obj);
                // System.Timers.Timer timer = new System.Timers.Timer();
                //timer1.Start();
                //timer1.Tick += new EventHandler(timer1_Tick);
                //csv redundant?

                // timer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);

                //lbstatus.Text = "Process has been completed.";
                //lbstatus.Refresh();

                //Thread.Sleep(3000);
                //ShowRankGridInformation(obj);

                Thread loadRankDataThread = new Thread(LoadRankDataAsync);
                loadRankDataThread.Start();
            }
            catch (Exception ex)
            {
                button1.Text = "Connect";
                lbstatus.Text = "Please try again";
                Debug.WriteLine("Exception Button.LoginAndGetData(), trying again " + ex.ToString());
                Console.WriteLine("Exception Button.LoginAndGetData(), trying again " + ex.ToString());
                lbstatus.Refresh();
            }

            //key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            //if (key != null && key.GetValue("is_login") != null)
            //{
            //    if (key.GetValue("is_login").ToString() == "Yes")
            //    {
            //        fetchQueue();
            //    }
            //}
            //key.Close();
        }

        private async void LoadRankDataAsync()
        {
            if (InvokedRequired)
            {
                //BeginInvoke(new Action(() => {
                //    ShowRankGridInformation(obj);
                //} ));

                await Task.Run(new Action(() =>
                {
                    ShowRankGridInformation(obj);
                }));
            }
        }

        //private RequestBO.ReportParam searchengine(RequestBO.Rootobject objroot)
        //{
        //    Debug.WriteLine("searchengine");

        //    var publish = new RequestBO.ReportParam();

        //    RequestBO.SearchParam objsearch = new RequestBO.SearchParam();
        //    RequestBO.SearchParam objresult = new RequestBO.SearchParam();
        //    RequestBO.ReportParam objreport = new RequestBO.ReportParam();
        //    HttpModule request = new HttpModule();
        //    objsearch.page = 0;
        //    objsearch.searchtxt = "";
        //    int length = objroot.project.keywords.Length;
        //    for (int i = 0; i < length; i++)
        //    {
        //        objsearch.searchtxt = objroot.project.keywords[i];


        //        objsearch.website = objroot.project.website;
        //        lbstatus.Text = "Search No" + (i + 1) + " is in progress.";
        //        lbstatus.Show();
        //        lbstatus.Refresh();
        //        FetchUrl objchrome = new FetchUrl();
        //        objresult = objchrome.createConnection();
        //        Thread.Sleep(6000);
        //        objsearch.driver = objresult.driver;
        //        Thread.Sleep(6000);
        //        objchrome.gethtml(objsearch);
        //        objresult = objchrome.getelement(objsearch);

        //        int page = 1;

        //        while (objresult.elements.Count == 0)

        //        {
        //            if (page < 30)
        //            {
        //                objchrome.getnextpage(objresult);
        //                objresult = objchrome.getelement(objsearch);
        //                page++;
        //            }
        //        }

        //        objchrome.getlinks(objresult);
        //        objchrome.closeConnection(objsearch);
        //        objreport.google_page = page.ToString();
        //        objreport.keyword = objsearch.searchtxt;
        //        objreport.project_id = objroot.project._id;
        //        objreport.visited_link = objroot.project.website;


        //        publish = request.PublishReport(objreport);

        //    }
        //    timer1.Interval = 180000;

        //    return (publish);
        //}



        public void DisplayDetailInformationUI(RequestBO.Rootobject stat)
        {
            lblVersion.Text = app_version;
            lblVersion.Refresh();

            if (stat == null)
                return;
            if (stat.project == null)
                return;

            switch (stat.project.project_type.ToLower())
            {
                case "bing":
                    label7.Text = "Bing Ranking Search Now ....";
                    break;
                case "youtube":
                    label7.Text = "Youtube Ranking Search Now ....";
                    break;
                case "amazon":
                    label7.Text = "Amazon Ranking Search Now ....";
                    break;
                case "yelp":
                    label7.Text = "Yelp Ranking Search Now ....";
                    break;
                default:
                    label7.Text = "Google Ranking Search Now ....";
                    break;
            }

            if(string.Equals(stat.resp.show_custom_message.ToLower(), "y"))
            {
                agencyDetailsPanel.Hide();
                lblCustomMessageValue.Text = stat.resp.custom_message;
                lblCustomMessageValue.Refresh();
                customeMessagePanel.Show();
                customeMessagePanel.Refresh();
            }
            else
            {
                customeMessagePanel.Hide();
                lbproject.Text = stat.resp.name;
                lbproject.Refresh();
                lbcontact.Text = stat.resp.contact_name;
                lbcontact.Refresh();
                lbphone.Text = stat.resp.contact_1;
                lbphone.Refresh();
                lbemail.Text = stat.resp.email;
                lbemail.Refresh();
                lbwebsite.Text = stat.resp.website;
                lbwebsite.Refresh();
                agencyDetailsPanel.Show();
                agencyDetailsPanel.Refresh();
            }

            //if(rank.KeywordWise != null && rank.KeywordWise.Length > 0)
            //{
            //    dataGridView1.DataSource = rank.KeywordWise.Select(o => new { Column1 = o.keyword, Column2 = o.rank, Column3 = o.position }).ToList();
            //    dataGridView1.Columns[0].Width = 270;
            //    dataGridView1.Refresh();
            //}

            paneldetails.Show();
            paneldetails.Refresh();

            //if (rank.KeywordWise != null && rank.KeywordWise.Length > 0)
            //{
            //    foreach(var item in rank.KeywordWise)
            //    {
            //        SendSearchResultToDatabase(stat, item);
            //    }
            //}

            InvokedRequired = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void ShowRankGridInformation(RequestBO.Rootobject stat)
        {
            HttpModule request = new HttpModule();

            if (string.Equals(stat.project.code, "hDFBN"))
            {
                rank = request.RankDetailsOfYoutube(stat.project.keywords, stat.project.website, stat.resp.contact_1);
            }
            //else if (string.Equals(stat.project.code, "RhEip"))
            //{
            //    rank = request.RankDetailsOfAmazon(stat.project.keywords, stat.project.website, stat.resp.contact_1);
            //}
            else if (string.Equals(stat.project.code, "PBnhr"))
            {
                rank = request.RankDetailsOfYelp(stat.project.keywords, stat.project.website, stat.resp.contact_1, stat.resp.about);
            }
            else
            {
                var search_engine_type = stat.project.project_type.ToLower();
                switch (search_engine_type)
                {
                    case "google":
                        rank = request.RankDetailsOfGoogle(stat.project.website, stat.project._id.ToString());
                        break;
                    case "bing":
                        rank = request.RankDetailsOfBing(stat.project.keywords, stat.project.website);
                        break;
                    case "yelp":
                        rank = request.RankDetailsOfYelp(stat.project.keywords, stat.project.website, stat.resp.contact_1, stat.resp.about);
                        break;
                    case "amazon":
                        rank = request.RankDetailsOfAmazon(stat.project.keywords, stat.project.website, stat.resp.contact_1);
                        break;
                    default:
                        rank = request.RankDetailsOfGoogle(stat.project.website, stat.project._id.ToString());
                        break;
                }
            }

            if (rank.KeywordWise != null && rank.KeywordWise.Length > 0)
            {
                this.Invoke(new Action(() => {
                    dataGridView1.DataSource = rank.KeywordWise.Select(o => new { Column1 = o.keyword, Column2 = o.rank, Column3 = o.position }).ToList();
                    dataGridView1.Columns[0].Width = 270;
                    dataGridView1.Refresh();

                    switch (stat.project.project_type.ToLower())
                    {
                        case "bing":
                            label7.Text = "Current Bing Ranking";
                            break;
                        case "youtube":
                            label7.Text = "Current Youtube Ranking";
                            break;
                        case "amazon":
                            label7.Text = "Current Amazon Ranking";
                            break;
                        case "yelp":
                            label7.Text = "Current Yelp Ranking";
                            break;
                        default:
                            label7.Text = "Current Google Ranking";
                            break;
                    }

                    label7.Refresh();
                }));

                foreach (var item in rank.KeywordWise)
                {
                    SendSearchResultToDatabase(stat, item);
                }
            }

            //if (rank.KeywordWise != null && rank.KeywordWise.Length > 0)
            //{
            //    dataGridView1.DataSource = rank.KeywordWise.Select(o => new { Column1 = o.keyword, Column2 = o.rank, Column3 = o.position }).ToList();
            //    dataGridView1.Columns[0].Width = 270;
            //    dataGridView1.Refresh();
            //}

            //switch (stat.project.project_type.ToLower())
            //{
            //    case "bing":
            //        label7.Text = "Current Bing Ranking";
            //        break;
            //    case "youtube":
            //        label7.Text = "Current Youtube Ranking";
            //        break;
            //    case "amazon":
            //        label7.Text = "Current Amazon Ranking";
            //        break;
            //    case "yelp":
            //        label7.Text = "Current Yelp Ranking";
            //        break;
            //    default:
            //        label7.Text = "Current Google Ranking";
            //        break;
            //}

            //paneldetails.Show();
            //paneldetails.Refresh();

            //if (rank.KeywordWise != null && rank.KeywordWise.Length > 0)
            //{
            //    foreach (var item in rank.KeywordWise)
            //    {
            //        SendSearchResultToDatabase(stat, item);
            //    }
            //}
        }

        private void SendSearchResultToDatabase(RequestBO.Rootobject stat, RequestBO.Keywordwise keywordwise)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                {
                    {"project_id", stat.project._id},
                    {"found_on_page", keywordwise.rank.ToString() },
                    {"stay_page",keywordwise.date },
                    {"visited_link", stat.project.website },
                    {"keyword", keywordwise.keyword },
                    {"maxSearchPages","30" },
                    {"ip",GetIPAddress()},
                    {"type","windows" }, //versionURL
                    {"device_id", GetMacAddress()},
                    {"rank", keywordwise.position.ToString() },
                    {"app_version", app_version }
                };

            HttpContent content = new FormUrlEncodedContent(values);
            String url = "https://my.site-pop.com:8081/report/put";
            try
            {
                var response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<RequestBO.ReportParam>(responseString);

                Debug.WriteLine("----------------------- First Report Result -----------------------------");
                Console.WriteLine("----------------------- First Report Result -----------------------------");
                Debug.WriteLine("Report: keyword - {0}, rank - {1}, found_on_page - {2}, app_version - {3}, website - {4}",
                    keywordwise.keyword, keywordwise.position, keywordwise.rank, app_version, stat.project.website);
                Console.WriteLine("Report: keyword - {0}, rank - {1}, found_on_page - {2}, app_version - {3}, website - {4}",
                    keywordwise.keyword, keywordwise.position, keywordwise.rank, app_version, stat.project.website);
                Debug.WriteLine("----------------------- First Report Result End -----------------------------");
                Console.WriteLine("----------------------- First Report Result End -----------------------------");
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
                    JsonConvert.DeserializeObject<RequestBO.ReportParam>(responseString);
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                    Console.WriteLine("Failed second PublishReport(), exiting function " + ex2.ToString());
                }
            }
        }

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

            return address;
        }

        public string GetMacAddress()
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


        //public string GetMacAddress()
        //{
        //    String firstMacAddress = "";
        //    firstMacAddress = NetworkInterface
        //    .GetAllNetworkInterfaces()
        //    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        //    .Select(nic => nic.GetPhysicalAddress().ToString())
        //    .FirstOrDefault();

        //    return firstMacAddress;
        //}

        public RequestBO.Rootobject LoginAndGetData()
        {
            if (txtEmail.Text == string.Empty || txtProj.Text == string.Empty)
            {
                MessageBox.Show("Please provide valid Email Address and Project Code");
                button1.Text = "Connect";
                return null;
            }

            String email = txtEmail.Text;
            string project = txtProj.Text;
            HttpModule request = new HttpModule();
            var status = request.LoginAutho(email, project);
            //app_version = request.getAppVersionFromAPI();

            if (ApplicationDeployment.IsNetworkDeployed)
                app_version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();

            RequestBO.Rootobject stat = new RequestBO.Rootobject();
            RequestBO.Rootobject1 resp = new RequestBO.Rootobject1();

            if (status.status == "true")
            {
                //Properties.Settings.Default.FirstUserSetting = "abc";                
                key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
                key.SetValue("is_login", "Yes");
                key.SetValue("email", email);
                key.SetValue("project", project);
                key.Close();

                marketing_assistant.Properties.Settings.Default.is_login = true;
                marketing_assistant.Properties.Settings.Default.email = email;
                marketing_assistant.Properties.Settings.Default.project = project;
                marketing_assistant.Properties.Settings.Default.user_device_id = status.id;
                marketing_assistant.Properties.Settings.Default.Save();

                HttpModule request2 = new HttpModule();

                if (string.Equals(project, "hDFBN"))
                {
                    stat = request2.GetYoutubeData();
                    //rank = request2.RankDetailsOfYoutube(stat.project.keywords, stat.project.website, stat.resp.contact_1);
                }
                //else if (string.Equals(project, "RhEip"))
                //{
                //    stat = request2.GetAmazonData();
                //    //rank = request2.RankDetailsOfAmazon(stat.project.keywords, stat.project.website, stat.resp.contact_1);
                //}
                else if (string.Equals(project, "PBnhr"))
                {
                    stat = request2.GetYelpData();
                    //rank = request2.RankDetailsOfYelp(stat.project.keywords, stat.project.website, stat.resp.contact_1, stat.resp.about);
                }
                else
                {
                    stat = request2.ProjectAuth(project.ToString(), status.id);
                    resp = request2.AgenyDetails(stat.project._id.ToString(), status.id);
                    stat.resp = resp.response;

                    var search_engine_type = stat.project.project_type.ToLower();
                    //switch (search_engine_type)
                    //{
                    //    case "google":
                    //        rank = request2.RankDetailsOfGoogle(stat.project.website, stat.project._id.ToString());
                    //        break;
                    //    case "bing":
                    //        rank = request2.RankDetailsOfBing(stat.project.keywords, stat.project.website);
                    //        break;
                    //    case "yelp":
                    //        rank = request2.RankDetailsOfYelp(stat.project.keywords, stat.project.website, stat.resp.contact_1, stat.resp.about);
                    //        break;
                    //    case "amazon":
                    //        rank = request2.RankDetailsOfAmazon(stat.project.keywords, stat.project.website, stat.resp.contact_1);
                    //        break;
                    //    default:
                    //        rank = request2.RankDetailsOfGoogle(stat.project.website, stat.project._id.ToString());
                    //        break;
                    //}
                }

                return (stat);
            }
            else
            {
                MessageBox.Show("Please try again");
                return (stat);
            }
        }

        public RequestBO.Rootobject HitRequest()
        {
            String email = txtEmail.Text;
            string project = txtProj.Text;
            HttpModule request = new HttpModule();
            var status = request.LoginAutho(email, project);
            RequestBO.Rootobject stat = new RequestBO.Rootobject();
            RequestBO.Rootobject1 resp = new RequestBO.Rootobject1();

            if (status.status == "true")
            {
                //Properties.Settings.Default.FirstUserSetting = "abc";                
                key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
                key.SetValue("is_login", "Yes");
                key.SetValue("email", email);
                key.SetValue("project", project);
                key.Close();

                marketing_assistant.Properties.Settings.Default.is_login = true;
                marketing_assistant.Properties.Settings.Default.email = email;
                marketing_assistant.Properties.Settings.Default.project = project;
                marketing_assistant.Properties.Settings.Default.Save();

                HttpModule request2 = new HttpModule();

                stat = request2.ProjectAuth(project.ToString(), status.id);
                resp = request2.AgenyDetails(stat.project._id.ToString(), status.id);
                stat.resp = resp.response;
                rank = request2.RankDetailsOfGoogle(stat.resp.website, stat.project._id.ToString());

                return (stat);
            }
            else
            {
                MessageBox.Show("Please try again");
                return (stat);
            }
        }

        /*

        private void fetchQueueTimer_Tick(object sender, EventArgs e)
        {
            //backgroundQueue(obj);
        }
        */

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    background(obj);
        //}
        //public void background(RequestBO.Rootobject objroot)
        //{
        //    //lbstatus.Text = "Search is in progress.";
        //    //lbstatus.Show();
        //    //lbstatus.Refresh();
        //    timer1.Enabled = false;
        //    //timer1.Stop(); //-csv redundant
        //    BackgroundWorker bw = new BackgroundWorker();
        //    bw.WorkerReportsProgress = true;
        //    bw.WorkerSupportsCancellation = true;
        //    bw.DoWork += Bw_DoWork;
        //    bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

        //    //Parameter you need to work in Background-Thread for example your strings
        //    // string[] param = new[] { "Text1", "Text2", "Text3", "Text4" };
        //    //RequestBO.Rootobject objroot
        //    //Start work
        //    bw.RunWorkerAsync(objroot);
        //    return;

        //}

        /*

        public void backgroundQueue(RequestBO.Rootobject objroot)
        {
            fetchQueueTimer.Enabled = false;
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += Bw_DoQueueWork;
            bw.RunWorkerCompleted += Bw_RunWorkerQueueCompleted;

            //Parameter you need to work in Background-Thread for example your strings
            // string[] param = new[] { "Text1", "Text2", "Text3", "Text4" };
            //RequestBO.Rootobject objroot
            //Start work
            bw.RunWorkerAsync(objroot);
            return;
        }
        */

        /*
        private void Bw_DoQueueWork(object sender, DoWorkEventArgs e)
        {
            //replaces fetchqueue() timer
            try
            {
                Debug.WriteLine("fetchQueue bw: " + DateTime.Now);
                Console.WriteLine("fetchQueue bw: " + DateTime.Now);
                //here write the code that you want to schedule
                HttpModule httpModule = new HttpModule();
                projectQueueRootobject = httpModule.getProjectQueue();

                lstProjectQueueSorted = new List<RequestBO.ProjectQueueSorted>();

                foreach (RequestBO.ProjectQueue o in projectQueueRootobject.nearby)
                {
                    RequestBO.ProjectQueueSorted addme = new RequestBO.ProjectQueueSorted();
                    addme.code = o.code;
                    addme.distance = 0;
                    addme.latitude = o.latitude;
                    addme.longitude = o.longitude;

                    lstProjectQueueSorted.Add(addme);
                };


                //starting with a random project now - also, .Count is OK for dereference as max # provided is actually NOT inclusive - eg. (0,4) max gen'd would be 3
                
                //getKeywordsToSearch(lstProjectQueueSorted[RandomNumber(0, lstProjectQueueSorted.Count)].code);
                //Changed this - don't need to grab new keywords on background new get_today_queue


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception fetchQueue(), trying again " + ex.ToString());
                Console.WriteLine("Exception fetchQueue(), trying again " + ex.ToString());
                //Thread.Sleep(6000);
                //fetchQueue();
            }
            return;
        }
        */

        ////Do your Background-Work
        //private void Bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    RequestBO.Rootobject objroot = e.Argument as RequestBO.Rootobject;
        //    var publish = new RequestBO.ReportParam();

        //    RequestBO.SearchParam objsearch = new RequestBO.SearchParam();
        //    RequestBO.SearchParam objresult = new RequestBO.SearchParam();
        //    RequestBO.ReportParam objreport = new RequestBO.ReportParam();
        //    HttpModule request = new HttpModule();
        //    DateTime startTime;
        //    objsearch.page = 0;
        //    objsearch.searchtxt = "";
        //    int length = objroot.project.keywords.Length;
        //    Random rnd;
        //    int keywordindex;
        //    bool found = true;
        //    FetchUrl objchrome = new FetchUrl();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (i > 0)
        //        {
        //            Thread.Sleep(RandomNumber(604800, 720000));
        //        }

        //        rnd = new Random();
        //        int max = length;
        //        keywordindex = rnd.Next(0, length);
        //        objchrome = new FetchUrl();
        //        objsearch.searchtxt = objroot.project.keywords[keywordindex];
        //        objsearch.website = objroot.project.website;

        //        startTime = DateTime.Now;
        //        objresult = objchrome.createConnection();
        //        Thread.Sleep(6000);
        //        objsearch.driver = objresult.driver;
        //        testc.IJavaScriptExecutor jse = (testc.IJavaScriptExecutor)objsearch.driver;
        //        Thread.Sleep(6000);
        //        objchrome.gethtml(objsearch);
        //        objresult = objchrome.getelement(objsearch);

        //        int page = 1;
        //        int currentpage = 1;
        //        int pagemax = RandomNumber(4, 9);

        //        while (objresult.elements.Count == 0)

        //        {
        //            if (page < pagemax)
        //            {
        //                //int contentHeight = (int)jse.ExecuteScript("return window.innerHeight");

        //                //int contentWidth = (int)jse.ExecuteScript("return window.innerWidth");

        //                //int firstScroll = RandomNumber(100, contentHeight);

        //                //jse.ExecuteScript("window.scrollTo(0, " + firstScroll.ToString() + ")");
        //                jse.ExecuteScript("window.scrollTo(0, 2000)");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,-1275)");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,880)");
        //                Thread.Sleep(RandomNumber(800, 1200));
        //                jse.ExecuteScript("window.scrollBy(0,390)");
        //                Thread.Sleep(RandomNumber(5000, 6999));
        //                jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        //                objchrome.getnextpage(objresult);
        //                objresult = objchrome.getelement(objsearch);
        //                page++;
        //                currentpage = page;
        //            }
        //            else
        //            {
        //                found = false;
        //                break;
        //            }
        //        }

        //        if (found == true)
        //        {
        //            try
        //            {
        //                objchrome.getRank(objresult);
        //                Thread.Sleep(RandomNumber(20000, 65999));
        //                objchrome.getlinks(objresult);

        //                var _randomNumber = RandomNumber(0, 1000).ToString();
        //                jse.ExecuteScript("window.scrollTo(0," + _randomNumber + ")");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,-1235)");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,872)");
        //                Thread.Sleep(RandomNumber(800, 1200));
        //                jse.ExecuteScript("window.scrollBy(0,355)");
        //                Thread.Sleep(RandomNumber(800, 1200));
        //                jse.ExecuteScript("window.scrollBy(0,-1214)");
        //                //Basically, this swaps out static "About" and "Contact" RandomClicks for single-word values actually in our keywords.
        //                int spacecount = 0;
        //                int wordIndex = 0;
        //                String randomSearchText = project.keywords[RandomNumber(0, project.keywords.Length)];
        //                String[] keyWordArray = randomSearchText.Split(' ');
        //                if (keyWordArray != null && keyWordArray.Length > 0)
        //                {
        //                    spacecount = randomSearchText.Split(' ').Length - 1;
        //                    wordIndex = RandomNumber(0, spacecount + 1);
        //                }
        //                for (int k = 0; k <= wordIndex; k++)
        //                {
        //                    if (randomSearchText.IndexOf(" ") != -1)
        //                    {
        //                        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
        //                    }
        //                }
        //                if (randomSearchText.LastIndexOf(" ") != -1)
        //                {
        //                    randomSearchText = randomSearchText.Substring(randomSearchText.LastIndexOf(" ") + 1);
        //                }
        //                objresult.innerlink = randomSearchText;
        //                objchrome.RandomLinks(objresult);
        //                Thread.Sleep(RandomNumber(25000, 75999));
        //                spacecount = 0;
        //                wordIndex = 0;
        //                randomSearchText = project.keywords[RandomNumber(0, project.keywords.Length)];
        //                keyWordArray = randomSearchText.Split(' ');
        //                if (keyWordArray != null && keyWordArray.Length > 0)
        //                {
        //                    spacecount = randomSearchText.Split(' ').Length - 1;
        //                    wordIndex = RandomNumber(0, spacecount + 1);
        //                }
        //                for (int k = 0; k <= wordIndex; k++)
        //                {
        //                    if (randomSearchText.IndexOf(" ") != -1)
        //                    {
        //                        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
        //                    }
        //                }
        //                if (randomSearchText.LastIndexOf(" ") != -1)
        //                {
        //                    randomSearchText = randomSearchText.Substring(randomSearchText.LastIndexOf(" ") + 1);
        //                }
        //                objresult.innerlink = randomSearchText;
        //                objchrome.RandomLinks(objresult);
        //                jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,-1131)");
        //                Thread.Sleep(RandomNumber(1000, 1500));
        //                jse.ExecuteScript("window.scrollBy(0,853)");
        //                Thread.Sleep(RandomNumber(800, 1200));
        //                jse.ExecuteScript("window.scrollBy(0,323)");
        //                Thread.Sleep(RandomNumber(800, 1200));
        //                jse.ExecuteScript("window.scrollBy(0,-1278)");
        //                Thread.Sleep(RandomNumber(38000, 65999));
        //                objchrome.closeConnection(objresult);
        //                //objreport.google_page = objsearch.rank.ToString();
        //                objreport.google_page = currentpage.ToString();
        //                TimeSpan timeElapsed = DateTime.Now - startTime;
        //                objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
        //                objreport.keyword = objsearch.searchtxt;
        //                objreport.project_id = objroot.project._id;
        //                objreport.visited_link = objroot.project.website;
        //            }

        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine("Error during found-click Bw_DoWork() " + ex.ToString());
        //                Console.WriteLine("Error during found-click Bw_DoWork() " + ex.ToString());
        //            }

        //        }
        //        else
        //        {
        //            objchrome.closeConnection(objresult);
        //            objreport.google_page = "0";
        //            TimeSpan timeElapsed = DateTime.Now - startTime;
        //            objreport.stay_page = "0";
        //            objreport.keyword = objsearch.searchtxt;
        //            objreport.project_id = objroot.project._id;
        //            objreport.visited_link = objroot.project.website;

        //        }

        //        publish = request.PublishReport(objreport);
        //        Debug.Write("Publish Report Done for " + objroot.project.website);
        //        Console.WriteLine("Publish Report Done for " + objroot.project.website);

        //    }

        //    //Process your long running  task
        //    e.Result = publish; //Set your Result of the long running task
        //    objchrome.closeConnection(objsearch);
        //    return;

        //}

        ////Taking your results
        //private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    timer1.Stop();
        //    timer1.Enabled = false;
        //    timer1.Dispose();
        //    Application.Exit();
        //    return;

        //}
        private void Bw_timerSearchKeywordsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerSearchKeywords.Stop();
            timerSearchKeywords.Enabled = false;
            //timerSearchKeywords.Dispose();
            //Application.Exit();

            int sleeper = RandomNumber(420000, 900000); //7m to 15min
            //int sleeper = RandomNumber(2000, 9000); //7m to 15min

            timerSearchKeywords.Interval = sleeper; //randomized here now
            timerSearchKeywords.Enabled = true;
            //timerSearchKeywords.Tick += new System.EventHandler(this.timerSearchKeywords_Tick);
            timerSearchKeywords.Start();

            Application.Exit();
            return;

        }

        /*
         * No more repeated fetchQueue. Also removing update check in here; it just runs on timerUpdateCheck11hrs

        private void Bw_RunWorkerQueueCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            fetchQueueTimer.Stop();
            fetchQueueTimer.Enabled = false;
            fetchQueueTimer.Dispose();

            //fetchQueueTimer.Interval = 39600000;
            //fetchQueueTimer.Tick += new System.EventHandler(this.fetchQueueTimer_Tick);
            fetchQueueTimer.Enabled = true;
            fetchQueueTimer.Start();

            Application.Exit();
            return;

        }
        */

        /*
         * No more repeated fetchQueue. Also removing update check in here; it just runs on timerUpdateCheck11hrs
         * 
         * 
        //function to fetch queue first.. doesn't work repeatedly. added functions above to handle that.
        private void fetchQueue()
        {
            try
            {
                Debug.WriteLine("Application started and update task scheduled " + DateTime.Now);
                Console.WriteLine("Application started and update task scheduled " + DateTime.Now);
                //here write the code that you want to schedule

                //DEVQUEUE NUKED - TBR

                //HttpModule httpModule = new HttpModule();
                //projectQueueRootobject = httpModule.getProjectQueue();

                //Removed GEO-IP sorting code

                //double sLat = Double.Parse(HttpModule.GetLatAddress());
                //double sLong = Double.Parse(HttpModule.GetLongitudeAddress());

                //for (int i = 0; i < projectQueueRootobject.nearby.Count(); i++)
                //{
                //    RequestBO.ProjectQueueSorted projectQueueSorted = new RequestBO.ProjectQueueSorted();
                //    double pLat = Double.Parse(projectQueueRootobject.nearby[i].latitude);
                //    double pLong = Double.Parse(projectQueueRootobject.nearby[i].longitude);

                //    projectQueueSorted.code = projectQueueRootobject.nearby[i].code;
                //    projectQueueSorted.latitude = projectQueueRootobject.nearby[i].latitude;
                //    projectQueueSorted.longitude = projectQueueRootobject.nearby[i].longitude;
                //    projectQueueSorted.distance = getDistance(sLat, sLong, pLat, pLong);

                //    lstProjectQueueSorted.Add(projectQueueSorted);
                //}


                ////
                //// lstProjectQueueSorted.Sort(p => p.distance);


                //lstProjectQueueSorted.Sort(delegate (RequestBO.ProjectQueueSorted x, RequestBO.ProjectQueueSorted y)
                //{
                //    return x.distance.CompareTo(y.distance);
                //});



            //DEVQUEUE NUKED - TBR
            //lstProjectQueueSorted = new List<RequestBO.ProjectQueueSorted>();

            //foreach (RequestBO.ProjectQueue o in projectQueueRootobject.nearby)
            //{
            //    RequestBO.ProjectQueueSorted addme = new RequestBO.ProjectQueueSorted();
            //    addme.code = o.code;
            //    addme.distance = 0;
            //    addme.latitude = o.latitude;
            //    addme.longitude = o.longitude;

            //    lstProjectQueueSorted.Add(addme);
            //};

            //starting with a random project now - also, .Count is OK for dereference as max # provided is actually NOT inclusive - eg. (0,4) max gen'd would be 3
            
            //doing this on each call now..    
            //getKeywordsToSearch(lstProjectQueueSorted[RandomNumber(0, lstProjectQueueSorted.Count)].code);




            //So fetchqueue runs every 11hrs
            //this manual task scheduler runs, if 


                //this check is happening every hour.... I'm just not sure why these params are wrong but could be worse
                TaskScheduler.Instance.ScheduleTask(11, 25, 1.0, () =>
                {
                    Debug.WriteLine("UpdateCheck: " + DateTime.Now);
                    Console.WriteLine("UpdateCheck: " + DateTime.Now);
                    //here write the code that you want to schedule


                    //Going to nest this updatecheck (even tho it's 11hrs now) inside an extra try for debug purposes (it blows up when running under dbg)
                    try
                    {
                        ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                        UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();

                        if (info.UpdateAvailable)
                        {
                            // MessageBox.Show("New Update Available. Please update.", "Marketing Assistant");
                            // string update_available = "New Update Available";
                            // linkLabel2.Text = "New Update Available";
                            DialogResult result4 = MessageBox.Show("There is a newer version of Marketing Assistant available. Click 'Ok' to update.",
                                   "Marketing Assistant",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                            if (result4 == DialogResult.OK)
                            {
                                // Thread.Sleep(6000);
                                updateCheck.Update();
                            }
                            // key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
                            // MessageBox.Show("The application has been upgraded to latest version, Please disconnect and re-connect the Application.", "Marketing Assistant");

                            DialogResult result5 = MessageBox.Show("The application has been upgraded to latest version, Please restart the Marketing Assistant.",
                                   "Marketing Assistant",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);
                            if (result5 == DialogResult.OK)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("Update check threw exception: Possibly due to debug run: " + ex.ToString());
                        Console.WriteLine("Update check threw exception: Possibly due to debug run: " + ex.ToString());
                    }



                //HttpModule httpModule1 = new HttpModule();
                //projectQueueRootobject = httpModule1.getProjectQueue();

                
                //second removal of same code, for some reason... -csv


                //double sLat1 = Double.Parse(HttpModule.GetLatAddress());
                //double sLong1 = Double.Parse(HttpModule.GetLongitudeAddress());

                //lstProjectQueueSorted.Clear();

                //for (int i = 0; i < projectQueueRootobject.nearby.Count(); i++)
                //{
                //    RequestBO.ProjectQueueSorted projectQueueSorted = new RequestBO.ProjectQueueSorted();
                //    double pLat = Double.Parse(projectQueueRootobject.nearby[i].latitude);
                //    double pLong = Double.Parse(projectQueueRootobject.nearby[i].longitude);

                //    projectQueueSorted.code = projectQueueRootobject.nearby[i].code;
                //    projectQueueSorted.latitude = projectQueueRootobject.nearby[i].latitude;
                //    projectQueueSorted.longitude = projectQueueRootobject.nearby[i].longitude;
                //    projectQueueSorted.distance = getDistance(sLat1, sLong1, pLat, pLong);

                //    lstProjectQueueSorted.Add(projectQueueSorted);
                //}
                ////
                //// lstProjectQueueSorted.Sort(p => p.distance);
                //lstProjectQueueSorted.Sort(delegate (RequestBO.ProjectQueueSorted x, RequestBO.ProjectQueueSorted y)
                //{
                //    return x.distance.CompareTo(y.distance);
                //});


                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception fetchQueue() first-run " + ex.ToString());
                Console.WriteLine("Exception fetchQueue() first-run " + ex.ToString());
                //Thread.Sleep(6000);
                //fetchQueue();
            }

            return;

        }
        */


        //function to hit keywords randomly
        private void doSearchKeywordsRepeatedly(RequestBO.Project currentProject)
        {
            RequestBO.SearchParam objresult = new RequestBO.SearchParam();
            Thread.Sleep(6000);
            FetchUrl objchrome = new FetchUrl();
            DateTime startTime = DateTime.Now;
            try
            {
                Debug.WriteLine("doSearchKeywordsRepeatedly: " + DateTime.Now);
                Console.WriteLine("doSearchKeywordsRepeatedly: " + DateTime.Now);
                // project.website = "maridonmarketing.com";
                // RequestBO.Rootobject objroot = e.Argument as RequestBO.Rootobject;
                var publish = new RequestBO.ReportParam();

                RequestBO.SearchParam objsearch = new RequestBO.SearchParam();
               // RequestBO.SearchParam objresult = new RequestBO.SearchParam();
                RequestBO.ReportParam objreport = new RequestBO.ReportParam();
                HttpModule request = new HttpModule();
                objsearch.page = 0;
                objsearch.searchtxt = "";
                int length = currentProject.keywords.Length;
                Random rnd = new Random();
                int keywordindex;
                bool found = false;
                keywordindex = rnd.Next(0, length);
                objsearch.searchtxt = currentProject.keywords[keywordindex];
                objsearch.website = currentProject.website;

                // FetchUrl objchrome = new FetchUrl();
                //startTime = DateTime.Now;
                //starttime shouldn't be set here - it's supposed to measure time spent ON user's site.
                Debug.WriteLine("Launching Chrome");
                Console.WriteLine("Launching Chrome");
                objresult = objchrome.createConnection();
                Thread.Sleep(6000);
                objsearch.driver = objresult.driver;
                testc.IJavaScriptExecutor jse = (testc.IJavaScriptExecutor)objsearch.driver;
                Thread.Sleep(6000);
                objchrome.gethtml(objsearch);

                int attempts = 0;
                while (++attempts <= 4 && (objsearch.driver.Url.Contains("google.com/#spf=") || objsearch.driver.Url.Contains("google.com/webhp")))
                {   //our search didn't stick... let's sleep and try once more
                    Thread.Sleep(RandomNumber(2000, 4000));
                    objchrome.gethtml(objsearch);
                }
                if (attempts >= 4)
                {
                    objchrome.closeConnection(objresult);
                    return;
                }

                objresult = objchrome.getelement(objsearch);

                int page = 1;
                int currentpage = 1;
                int pagemax = RandomNumber(7, 9);
                Boolean randomClicks = false;
                //bool foundwithURL = false; //hoping we didn't have to use this...
                while (objresult.elements.Count == 0)

                {
                    if (page < pagemax)
                    {
                        //int contentHeight = (int)jse.ExecuteScript("return window.innerHeight");
                        //int contentWidth = (int)jse.ExecuteScript("return window.innerWidth");
                        //int firstScroll = RandomNumber(100, contentHeight);
                        //jse.ExecuteScript("window.scrollTo(0, " + firstScroll.ToString() + ")");
                        jse.ExecuteScript("window.scrollTo(0, 2000)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,-1275)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,880)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,390)");
                        Thread.Sleep(RandomNumber(5000, 6999));
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        objchrome.getnextpage(objresult);
                        objresult = objchrome.getelement(objsearch);
                        page++;
                        currentpage = page;

                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
                if (objresult.elements.Count > 0)
                {
                    found = true;
                }
                if (found == true)
                {
                    objchrome.getRank(objresult);
                    Thread.Sleep(RandomNumber(20000, 65999));
                    startTime = DateTime.Now;
                    objchrome.getlinks(objresult);
                    //above actually clicks on search result
                    jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    Thread.Sleep(RandomNumber(1000, 1500));
                    jse.ExecuteScript("window.scrollBy(0,-1235)");
                    Thread.Sleep(RandomNumber(1000, 1500));
                    jse.ExecuteScript("window.scrollBy(0,872)");
                    Thread.Sleep(RandomNumber(800, 1200));
                    jse.ExecuteScript("window.scrollBy(0,355)");
                    Thread.Sleep(RandomNumber(800, 1200));
                    jse.ExecuteScript("window.scrollBy(0,-1214)");

                    //Adding long sleep per Chris
                    Thread.Sleep(RandomNumber(15000, 20000));

                    //Basically, this swaps out static "About" and "Contact" RandomClicks for single-word values actually in our keywords.

                    //Switching back to About/Contact pages only instead of Random links - per Chris
                    //int spacecount = 0;
                    //int wordIndex = 0;
                    //String randomSearchText = currentProject.keywords[RandomNumber(0, currentProject.keywords.Length)];
                    String randomSearchText = "About";

                    //String[] keyWordArray = randomSearchText.Split(' ');
                    //if (keyWordArray != null && keyWordArray.Length > 0)
                    //{
                    //    spacecount = randomSearchText.Split(' ').Length - 1;
                    //    wordIndex = RandomNumber(0, spacecount + 1);
                    //}
                    //for (int i = 0; i <= wordIndex; i++)
                    //{
                    //    if (randomSearchText.IndexOf(" ") != -1)
                    //    {
                    //        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
                    //    }
                    //}
                    //if (randomSearchText.LastIndexOf(" ") != -1)
                    //{
                    //    randomSearchText = randomSearchText.Substring(randomSearchText.LastIndexOf(" ") + 1);
                    //}
                    objresult.innerlink = randomSearchText;
                    objchrome.RandomLinks(objresult);
                    Thread.Sleep(RandomNumber(25000, 80000));

                    //spacecount = 0;
                    //wordIndex = 0;
                    //randomSearchText = currentProject.keywords[RandomNumber(0, currentProject.keywords.Length)];
                    randomSearchText = "Contact";
                    //keyWordArray = randomSearchText.Split(' ');
                    //if (keyWordArray != null && keyWordArray.Length > 0)
                    //{
                    //    spacecount = randomSearchText.Split(' ').Length - 1;
                    //    wordIndex = RandomNumber(0, spacecount + 1);
                    //}
                    //for (int i = 0; i <= wordIndex; i++)
                    //{
                    //    if (randomSearchText.IndexOf(" ") != -1)
                    //    {
                    //        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
                    //    }
                    //}
                    //if (randomSearchText.LastIndexOf(" ") != -1)
                    //{
                    //    randomSearchText = randomSearchText.Substring(randomSearchText.LastIndexOf(" ") + 1);
                    //}
                    objresult.innerlink = randomSearchText;
                    objchrome.RandomLinks(objresult);
                    Thread.Sleep(RandomNumber(1500, 3500));

                    jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    Thread.Sleep(RandomNumber(1000, 1500));
                    jse.ExecuteScript("window.scrollBy(0,-1131)");
                    Thread.Sleep(RandomNumber(1000, 1500));
                    jse.ExecuteScript("window.scrollBy(0,853)");
                    Thread.Sleep(RandomNumber(800, 1200));
                    jse.ExecuteScript("window.scrollBy(0,323)");
                    Thread.Sleep(RandomNumber(800, 1200));
                    jse.ExecuteScript("window.scrollBy(0,-1278)");
                    Thread.Sleep(RandomNumber(38000, 65999));
                    jse.ExecuteScript("return window.stop");                    
                    objchrome.closeConnection(objresult);
                    //objreport.google_page = objsearch.rank.ToString();
                    objreport.google_page = currentpage.ToString();
                    TimeSpan timeElapsed = DateTime.Now - startTime;
                    objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
                    objreport.keyword = objsearch.searchtxt;
                    objreport.project_id = currentProject._id;
                    objreport.visited_link = currentProject.website;
                }
                else if (!found)
                {
                    Debug.WriteLine("Failed to find link; adding company name to search term");
                    Console.WriteLine("Failed to find link; adding company name to search term");
                    page = 0;

                    objsearch.searchtxt = currentProject.keywords[keywordindex] + " " + currentProject.company_name[0];
                    objsearch.website = currentProject.website;

                    startTime = DateTime.Now;
                    Thread.Sleep(3000);
                    objchrome.gethtml(objsearch);
                    page = 1;
                    currentpage = 1;
                    objresult = objchrome.getelement(objsearch);

                    while (page <= 5 && objresult.elements.Count == 0)
                    {
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,-1275)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,880)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,390)");
                        Thread.Sleep(RandomNumber(5000, 6999));
                        objchrome.getnextpage(objresult);
                        objresult = objchrome.getelement(objsearch);
                        page++;
                        currentpage = page;
                    }

                    if (objresult.elements.Count > 0)
                    {
                        //objchrome.getlinks(objresult);
                        randomClicks = true;
                        found = true;
                        objchrome.getRank(objresult);
                        //objchrome.closeConnection(objresult);
                        //objreport.google_page = objsearch.rank.ToString();
                        objreport.google_page = currentpage.ToString();
                        //TimeSpan timeElapsed = DateTime.Now - startTime;
                        //objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
                        objreport.keyword = objsearch.searchtxt;
                        objreport.project_id = currentProject._id;
                        objreport.visited_link = currentProject.website;
                        //publish = request.PublishReport(objreport);
                        //Debug.Write("Publish Report Done for " + currentProject.website);
                        //Console.WriteLine("Publish Report Done for " + currentProject.website);
                    }
                    else
                    {
                        Debug.WriteLine("Failed to find link; adding company URL to search term");
                        Console.WriteLine("Failed to find link; adding company URL to search term");

                        objsearch.searchtxt = currentProject.keywords[keywordindex] + " " + currentProject.website;
                        objsearch.website = currentProject.website;

                        startTime = DateTime.Now;
                        Thread.Sleep(3000);
                        objchrome.gethtml(objsearch);
                        page = 1;
                        currentpage = 1;
                        objresult = objchrome.getelement(objsearch);
                        while (page <= 5 && objresult.elements.Count == 0)
                        {
                            jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                            Thread.Sleep(RandomNumber(1000, 1500));
                            jse.ExecuteScript("window.scrollBy(0,-1275)");
                            Thread.Sleep(RandomNumber(1000, 1500));
                            jse.ExecuteScript("window.scrollBy(0,880)");
                            Thread.Sleep(RandomNumber(800, 1200));
                            jse.ExecuteScript("window.scrollBy(0,390)");
                            Thread.Sleep(RandomNumber(5000, 6999));
                            objchrome.getnextpage(objresult);
                            objresult = objchrome.getelement(objsearch);
                            page++;
                            currentpage = page;
                        }

                        if (objresult.elements.Count > 0)
                        {

                            Debug.WriteLine("Found site with URL " + currentProject.website);
                            Console.WriteLine("Found site with URL " + currentProject.website);
                            Debug.WriteLine("Using " + objsearch.searchtxt + "  On page " + objsearch.page + "  Ranked:  " + objsearch.rank);
                            Console.WriteLine("Using " + objsearch.searchtxt + "  On page " + objsearch.page + "  Ranked:  " + objsearch.rank);
                            Thread.Sleep(RandomNumber(2000, 6599));

                            //objchrome.getlinks(objresult);
                            randomClicks = true;
                            found = true;
                            objchrome.getRank(objresult);
                            //objchrome.closeConnection(objresult);
                            //objreport.google_page = objsearch.rank.ToString();
                            objreport.google_page = currentpage.ToString();
                            //TimeSpan timeElapsed = DateTime.Now - startTime;
                            //objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
                            objreport.keyword = objsearch.searchtxt;
                            objreport.project_id = currentProject._id;
                            objreport.visited_link = currentProject.website;
                            //publish = request.PublishReport(objreport);
                            //Debug.Write("Publish Report Done for " + currentProject.website);
                            //Console.WriteLine("Publish Report Done for " + currentProject.website);

                        }
                        else
                        {
                            page = 0;
                            currentpage = 0;
                            Debug.WriteLine("Didn't find site with URL " + currentProject.website);
                            Console.WriteLine("Didn't find site with URL " + currentProject.website);
                        }
                    }

                    if (randomClicks)
                    {
                        Debug.WriteLine("Found site with Search Term + URL or Company Name; perfroming RandomClicks()");
                        Console.WriteLine("Found site with Search Term + URL or Company Name; perfroming RandomClicks()");

                        startTime = DateTime.Now;
                        //TimeSpan timeElapsed = DateTime.Now - startTime;
                        //objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
                        
                        
                        //objchrome.getRank(objresult);
                        Thread.Sleep(RandomNumber(20000, 65999));
                        objchrome.getlinks(objresult);


                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,-1235)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,872)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,355)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,-1214)");


                        //Adding long sleep per Chris
                        Thread.Sleep(RandomNumber(20000, 40000));


                        //Basically, this swaps out static "About" and "Contact" RandomClicks for single-word values actually in our keywords.
                        //int spacecount = 0;
                        //int wordIndex = 0;
                        //int j = 0;
                        //String randomSearchText = currentProject.keywords[RandomNumber(0, currentProject.keywords.Length)];
                        String randomSearchText = "About";
                        //String[] keyWordArray = randomSearchText.Split(' ');
                        //if (keyWordArray != null && keyWordArray.Length > 0)
                        //{
                        //    spacecount = randomSearchText.Split(' ').Length - 1;
                        //    wordIndex = RandomNumber(0, spacecount + 1);
                        //}
                        //for (j = 0; (j <= wordIndex); j++)
                        //{
                        //    if (randomSearchText.IndexOf(" ") != -1)
                        //    {
                        //        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
                        //    }
                        //}
                        objresult.innerlink = randomSearchText;
                        objchrome.RandomLinks(objresult);
                        Thread.Sleep(RandomNumber(25000, 80000));

                        //spacecount = 0;
                        //wordIndex = 0;
                        //randomSearchText = currentProject.keywords[RandomNumber(0, currentProject.keywords.Length)];
                        randomSearchText = "Contact";
                        //keyWordArray = randomSearchText.Split(' ');
                        //if (keyWordArray != null && keyWordArray.Length > 0)
                        //{
                        //    spacecount = randomSearchText.Split(' ').Length - 1;
                        //    wordIndex = RandomNumber(0, spacecount + 1);
                        //}
                        //for (j = 0; (j <= wordIndex); j++)
                        //{
                        //    if (randomSearchText.IndexOf(" ") != -1)
                        //    {
                        //        randomSearchText = randomSearchText.Substring(0, randomSearchText.IndexOf(" "));
                        //    }
                        //}
                        objresult.innerlink = randomSearchText;
                        objchrome.RandomLinks(objresult);
                        Thread.Sleep(RandomNumber(1500, 3500));

                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,-1131)");
                        Thread.Sleep(RandomNumber(1000, 1500));
                        jse.ExecuteScript("window.scrollBy(0,853)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,323)");
                        Thread.Sleep(RandomNumber(800, 1200));
                        jse.ExecuteScript("window.scrollBy(0,-1278)");
                        Thread.Sleep(RandomNumber(38000, 70000));
                        jse.ExecuteScript("return window.stop");
                        objchrome.closeConnection(objresult);
                        //objreport.google_page = "0";
                        //objreport.google_page = currentpage.ToString();
                        TimeSpan timeElapsed = DateTime.Now - startTime;
                        objreport.stay_page = timeElapsed.TotalMilliseconds.ToString("00000");
                        ////objreport.keyword = currentProject.keywords[keywordindex];
                        //objreport.keyword = objresult.searchtxt;
                        ////this is the REAL search text we used.
                        //objreport.project_id = currentProject._id;
                        //objreport.visited_link = currentProject.website;
                    }
                }
                //this won't ever run...
                //else
                //{
                //    objchrome.closeConnection(objresult);
                //    objreport.google_page = "0";
                //    TimeSpan timeElapsed = DateTime.Now - startTime;
                //    objreport.stay_page = "0";
                //    objreport.keyword = objsearch.searchtxt;
                //    objreport.project_id = currentProject._id;
                //    objreport.visited_link = currentProject.website;

                //}



                String searchItemFinishedURL = "https://my.site-pop.com:8081/mark_search_finished/?searchItem=" + currentProject.new_search_item_id;



                if (!found)
                {

                    searchItemFinishedURL = searchItemFinishedURL + "&status=I&completedStatusVerbiage=notfound";

                    Debug.WriteLine("Site not found using any method.");
                    Console.WriteLine("Site not found using any method.");
                    objchrome.closeConnection(objresult);
                    objreport.google_page = "0";
                    TimeSpan timeElapsed = DateTime.Now - startTime;
                    objreport.stay_page = "0";
                    objreport.keyword = currentProject.keywords[keywordindex];
                    objreport.project_id = currentProject._id;
                    objreport.visited_link = currentProject.website;

                }
                else
                {
                    searchItemFinishedURL = searchItemFinishedURL + "&status=C&completedStatusVerbiage=completed";

                }




                //So.. new dev queue marks search item complete here
                //if (useDevQueue)
                //{
                HttpModule.MarkNewSearchItemFinished(searchItemFinishedURL);
                Debug.WriteLine("devQueue MarkNewSearchItemFinished " + currentProject.new_search_item_id);
                Console.WriteLine("devQueue MarkNewSearchItemFinished " + currentProject.new_search_item_id);
                //}
                //else
                //{

                objreport.app_version = app_version;
                publish = request.PublishReport(objreport, currentProject.project_type);
                Debug.WriteLine("Publish Report Done for " + currentProject.website);
                Console.WriteLine("Publish Report Done for " + currentProject.website);
                //}


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception doSearchKeywordsRepeatedly(), closing connection " + ex.ToString());
                Console.WriteLine("Exception doSearchKeywordsRepeatedly(), closing connection " + ex.ToString());
                objchrome.closeConnection(objresult);
                //Console.WriteLine("SearchKeywordError: " + ei.Message);
            }




            //Process your long running  task

            // publish; //Set your Result of the long running task

        }


        ////function to getKeywords for nearby project
        //private RequestBO.Project getKeywordsToSearch(string code)
        //{
        //    try
        //    {
        //        HttpModule httpModule = new HttpModule();
        //        RequestBO.Project currentproject = httpModule.getProjectKeywords(code);
        //        return currentproject;

        //        //string[] lstKeywordsToSearch = currentproject.keywords;
        //        //toSearch = true;
        //        //return lstKeywordsToSearch;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("getKeywordsToSearch");
        //        Debug.WriteLine("Exception getKeywordsToSearch() " + ex.ToString());
        //        Console.WriteLine("Exception getKeywordsToSearch() " + ex.ToString());
        //        //Thread.Sleep(6000);
        //        //getKeywordsToSearch(code);
        //    }
        //    return null;
        //}

        private double getDistance(double sLat, double sLong, double pLat, double pLong)
        {
            //User Self Lat Long
            var sCoord = new GeoCoordinate(sLat, sLong);

            //Project Lat Long
            var eCoord = new GeoCoordinate(pLat, pLong);

            return sCoord.GetDistanceTo(eCoord);
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = rank.report_url;
            ProcessStartInfo sInfo = new ProcessStartInfo(url);
            Process.Start(sInfo);
        }

        private void paneldetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lbcontact_Click(object sender, EventArgs e)
        {

        }

        private void lbphone_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Login_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        //private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Show();
        //    this.WindowState = FormWindowState.Normal;
        //    notifyIcon.Visible = false;
        //}

        private void lbchecked_Click(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            notifyIcon.Visible = true;
        }
        private void Disconnect()
        {
            marketing_assistant.Properties.Settings.Default.is_login = false;
            //marketing_assistant.Properties.Settings.Default.email = "";
            //marketing_assistant.Properties.Settings.Default.project = "";
            marketing_assistant.Properties.Settings.Default.Save();

            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            key.SetValue("is_login", "No");
            //key.SetValue("email", "");
            //key.SetValue("project", "");
            key.Close();

            HttpModule httpModule = new HttpModule();
            httpModule.PerformLogout();

            //txtEmail.Text = "";
            //txtProj.Text = "";

            paneldetails.Hide();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //Environment.Exit(0);
            //Login.ActiveForm.Close();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //why empty these fields?
            marketing_assistant.Properties.Settings.Default.is_login = false;
            //marketing_assistant.Properties.Settings.Default.email = "";
            //marketing_assistant.Properties.Settings.Default.project = "";
            marketing_assistant.Properties.Settings.Default.Save();

            key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            key.SetValue("is_login", "No");
            //key.SetValue("email", "");
            //key.SetValue("project", "");
            key.Close();

            HttpModule httpModule = new HttpModule();
            httpModule.PerformLogout();

            txtEmail.Text = "";
            txtProj.Text = "";

            paneldetails.Hide();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Environment.Exit(0);
            //Login.ActiveForm.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Debug.WriteLine("AutoUpdate: Is Network Deployed");
                Console.WriteLine("AutoUpdate: Is Network Deployed");
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo infoUC = updateCheck.CheckForDetailedUpdate();
                //
                if (infoUC.UpdateAvailable)
                {
                    DialogResult result2 = MessageBox.Show("There is a newer version of Marketing Assistant available. Click 'Ok' to update",
                            "Marketing Assistant",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    if (result2 == DialogResult.OK)
                    {
                        // Thread.Sleep(6000);
                        updateCheck.Update();
                    }
                    // key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
                    // MessageBox.Show("The application has been upgraded to latest version, Please disconnect and re-connect the Application.", "Marketing Assistant");

                    DialogResult result3 = MessageBox.Show("The application has been upgraded to latest version, Please restart the Marketing Assistant.",
                           "Marketing Assistant",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2);
                    if (result3 == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Your application is up-to-date.", "Marketing Assistant",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Debug.WriteLine("AutoUpdate: Is NOT Network Deployed");
                Console.WriteLine("AutoUpdate: Is NOT Network Deployed");
                //Debug.WriteLine("ApplicationDeployment.CurrentDeployment: " + ApplicationDeployment.CurrentDeployment.ToString());
                //Console.WriteLine("ApplicationDeployment.CurrentDeployment: " + ApplicationDeployment.CurrentDeployment.ToString());


                try
                {
                    // Setup the trust level
                    var deployment = ApplicationDeployment.CurrentDeployment;
                    var appId = new ApplicationIdentity(deployment.UpdatedApplicationFullName);
                    var unrestrictedPerms = new PermissionSet(PermissionState.Unrestricted);
                    var appTrust = new ApplicationTrust(appId)
                    {
                        DefaultGrantSet = new PolicyStatement(unrestrictedPerms),
                        IsApplicationTrustedToRun = true,
                        Persist = true
                    };
                    ApplicationSecurityManager.UserApplicationTrusts.Add(appTrust);
                    //manual trust level set, just in case..
                    // Check for update
                    var info = deployment.CheckForDetailedUpdate();
                    if (info.UpdateAvailable)
                    {
                        DialogResult result2 = MessageBox.Show("There is a newer version of Marketing Assistant available. Click 'Ok' to update",
                                "Marketing Assistant",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        if (result2 == DialogResult.OK)
                        {
                            deployment.Update();
                        }
                        DialogResult result3 = MessageBox.Show("The application has been upgraded to latest version, Please restart the Marketing Assistant.",
                               "Marketing Assistant",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2);
                        if (result3 == DialogResult.OK)
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your application is up-to-date !!!.", "Marketing Assistant",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button2);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Manual update policy set failed: " + ex.ToString());
                    Debug.WriteLine("Manual update policy set failed: " + ex.ToString());

                    MessageBox.Show("Please disconnect and restart your Application to check for update.", "Marketing Assistant",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button2);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //this is a lie - should be every 11hrs...
        private void timerUpdateCheck11hrs_Tick(object sender, EventArgs e)
        {
            //key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            //if (key != null && key.GetValue("is_login") != null)
            //{
            //    if (key.GetValue("is_login").ToString() == "Yes")
            //    {
            //        HttpModule httpModule = new HttpModule();
            //        if (txtProj.Text.Length > 0 && txtEmail.Text.Length > 0)
            //        {
            //            Console.WriteLine("HitLink: " + DateTime.Now.ToString());
            //            Debug.WriteLine("HitLink: " + DateTime.Now.ToString());
            //            //this logs in again for some reason
            //            HitRequest();
            //        }
            //    }
            //}
            //key.Close();

            ////Update-check uncomment start -csv

            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            //    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
            //    if (info.UpdateAvailable)
            //    {
            //        // MessageBox.Show("New Update Available. Please update.", "Marketing Assistant");
            //        // string update_available = "New Update Available";
            //        DialogResult result4 = MessageBox.Show("There is a newer version of Marketing Assistant available. Click 'Ok' to update.",
            //               "Marketing Assistant",
            //               MessageBoxButtons.OK,
            //               MessageBoxIcon.Information);
            //        if (result4 == DialogResult.OK)
            //        {
            //            // Thread.Sleep(6000);
            //            updateCheck.Update();
            //        }
            //        // key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MarketingAssistant", true);
            //        // MessageBox.Show("The application has been upgraded to latest version, Please disconnect and re-connect the Application.", "Marketing Assistant");

            //        DialogResult result5 = MessageBox.Show("The application has been upgraded to latest version, Please restart the Marketing Assistant.",
            //               "Marketing Assistant",
            //               MessageBoxButtons.OK,
            //               MessageBoxIcon.Question,
            //               MessageBoxDefaultButton.Button2);
            //        if (result5 == DialogResult.OK)
            //        {
            //            Environment.Exit(0);
            //        }
            //    }

            //    //Update-check uncomment end -csv
            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void timerSearchKeywords_DoBackgroundWork(object sender, DoWorkEventArgs e)
        //{
        //    Debug.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);
        //    Console.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);


        //    //RANDOMIZATION MOVED INTO NODE - NOT HAPPENING HERE ANY MORE
        //    //projectIndex = RandomNumber(0, lstProjectQueueSorted.Count - 1);

        //    //No more of this timespan stuff.. we're going to run 24/7 with regular thread-timers

        //    //TimeSpan start = new TimeSpan(0, 0, 0); // 23:59:59 runtime with refresh, so as to not break any current code dependent on potential process cycling -csv 
        //    //TimeSpan end = new TimeSpan(23, 59, 59); // one minute refresh-time
        //    //TimeSpan now = DateTime.Now.TimeOfDay;
        //    //if (toSearch && now > start && now < end)
        //    //{

        //    try
        //    {
        //        if (lstProjectQueueSorted.Count() > 0)
        //        {
        //            if (projectIndex >= lstProjectQueueSorted.Count())
        //            {
        //                projectIndex = 0;
        //            }

        //            RequestBO.ProjectQueueSorted searchProject = lstProjectQueueSorted[projectIndex];
        //            RequestBO.Project currentProject = getKeywordsToSearch(searchProject.code);
        //            projectIndex++;

        //            if (currentProject.keywords.Length > 0)
        //            {
        //                //RequestBO.Project currentproject = httpModule.getProjectKeywords(code);
        //                doSearchKeywordsRepeatedly(currentProject);
        //                Debug.WriteLine("projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
        //                Console.WriteLine("projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
        //            }
        //            else
        //            {
        //                Debug.WriteLine("SKIPPING (no search keywords) projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
        //                Console.WriteLine("SKIPPING (no search keywords) projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //        Console.WriteLine(ex.ToString());
        //    }


        //}

        private void timerSearchKeywords_DoBackgroundWork(object sender, DoWorkEventArgs e)
        {
            //if (useDevQueue)
            //{
                Debug.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);
                Console.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);

            try
            {

                RequestBO.Project currentProject = new RequestBO.Project();
                HttpModule mod = new HttpModule();
                RequestBO.NewSearchItem searchItem = mod.GetNewSearchItem();

                if (searchItem == null || searchItem.code == "EMPTY___QUEUE")
                {
                    //do nothing - queue is empty.
                    Debug.WriteLine("timerSearchKeywords_Tick_EMPTY__QUEUE: " + DateTime.Now);
                    Console.WriteLine("timerSearchKeywords_Tick_EMPTY__QUEUE: " + DateTime.Now);
                }
                else
                {
                    currentProject.code = searchItem.code;
                    currentProject.company_name = searchItem.company_name.Split(',');
                    String[] keywords = new string[1];
                    keywords[0] = searchItem.keyword;
                    currentProject.keywords = keywords;
                    currentProject._id = searchItem.project_id;
                    currentProject.new_search_item_id = searchItem._id;
                    currentProject.website = searchItem.website;
                    Debug.WriteLine("New search started for " + searchItem.code + " " + searchItem.website);
                    Console.WriteLine("New search started for " + searchItem.code + " " + searchItem.website);
                    doSearchKeywordsRepeatedly(currentProject);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString() + "======" + DateTime.Now);
                Console.WriteLine(ex.ToString() + "======" + DateTime.Now);
            }

            //}
            //else
            //{
            //    Debug.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);
            //    Console.WriteLine("timerSearchKeywords_Tick: " + DateTime.Now);

            //    try
            //    {
            //        if (lstProjectQueueSorted.Count() > 0)
            //        {
            //            if (projectIndex >= lstProjectQueueSorted.Count())
            //            {
            //                projectIndex = 0;
            //            }

            //            RequestBO.ProjectQueueSorted searchProject = lstProjectQueueSorted[projectIndex];
            //            RequestBO.Project currentProject = getKeywordsToSearch(searchProject.code);
            //            projectIndex++;

            //            if (currentProject.keywords.Length > 0)
            //            {
            //                //RequestBO.Project currentproject = httpModule.getProjectKeywords(code);
            //                doSearchKeywordsRepeatedly(currentProject);
            //                Debug.WriteLine("projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
            //                Console.WriteLine("projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
            //            }
            //            else
            //            {
            //                Debug.WriteLine("SKIPPING (no search keywords) projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());
            //                Console.WriteLine("SKIPPING (no search keywords) projectindex " + projectIndex + " code " + lstProjectQueueSorted[projectIndex].code + " count " + lstProjectQueueSorted.Count());

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine(ex.ToString());
            //        Console.WriteLine(ex.ToString());
            //    }
            //}


        }

        private void timerSearchKeywords_Background()
        {
            this.timerSearchKeywords.Enabled = false;
            this.timerSearchKeywords.Stop();
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += timerSearchKeywords_DoBackgroundWork;
            bw.RunWorkerCompleted += Bw_timerSearchKeywordsCompleted;
                //Could add some UI update stuff hypothetically... would need another background poll thread, though.
            bw.RunWorkerAsync();
            return;

        }

        private void timerSearchKeywords_Tick(object sender, EventArgs e)
        {
            timerSearchKeywords_Background();
        }

        #region populateUI
        public void populateUI(RequestBO.Rootobject stat)
        {
            bool value = stat.users.Any();
            //if (value == false)
            //{ }
            //else
            //{
            //lbusername.Text = stat.users[0].first_name + " " + stat.users[0].last_name;
            //}
            //lbusername.Refresh();
            //lbusername.Text = stat.resp.contact_name;
            //lbusername.Text = stat.resp.contact_name;
            if (value == false)
                lbcontact.Text = stat.resp.contact_name;
            else
                lbcontact.Text = stat.resp.contact_name;

            lbcontact.Refresh();
            //int length = stat.project.company_name.Length;
            //for (int i = 0; i < length; i++)
            //{
            //    lbproject.Text = lbproject.Text + stat.project.company_name[i];
            //}
            lbphone.Text = stat.resp.contact_1;
            lbphone.Refresh();
            lbproject.Text = stat.resp.name;
            lbproject.Refresh();
            lbwebsite.Text = stat.resp.website;
            lbwebsite.Refresh();
            //int lth = stat.project.contact_email.Length;
            //for (int i = 0; i < lth; i++)
            //{
            //    lbemail.Text = lbemail.Text + "," + stat.project.contact_email[i];
            //}
            lbemail.Text = stat.resp.email;
            lbemail.Refresh();

            if (string.Equals(stat.resp.show_custom_message.ToLower(), "y"))
            {
                customeMessagePanel.Show();
                lblCustomMessageValue.Text = stat.resp.custom_message;
                lblCustomMessageValue.Refresh();
            }

            //Issues with GTMatrix not updating/sending us correct site status - so hardcoding it to Live for now. 
            rank.site_status = "LIVE";

            //lbsite.Text = rank.site_status.ToString();
            //lbsite.Refresh();

            //lbchecked.Text = rank.lastReport.ToString();
            //lbchecked.Refresh();
            int pix = agencyDetailsPanel.Top;
            // dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = rank.KeywordWise.Select(o => new { Column1 = o.keyword, Column2 = o.rank, Column3 = o.position }).ToList();
            //dataGridView1.DataSource = rank.KeywordWise.Select(o => new { Column1 = o.keyword, Column2 = o.rank }).ToList();
            dataGridView1.Columns[0].Width = 270;

            var height = 30;
            // dataGridView1.DataSource = rank.KeywordWise;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                height += dr.Height;
            }

            dataGridView1.Height = height;

            //panel3.Height = paneldetails.Height + height;
            //paneldetails.Height = paneldetails.Height + height;
            this.Height = 489;
            paneldetails.Height = 480;
            dataGridView1.Show();
            dataGridView1.Refresh();

            //if (rank.site_status.ToUpper().Equals("LIVE"))
            //{
            //    button2.BackColor = Color.Green;
            //    String fake_load_time = "2." + RandomNumber(0, 899).ToString() + "s";
            //    lbload.Text = fake_load_time;

            //    //lbload.Text = rank.page_load_time.ToString() + "s";
            //    lbload.Refresh();
            //}
            //else
            //{
            //    button2.BackColor = Color.Red;
            //    lbload.Text = "N/A";
            //    lbload.Refresh();
            //}

            //button2.Visible = true;
            //button2.Show();
            //button2.Refresh();
            //agencyDetailsPanel.Top = panel3.Height+10;


            //lbstatus.Refresh();
            paneldetails.Show();
            paneldetails.Refresh();
        }
        #endregion

        #region Test
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Debug.WriteLine("AutoUpdate: Is Network Deployed");
                Console.WriteLine("AutoUpdate: Is Network Deployed");
                try
                {
                    ApplicationDeployment updateCheckTest = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo infoUCTest = updateCheckTest.CheckForDetailedUpdate();
                }
                catch(Exception exception)
                {
                    Debug.WriteLine("Step1 - Wrong Network Deployed---------", exception.ToString());
                    Console.WriteLine("Step1 - Initializing Is Network Deployed-------------", exception.ToString());
                }

                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo infoUC = updateCheck.CheckForDetailedUpdate();

                if(infoUC == null)
                {
                    Debug.WriteLine("Step2 - Wrong Network Deployed---------");
                    Console.WriteLine("Step2 - Initializing Is Network Deployed-------------");
                }
                //
                if (infoUC.UpdateAvailable)
                {
                    DialogResult result2 = MessageBox.Show("Success-----There is a newer version of Marketing Assistant available. Click 'Ok' to update",
                            "Marketing Assistant",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    if (result2 == DialogResult.OK)
                    {
                        Thread.Sleep(6000);
                        updateCheck.Update();
                    }

                    DialogResult result3 = MessageBox.Show("Success------The application has been upgraded to latest version, Please restart the Marketing Assistant.",
                           "Marketing Assistant",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2);
                    if (result3 == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Your application is up-to-date.", "Marketing Assistant",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Debug.WriteLine("Step3 - Wrong Network Deployed");
                Console.WriteLine("Step3 - Is NOT Network Deployed");
                Debug.WriteLine("Detail.........ApplicationDeployment.CurrentDeployment: " + ApplicationDeployment.CurrentDeployment.ToString());
                Console.WriteLine("Detail......ApplicationDeployment.CurrentDeployment: " + ApplicationDeployment.CurrentDeployment.ToString());

                try
                {
                    // Setup the trust level
                    var deployment = ApplicationDeployment.CurrentDeployment;
                    var appId = new ApplicationIdentity(deployment.UpdatedApplicationFullName);
                    //var appId2 = new ApplicationIdentity(deployment.UpdatedVersion.ToString());

                    var unrestrictedPerms = new PermissionSet(PermissionState.Unrestricted);
                    var appTrust = new ApplicationTrust(appId)
                    {
                        DefaultGrantSet = new PolicyStatement(unrestrictedPerms),
                        IsApplicationTrustedToRun = true,
                        Persist = true
                    };
                    ApplicationSecurityManager.UserApplicationTrusts.Add(appTrust);
                    var info = deployment.CheckForDetailedUpdate();
                    if (info.UpdateAvailable)
                    {
                        DialogResult result2 = MessageBox.Show("Success---There is a newer version of Marketing Assistant available. Click 'Ok' to update",
                                "Marketing Assistant",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        if (result2 == DialogResult.OK)
                        {
                            deployment.Update();
                        }
                        DialogResult result3 = MessageBox.Show("Success----The application has been upgraded to latest version, Please restart the Marketing Assistant.",
                               "Marketing Assistant",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2);
                        if (result3 == DialogResult.OK)
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your application is up-to-date !!!.", "Marketing Assistant",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button2);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine("Step4 -Deployment update policy set failed: " + exception.ToString());
                    Debug.WriteLine("Step4 - Deployment update policy set failed: " + exception.ToString());

                    MessageBox.Show("Please disconnect and restart your Application to check for update.", "Marketing Assistant",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button2);
                }
            }
        }
        #endregion
    }
}
