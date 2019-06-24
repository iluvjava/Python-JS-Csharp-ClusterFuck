using CsQuery;
using LittleRestClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpecificWebpages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Webpages;

namespace WebRequest.Tests
{
    [TestClass()]
    public class MyLittleWebPageTests
    {
        private string desktop = @"C:\Users\Administrator\Desktop";
        private string nl = Environment.NewLine;
        private string pokedex =
            "https://courses.cs.washington.edu/courses/cse154/webservices/pokedex/game.php";

        private string posturl = "https://postman-echo.com/post";
        private string url1 = 
            "https://www.deviantart.com/heddopen/art/Lil-Happi-Dashie-Colour-750967238";
        private string url2 = "https://www.deviantart.com/";
        private string url3 = 
            "https://www.deviantart.com/rainbow-highway/art/Phencyclidine-8k-736954613";
        private string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Creates a random hex string with given length.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string GetRandomHexNumber(int digits, Random random)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public static void print(object o = null)
        {
            Console.WriteLine(o == null ? "" : o.ToString());
        }

        public static void printEnumerator(IEnumerator e)
        {
            while (e.MoveNext())
            {
                print(e.Current);
            }
        }

        /// <summary>
        /// Function tries to use the API call method for fetching the website' content.
        /// </summary>
        [TestMethod()]
        public void APICallsTest()
        {
            {
                print("Makinga get requst to the url: " + url1);
                var mlr = new WebRequest.MyLittleRequest(url1);
                HttpResponseMessage r = mlr.MakeGetRequestAsync().Result;
                print("Headers: " + this.nl + r.Headers.ToString());
                //print(r.Content.ReadAsStringAsync().Result);
            }
            {
                print("Making get request: " + this.url2);
                var mlr = new WebRequest.MyLittleRequest(url2);
                HttpResponseMessage r = mlr.MakeGetRequestAsync().Result;
                print("Request Header: " + nl + r.RequestMessage.Headers.ToString());
                print("Reponse Header: \n" + nl + r.Headers.ToString());
            }
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod()]
        public void DAClassTest()
        {
            int i = 20;
            while (i-- != 0)
            {
                DAArtistwork instance = DAArtistwork.GetInstance(url1);
                print(instance.GetDownloadLink());
                print("------------------------------Raw Content---------------------------------");
                print(instance.dapage.raw_content_string);
                Assert.IsTrue(
                    instance.GetDownloadLink() != null);
                print(instance.SaveImageAsync(user).Result);
            }
        }

        [TestMethod()]
        public void DeviantArtClassTest()
        {
            goto test2;
            {
                print("Trying to make connection to: " + url1);
                DeviantArtPage dapage = DeviantArtPage.GetInstance(url1);
                dapage.LoadPage();
                print(dapage);
                var dllink = dapage.GetDownloadLink();
                print($"This is DL link: {dllink}");
                print("Trying to download: ");
                var newpage = dapage.TransferCookies(dllink);
                newpage.LoadPage();
                var response = newpage.content_raw_string;
                print(newpage);
                print("Base Address" + nl + newpage.mlr_thispage.client_handler);
                print("Raw String representing png: " + response.Substring(0, 2000));
            }

        test2:
            {
                print("Trying to make connection to: " + url1);
                DeviantArtPage dapage = DeviantArtPage.GetInstance(url1);
                dapage.LoadPage();
                print(dapage);
                var dllink = dapage.GetDownloadLink();
                print($"This is DL link: {dllink}");
                print("Trying to download: ");
                var newpage = dapage.Download_Redirect();
                newpage.LoadPage();
                var response = newpage.content_raw_string;
                print(newpage);
                print("Looking for content-disposition header: "
                    + newpage.GetValFromResponseHeader("content-disposition"));
                print("Raw String representing png: " + response.Substring(0, 2000));
            }
        }

        /// <summary>
        /// It also tests the Derpibooru APIs.
        /// </summary>
        [TestMethod()]
        public void JsonSerializeTest()
        {
            // string stuff = "{\"key\":{\"key2\":\"val\"}}"; IDictionary<string, object> dict =
            // APIs.JsonDecode(stuff); print(dict["key"].GetType()); print(DB.GetTodayImages());
            JObject j = DB.GetMainpageImages(1);
            print();
            print("-----------------------------------------------");
            print("These are all the keys associated with each image json object: ");
            IEnumerable<JToken> e = j["images"][0].AsEnumerable();
            foreach (JToken token in e)
            {
                print(token.ToString());
            }
            print("Trying to convert each of the image object in the json response.");
            IDBImage image = j["images"][0].ToObject<DBImage>();
            print(image);

        }

        [TestMethod()]
        public void LoadPageTest()
        {
            // string url = "https://www.deviantart.com/";
            // //MyLittleWebPage mlw = new MyLittleWebPage(url);
            // //mlw.LoadPage();
            // //Console.WriteLine(mlw.ToString());
            //
            // url = "https://www.deviantart.com/heddopen/art/Princess-test-II-779031701";
            // DeviantArtPage da = DeviantArtPage.GetInstance(url);
            // da.LoadPage();
            // Console.WriteLine("----------------------------");
            // Console.WriteLine(da.ToString());
            // Console.WriteLine(da.GetDownloadLink());
            // string dl = da.GetDownloadLink();
            // Console.WriteLine("dl link:"+dl);
            // MyLittleWebPage mlw = da.Transfer(dl);
            // mlw.LoadPage();
            //
            // Console.WriteLine(mlw.ToString());
        }

        ///<summary>
        ///
        ///</summary>
        [TestMethod()]
        public void MiscTest()
        {
            {
                int i = 20;
                while (i-- != 0)
                {
                    Random r = new Random();
                    Webpages.Webpage wp = new Webpages.Webpage(url3);
                    RequestCustomizer rc = delegate (IRestRequest request)
                    {
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Connection", "keep-alive");
                        request.AddHeader("accept-encoding", "gzip, deflate");
                        request.AddHeader("Accept", "*/*");
                        //request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)
                        //AppleWebKit /537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36
                        //Edge /14.14393");
                        request.AddHeader("Host", "www.deviantart.com");
                        request.AddHeader("Postman-Token",
                        "444dc8e3-1a2c-4802-8df7-234017033b7a,ede1a149-0f07-4750-85e2-f03030318567");
                        request.AddHeader("Cache-Control", "no-cache");
                        return request;
                    };
                    Webpage.Client.swappable_customizer = rc;
                    print(wp);
                    print(Webpage.Client);
                    print(wp.raw_content_string);
                    Assert.IsTrue(wp.raw_content_string.
                        Contains("<!--[if IE 9]><html class=\"ie eq9 lt10 \"><![endif]-->"));
                    //Thread.Sleep(30);
                }
            }
        }

        [TestMethod()]
        public void MyLittleWebPageTest()
        {
        }
        /// <summary>
        /// Try to redirect to the download button.
        /// </summary>
        [TestMethod()]
        public void RedirectTest()
        {
            DeviantArtPage da = DeviantArtPage.GetInstance(url1);
            da.LoadPage();
            string dllink = da.GetDownloadLink();
            print("Download Link: " + nl);
            print(dllink);

            MyLittleRequest mlr = new MyLittleRequest(dllink);
            mlr.cookie_jar = da.cookie_pot.GetCookies(new Uri(da.base_url));

            string result = mlr.MakeGetRequestAsync().Result.Content.ReadAsStringAsync().Result;
            print(result);
        }

        [TestMethod()]
        public void ToStringTest()
        {
        }
        //[TestMethod()]
        //public void InvestigateHttpClient()
        //{
        //    var client_handler = new HttpClientHandler();
        //    client_handler.AllowAutoRedirect = true;
        //    client_handler.UseCookies = true;
        //    var properties = client_handler.Properties;
        //    print("Investigaring if httpclient saves the cookies " +
        //        "accroding to reponse header from the server. ");
        //    MyLittleRequest mlr = new MyLittleRequest(url1);
        //    var res = mlr.MakeGetRequestAsync().Result;
        //    print("Cookie container count: "+ mlr.client_handler.CookieContainer.Count);
        //
        //}

        /// <summary>
        /// Testing get requests with cookies transfer using the
        /// MylittleRestClient.
        /// </summary>
        [TestMethod()]
        public void UsingRestSharp()
        {
            {
                MyLittleRestClient mlrc = new MyLittleRestClient();
                var res = mlrc.MakeGetRequestAsync(url1).Result;
                print("cookie len: " + res.Cookies.Count);
                IEnumerator e = res.Headers.GetEnumerator();
                printEnumerator(e);
                string content = res.Content;
                CQ c = new CQ(content);
                string dllink = c["a.dev-page-download[href]"].Attr("href");
                res = mlrc.MakeGetRequest(dllink);
                print();
                printEnumerator(res.Headers.GetEnumerator());
                print(res.Content);
            }
        }

        /// <summary>
        /// Testing postrequest using the MyLittleRestClient class.
        /// </summary>
        [TestMethod()]
        public void UsingRestSharp2()
        {
            IDictionary<string, string> formdata = new Dictionary<string, string>()
            {
                { "startgame", "true" },
                { "mypokemon" ,"detective-pikachu"}
            };
            var mlrc = new MyLittleRestClient();
            var response = mlrc.MakePostRequest(pokedex, formdata);
            print(response.Content);
        }

        [TestMethod()]
        public void Webpage2Test()
        {
            var page = new WebPage2(url1);
            print("Trying to css select the download link: ");
            page.LoadPage();
            string dllink = page.doc_css["a.dev-page-download[href]"].Attr("href");
            print(dllink);
            print("Trying to transfer to the above page: ");
            var newpage = page.Transfer(dllink);
            //Using postman default header
            CustomizedHeaders fun = delegate (HttpRequestHeaders arg)
            {
                print("The delegated defined in the test is called. ");
                arg.Add("Cache-Control", "no-cache");
                arg.Add("accept-encoding", "gzip,deflate");
                arg.Add("connection", "keepalive");
            };
            newpage.mlr_thispage.header_customizer = fun;

            newpage.LoadPage();
            print(newpage);
            print(newpage.response_headers);
            print(newpage.content_raw_string);
        }
        [TestMethod()]
        public void WebpageTest3()
        {
            Webpages.Webpage wp = new Webpages.Webpage(url1);
            print(wp.GetFileName());
            wp.SaveAsFile(this.desktop);
        }
    }
}