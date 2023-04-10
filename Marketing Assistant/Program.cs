using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;
using marketing_assistant;

namespace Marketing_Assistant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //GetGooglePagePosition();

            // IWebDriver driver = new ChromeDriver(@"C:\Users\hgu027\Documents\Visual Studio 2015\Projects\Marketing_Assistant\packages\Selenium.Chrome.WebDriver.2.40\driver");

            //// IWebDriver driver = new ChromeDriver();
            // //Thread.Sleep(5000);
            // //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            // driver.Manage().Window.Maximize();
            // driver.Navigate().GoToUrl("https://www.google.com");
            // IWebElement wb = driver.FindElement(By.ClassName("gsfi"));
            // wb.SendKeys("Sachin");
            // wb.SendKeys(Keys.Enter);


            // IList< IWebElement> elements = driver.FindElements(By.TagName("a")); //Will give you all the links on page.
            // string link1Text = elements[0].Text; //Will give you the text.   
            // string link1 = elements[0].GetAttribute("href"); // will give you the link url.



            // driver.Navigate().GoToUrl("http://google.com");

            // IWebElement element = driver.FindElement(By.Id("gbqfq"));
            // element.SendKeys("selenium webdriver");

            // // Get the search results panel that contains the link for each result.
            // IWebElement resultsPanel = driver.FindElement(By.Id("search"));

            // // Get all the links only contained within the search result panel.
            // ReadOnlyCollection<IWebElement> searchResults = resultsPanel.FindElements(By.XPath(".//a"));

            // // Print the text for every link in the search results.
            // foreach (IWebElement result in searchResults)
            // {
            //     Console.WriteLine(result.Text);
            // }
            //FetchUrl obj = new FetchUrl();
            //obj.getlink();
            //string uriString = "http://www.google.com/search";
            //string keywordString = "dentist in houston";

            //WebClient webClient = new WebClient();

            //NameValueCollection nameValueCollection = new NameValueCollection();
            //nameValueCollection.Add("q", keywordString);

            //webClient.QueryString.Add(nameValueCollection);

            //String Text = webClient.DownloadString(uriString);
            // Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool onlyInstance = false;
            Mutex mutex = new Mutex(true, "Marketing_Assistant", out onlyInstance);
            if (!onlyInstance)
            {
                MessageBox.Show("Please check System Tray or Task Manager.", "Marketing Assistant is already running!");
                // Mutex.TryOpenExisting("Marketing_Assistant", out System.Threading.Mutex result);
                return;
            }

            //FileStream ostrm;
            //StreamWriter writer;
            //TextWriter oldOut = Console.Out;
            //try
            //{
            //    ostrm = new FileStream("./ma19.log", FileMode.OpenOrCreate, FileAccess.Write);
            //    writer = new StreamWriter(ostrm);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Cannot open Redirect.txt for writing");
            //    Console.WriteLine(e.Message);
            //    return;
            //}
            //Console.SetOut(writer);
            //Console.WriteLine("This is a line of text");
            //Console.WriteLine("Everything written to Console.Write() or");
            //Console.WriteLine("Console.WriteLine() will be written to a file");
            //Console.SetOut(oldOut);

            //writer.Close();
            //ostrm.Close();
            //Console.WriteLine("Done");


            //This will log all console output
            FileStream filestream = new FileStream("ma19.log", FileMode.OpenOrCreate);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);


            Application.Run(new Login());
            //Application.Run(new SearchForm());

            // GC.KeepAlive(mutex);

            // Application.Run(new Login());

        }


        #region Test
        //private static int AnotehrFindPosition(string source, Uri uri)
        //{
        //    HtmlWeb hw = new HtmlWeb();
        //    var doc = hw.Load("https://www.google.com/search?q=ambient+footwell+lighting&start=30");
        //    foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
        //    {
        //        var test = link;
        //        var value = test.ParentNode;
        //    }

        //    return 0;
        //}

        //private static void GetGooglePagePosition()
        //{
        //    var chromeOptions = new ChromeOptions();
        //    var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        //    if (!File.Exists(chromePath))
        //        chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

        //    chromeOptions.BinaryLocation = chromePath;
        //    chromeOptions.AddArguments("--headless");
        //    string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        //    ChromeDriverService service = ChromeDriverService.CreateDefaultService(appPath);
        //    service.SuppressInitialDiagnosticInformation = true;
        //    service.HideCommandPromptWindow = true;

        //    var driver = new ChromeDriver(service, chromeOptions);

        //    string keyword = "ambient footwell lighting"; //"led vehicle lighting";

        //    var raw = "http://www.google.com/search?q={0}&start=30";
        //    var search = string.Format(raw, HttpUtility.UrlEncode(keyword));

        //    //driver.Navigate().GoToUrl("https://www.google.com/search?q=ambient+footwell+lighting&ei=tzWFYKDEB6rG0PEPnKik4AM&start=30&sa=N&ved=2ahUKEwjg-4X3iZnwAhUqIzQIHRwUCTw4FBDy0wN6BAgBEDo&biw=1536&bih=754");
        //    driver.Navigate().GoToUrl(search);
        //    string source = driver.PageSource;

        //    var uri = new Uri("https://u-hue.com");
        //    var index = FindPosition(source, uri);

        //    var myValue = index;
        //}

        //private static int FindPosition(string source, Uri uri)
        //{
        //    //var lookup = "<a href=\"\\w+[a-zA-Z_.-?=/]*\" data-ved=\"";
        //    //var lookup = "/<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1/";
        //    //var lookup = "(<div class=\"yuRUbf\">)(<a href=\"https?://)(.*?)\" data-ved=\"(.*?)\" ping=\"";
        //    var lookup = "(<div class=\"yuRUbf\">)(<a href=\"https?://)(.*?)\" data-ved=\"";
        //    MatchCollection matches = Regex.Matches(source, lookup);

        //    for (var i = 0; i < matches.Count; i++)
        //    {
        //        var match = matches[i].Groups[3].Value;
        //        if (match.Contains(uri.Host))
        //            return i + 1;
        //    }

        //    return 0;
        //}
        #endregion
    }
}
