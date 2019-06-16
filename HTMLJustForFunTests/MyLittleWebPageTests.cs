using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebRequest.Tests
{
    [TestClass()]
    public class MyLittleWebPageTests
    {

        string url1 = "https://www.deviantart.com/heddopen/art/Princess-test-II-779031701";
        string url2 = "https://www.deviantart.com/";
        string nl = Environment.NewLine;
        [TestMethod()]
        public void MyLittleWebPageTest()
        {
           
        }

        [TestMethod()]
        public void LoadPageTest()
        {
            string url = "https://www.deviantart.com/";
            //MyLittleWebPage mlw = new MyLittleWebPage(url);
            //mlw.LoadPage();
            //Console.WriteLine(mlw.ToString());

            url = "https://www.deviantart.com/heddopen/art/Princess-test-II-779031701";
            DeviantArtPage da = DeviantArtPage.GetInstance(url);
            da.LoadPage();
            Console.WriteLine("----------------------------");
            Console.WriteLine(da.ToString());
            Console.WriteLine(da.GetDownloadLink());
            string dl = da.GetDownloadLink();
            Console.WriteLine("dl link:"+dl);
            MyLittleWebPage mlw = da.Transfer(dl);
            mlw.LoadPage();

            Console.WriteLine(mlw.ToString());
        }


        [TestMethod()]
        public void ToStringTest()
        {
            
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
                print("Headers: "+ this.nl + r.Headers.ToString());
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
        /// Try to redirect to the download button. 
        /// </summary>
        [TestMethod()]
        public void RedirectTest()
        {
            DeviantArtPage da = DeviantArtPage.GetInstance(url1);
            da.LoadPage();
            string dllink = da.GetDownloadLink();
            MyLittleRequest mlr = new MyLittleRequest(dllink);
            mlr.cookie_jar = da.cookie_pot.GetCookies(new Uri(da.base_url));
            string result = mlr.MakeGetRequestAsync().Result.Content.ReadAsStringAsync().Result;
            print(result);
        }


        public static void print(object o)
        {
            Console.WriteLine(o.ToString());
        }
    }
}