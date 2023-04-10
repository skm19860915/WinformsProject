using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using System.Configuration;
using System.Windows.Forms;
using testc = OpenQA.Selenium;

namespace Marketing_Assistant
{
    class FetchUrl
    {
        public bool exceptionRaised = false;

        HtmlAgilityPack.HtmlDocument htmlSnippet = new HtmlAgilityPack.HtmlDocument();

        public RequestBO.SearchParam createConnection()
        {
            try
            {
                RequestBO.SearchParam objsearch = new RequestBO.SearchParam();
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--headless");
                chromeOptions.AddArguments("--no-sandbox");

                String[] lstUserAgents = new String[30];
                lstUserAgents[0] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0";
                lstUserAgents[1] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:54.0) Gecko/20100101 Firefox/55.2";
                lstUserAgents[2] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";
                lstUserAgents[3] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";
                lstUserAgents[4] = "Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2101.33 Safari/537.36";
                lstUserAgents[5] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
                lstUserAgents[6] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                lstUserAgents[7] = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
                lstUserAgents[8] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:62.0) Gecko/20100101 Firefox/62.0";
                lstUserAgents[9] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240";
                lstUserAgents[10] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";
                lstUserAgents[11] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
                lstUserAgents[12] = "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko";
                lstUserAgents[13] = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                lstUserAgents[14] = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; MAARJS; rv:11.0) like Gecko";
                lstUserAgents[15] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                lstUserAgents[16] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                lstUserAgents[17] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.157 Safari/537.36";
                lstUserAgents[18] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10; rv:33.0) Gecko/20100101 Firefox/33.0";
                lstUserAgents[19] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A";
                lstUserAgents[20] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.59.10 (KHTML, like Gecko) Version/5.1.9 Safari/534.59.10";
                lstUserAgents[21] = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_6; en-en) AppleWebKit/533.19.4 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4";
                lstUserAgents[22] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36";
                lstUserAgents[23] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/603.2.5 (KHTML, like Gecko) Version/10.1.1 Safari/603.2.5";
                lstUserAgents[24] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/601.6.17 (KHTML, like Gecko) Version/9.1.1 Safari/601.6.17";
                lstUserAgents[25] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/602.3.12 (KHTML, like Gecko) Version/10.0.2 Safari/602.3.12";
                lstUserAgents[26] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56";
                lstUserAgents[27] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/601.2.7 (KHTML, like Gecko) Version/9.0.1 Safari/601.2.7";

                Random random = new Random();
                String currentAgent = lstUserAgents[random.Next(0, 27)]; //apparently max is not inclusive...
                chromeOptions.AddArguments("--user-agent=" + currentAgent);

                //IWebDriver driver = new ChromeDriver(ConfigurationManager.AppSettings["Path"], chromeOptions);
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                Debug.WriteLine("Path " + appPath);
                Console.WriteLine("Path " + appPath);
                //ChromeDriverService service = ChromeDriverService.CreateDefaultService(ConfigurationManager.AppSettings["Path"]);
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(appPath);
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;

                var driver = new ChromeDriver(service, chromeOptions);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                objsearch.driver = driver;
                return (objsearch);
            }
            catch (Exception ex)
            {
                //Thread.Sleep(6000);
                //createConnection();
                Debug.WriteLine("CreateConnection() failed " + ex.ToString());
                Console.WriteLine("CreateConnection() failed " + ex.ToString());
                this.exceptionRaised = true;
                
                return null;
            }
            }

        public void closeConnection(RequestBO.SearchParam objSearch)
        {
            try
            {
                Thread.Sleep(3000);
                objSearch.driver.Quit();
            }
            catch (Exception ex)
            {
                //Thread.Sleep(6000);
                //closeConnection(objSearch);
                Debug.WriteLine("CloseConnection() failed " + ex.ToString());
                Console.WriteLine("CloseConnection() failed " + ex.ToString());
                this.exceptionRaised = true;

            }
        }

        public RequestBO.SearchParam gethtml(RequestBO.SearchParam objSearch)
        {
            String searchParam = objSearch.searchtxt;
            StringBuilder sb = new StringBuilder();
            objSearch.driver.Manage().Window.Maximize();
            try
            { 
                objSearch.driver.Navigate().GoToUrl("https://www.google.com");
                Debug.WriteLine("Went to google.com");
                Console.WriteLine("Went to google.com");
                Thread.Sleep(3000);
                Debug.WriteLine("Current URL: " + objSearch.driver.Url);
                Console.WriteLine("Current URL: " + objSearch.driver.Url);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("gethtml(google.com) failed " + ex.ToString());
                Console.WriteLine("gethtml(google.com) failed " + ex.ToString());
                objSearch.driver.Quit();
            }

            IWebElement wb = null;
            wb = FindSearchBox(objSearch);

            if (wb == null)
            {
                Debug.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                Console.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                return (objSearch);
            }

            EnterTextCharByChar(searchParam, wb);
            // wb.SendKeys(searchParam);
            wb.Click();

            wb.SendKeys(OpenQA.Selenium.Keys.Enter);
            if (objSearch.driver.Url.Contains("google.com/#spf="))
            {
                wb.Submit();
            }

            string singlewordofsearchparam = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            int indexspace = searchParam.IndexOf(' ');
            if (indexspace > 0)
            {
                 singlewordofsearchparam = searchParam.Substring(0, indexspace);
            }
            else
            {
                singlewordofsearchparam = searchParam;
            }

            if (objSearch.driver.Url.Contains("google.com/#spf=") || !objSearch.driver.Url.Contains(singlewordofsearchparam))
            {
                wb = FindSearchBox(objSearch);
                if (wb != null)
                {
                    EnterTextCharByChar(searchParam, wb);
                    wb.Submit();
                }
                else
                {
                    if (wb == null)
                    {
                        Debug.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                        Console.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                        return (objSearch);
                    }
                }
            }
            Debug.WriteLine("Selenium searched for " + searchParam);
            Console.WriteLine("Selenium searched for " + searchParam);
            Thread.Sleep(3000);
            Debug.WriteLine("Current URL: " + objSearch.driver.Url);
            Console.WriteLine("Current URL: " + objSearch.driver.Url);

            if (objSearch.driver.Url.Contains("google.com/#spf="))
            {
                Debug.WriteLine("Uh oh.. Google caught us. Waiting and trying again");
                Console.WriteLine("Uh oh.. Google caught us. Waiting and trying again");
                Random random = new Random();
                int sleeper = random.Next(1234, 4430);
                Thread.Sleep(sleeper);

                Boolean found = false; 
                
                wb = FindSearchBox(objSearch);
                if (wb != null)
                {
                    found = true;
                }
                else
                {
                    if (wb == null)
                    {
                        Debug.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                        Console.WriteLine("Uh oh.. Google caught us. Can't find the search box. ");
                        return (objSearch);
                    }
                }

                if (found)
                {
                    EnterTextCharByChar(searchParam, wb);
                    // wb.SendKeys(searchParam);
                    wb.Click();
                    wb.SendKeys(OpenQA.Selenium.Keys.Enter);
                    if (objSearch.driver.Url.Contains("google.com/#spf="))
                    {
                        wb.Submit();
                    }

                    Debug.WriteLine("Selenium searched for " + searchParam);
                    Console.WriteLine("Selenium searched for " + searchParam);
                    Thread.Sleep(3000);
                    Debug.WriteLine("Current URL: " + objSearch.driver.Url);
                    Console.WriteLine("Current URL: " + objSearch.driver.Url);
                }
                else
                {
                    Debug.WriteLine("Failed to find search box on current page...");
                    Console.WriteLine("Failed to find search box on current page...");
                }
            }
            return (objSearch);
        }

        private IWebElement FindSearchBox(RequestBO.SearchParam objSearch)
        {
            IWebElement wb = null;
            try
            {
                wb = objSearch.driver.FindElement(By.XPath("//input[@title='Search']"));
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("FindSearchBox failed 1/4");
            }

            try
            {
                Thread.Sleep(500);
                wb = objSearch.driver.FindElement(By.XPath("//input[@type='text' or @type='search' or @type='submit']"));
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("FindSearchBox failed 2/4");
                //Debug.WriteLine(ex2.ToString());
            }

            try
            {
                wb = objSearch.driver.FindElement(By.XPath("//html/body/center/form/table/tbody/tr/td[2]/div/input"));
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("FindSearchBox failed 3/4");
                //Debug.WriteLine(ex2.ToString());
            }

            try
            {
                wb = objSearch.driver.FindElement(By.XPath("//input[@title='Google Search']"));
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("FindSearchBox failed 4/4");
                //Debug.WriteLine(ex2.ToString());
            }

            Debug.WriteLine("Couldn't find a SearchBox on this page...");

            return wb;
        }

        private Boolean ClickNextButton(RequestBO.SearchParam objSearch)
        {
            Boolean found = false;
            try
            {
                objSearch.driver.FindElement(By.XPath("//a[@id='pnnext']/span[1]")).Click();
                Debug.WriteLine("getnextpage(objsearch) clicked element via span1");
                Console.WriteLine("getnextpage(objsearch) clicked element via span1");
                return true;
            }
            catch (Exception ex1)
            {
            }

            try
            {
                objSearch.driver.FindElement(By.XPath("//a[@id='pnnext']/span[2]")).Click();
                Debug.WriteLine("getnextpage(objsearch) clicked element via span2");
                Console.WriteLine("getnextpage(objsearch) clicked element via span2");
                return true;
            }
            catch (Exception ex2)
            {
            }

            try
            {
                objSearch.driver.FindElement(By.XPath("//*[@id='nav']/tbody/tr/td[3]/a")).Click();
                Debug.WriteLine("getnextpage(objsearch) clicked element via nav");
                Console.WriteLine("getnextpage(objsearch) clicked element via nav");
                return true;
            }
            catch (Exception ex2)
            {
            }
            return found;
        }

        public void EnterTextCharByChar(String text, IWebElement wb)
        {
            // Clear the text box
            //      No this doesn't....
            //wb.SendKeys(OpenQA.Selenium.Keys.Escape);

            Random ran = new Random();

            wb.Click();
            //wb.Text.Length

            int deleteMethod = ran.Next(0, 2);

            switch (deleteMethod)
            {
                case 0:
                    wb.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    wb.SendKeys(OpenQA.Selenium.Keys.Backspace);
                    break;
                case 1:
                    int extraBackspaces = ran.Next(1, 15);
                    extraBackspaces += wb.Text.Length;
                    for (int i = 0; i < extraBackspaces; i++)
                    {
                        Thread.Sleep(002);
                        wb.SendKeys(OpenQA.Selenium.Keys.Backspace);
                        i++;
                    }
                    break;
                default:
                    wb.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    wb.SendKeys(OpenQA.Selenium.Keys.Backspace);
                    break;
            }


            char[] charArr = text.ToCharArray();
            if(charArr != null)
            {
                foreach (char c in charArr)
                {
                    Thread.Sleep(ran.Next(100)); //this is a max value i think we should get rid of somehow....
                    int numbadchars = ran.Next(4);
                    for (int i = 0; i < numbadchars; i++)
                    {
                        if (ran.Next(10) > 8)
                        {
                            int num = ran.Next(0, 26); // Zero to 25
                            char let = (char)('a' + num);
                            wb.SendKeys(let.ToString());
                            Thread.Sleep(ran.Next(150)); //this is a max value i think we should get rid of somehow....
                            wb.SendKeys(OpenQA.Selenium.Keys.Backspace);
                            Thread.Sleep(ran.Next(150)); //this is a max value i think we should get rid of somehow....
                        }
                    }
                    wb.SendKeys(c.ToString());
                }
            }

        }

        public RequestBO.SearchParam getlinks(RequestBO.SearchParam objSearch)
        {
            int count = 0;
            for (count = 0; count < objSearch.elements.Count; count++)
            {
                if (objSearch.elements[count].GetAttribute("href").ToString().Contains(objSearch.website))
                { //elements[count].SendKeys(testc.Keys.Control);
                    try
                    {
                        objSearch.elements[count].Click();
                        Debug.WriteLine("GetLinks() clicked on Google search result");
                        Console.WriteLine("GetLinks() clicked on Google search result");
                        objSearch.url = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().StartsWith("OpenQA.Selenium.ElementNotVisibleException"))
                        {
                            Debug.WriteLine("Exception getlinks(), clicking via JSE and quitting driver: OpenQA.Selenium.ElementNotVisibleException: element not visible");
                            Console.WriteLine("Exception getlinks(), clicking via JSE and quitting driver: OpenQA.Selenium.ElementNotVisibleException: element not visible");
                        }
                        else
                        {
                            Debug.WriteLine("Exception getlinks(), couldn't click with Selenium - using Javascript instead");
                            Console.WriteLine("Exception getlinks(), couldn't click with Selenium - using Javascript instead");
                        }
                        testc.IJavaScriptExecutor jse = (testc.IJavaScriptExecutor)objSearch.driver;
                        IWebElement ele = objSearch.elements[count];

                        if (ele != null)
                        {
                            jse.ExecuteScript("arguments[0].click();", ele);
                            Debug.WriteLine("JS clicked  " + ele.ToString());
                            Console.WriteLine("JS clicked  " + ele.ToString());
                            objSearch.url = true;
                            break;
                        }

                        objSearch.driver.Quit();
                    }
                }

            }
            if (objSearch.url == false)
            {
                //objSearch.page = objSearch.page + 10; 
                //csv - ranking done by page # not result # for now - TBC
                objSearch.page = objSearch.page + 1;

            }

            Debug.WriteLine("GetLinks() Complete, currentl URL: " + objSearch.driver.Url);
            Console.WriteLine("GetLinks() Complete, currentl URL: " + objSearch.driver.Url);

            return (objSearch);

        }

        public RequestBO.SearchParam getnextpage(RequestBO.SearchParam objSearch)
        {
            Boolean found = false;
            //String searchParam = objSearch.searchtxt;
            //StringBuilder sb = new StringBuilder();
            found = ClickNextButton(objSearch);

            if (!found)
            {
                Debug.WriteLine("Failed to find next element on getnextpage() ");
                Console.WriteLine("Failed to find next element on getnextpage() ");
                if (!objSearch.driver.Url.Contains("www.google"))
                {
                    Debug.WriteLine("Current URL is not a Google result page; we shouldn't be here!");
                    Console.WriteLine("Current URL is not a Google result page; we shouldn't be here!");
                    Debug.WriteLine("URL: " + objSearch.driver.Url);
                    Console.WriteLine("URL: " + objSearch.driver.Url);
                }
            }

            return (objSearch);
        }

        public RequestBO.SearchParam getelement(RequestBO.SearchParam objSearch)
        {
            //IList<IWebElement> elementsCount = objSearch.driver.FindElements(By.CssSelector("a[href*='*']"));
            if (objSearch.website.EndsWith("/"))
            {
                objSearch.website = objSearch.website.Substring(0, objSearch.website.Length - 1);
            }
            objSearch.website = objSearch.website.ToLower();
            IList<IWebElement> elements = objSearch.driver.FindElements(By.CssSelector("a[href*='" + objSearch.website + "']"));

            //IList<IWebElement> all_elements = objSearch.driver.FindElements(By.CssSelector("a[href*='*']"));
            //IList<IWebElement> out_elements = new List<IWebElement>();
            //foreach (IWebElement e in all_elements) {
            //    if (e.ToString().Contains(objSearch.website)){
            //        out_elements.Add(e);
            //    }
            //}

            objSearch.elements = elements;
            //objSearch.elements = out_elements;

            Debug.WriteLine("Selenium correct result links on current page: " + ((elements == null) ? "None or NULL elements field" : elements.Count.ToString()));
            Console.WriteLine("Selenium correct result links on current page: " + ((elements == null) ? "None or NULL elements field" : elements.Count.ToString()));

            return (objSearch);
        }

        public RequestBO.SearchParam getRank(RequestBO.SearchParam objSearch)
        {
            if (objSearch.website.EndsWith("/"))
            {
                objSearch.website = objSearch.website.Substring(0, objSearch.website.Length - 1);
            }
            objSearch.website = objSearch.website.ToLower();

            IList<IWebElement> elements = objSearch.driver.FindElements(By.XPath("//div[@id='search']//cite"));
            int rank = 0;
            for ( int count = 0; count < elements.Count; count++ ) 
            {
                if(elements[count].Text.Contains(objSearch.website))
                {
                    rank = count + 1;
                    break;
                }
            }

            objSearch.rank = rank;
            
            return (objSearch);
        }


        public RequestBO.SearchParam RandomLinks(RequestBO.SearchParam objSearch)
        {
            Debug.WriteLine("RandomLinks() " + objSearch.innerlink); 
            ICollection<IWebElement> links = objSearch.driver.FindElements(By.TagName("a"));
            IWebElement finalLink = null;
            try
            {
                foreach (IWebElement link in links)
                {
                    if (link.Text.ToLower().Contains(objSearch.innerlink.ToLower()))
                    {
                        //Thread.Sleep(2000);
                        //no point in timer here, just slowing things wayyy down.
                        finalLink = link;
                        link.Click();
                        break;
                    }

                }
                objSearch.driver.Navigate().Back();
                return (objSearch);
            }

            catch(Exception ex)
            {
                Debug.WriteLine("RandomLinks() exception, attempting click via Javascript");
                Console.WriteLine("RandomLinks() exception, attempting click via Javascript");
                testc.IJavaScriptExecutor jse = (testc.IJavaScriptExecutor)objSearch.driver;

                if (finalLink != null)
                {
                    jse.ExecuteScript("arguments[0].click();", finalLink);
                    Debug.WriteLine("JS clicked  " + finalLink.ToString());
                    Console.WriteLine("JS clicked  " + finalLink.ToString());
                    objSearch.driver.Navigate().Back();
                }

                return (objSearch);
            }

        }
    }
}
