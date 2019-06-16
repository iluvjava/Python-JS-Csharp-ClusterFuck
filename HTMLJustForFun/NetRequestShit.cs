using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsQuery;
using System.Net.Http.Headers;

namespace WebRequest
{
    
    /// <summary>
    /// Creates a class for making api calls.
    /// </summary>
    public class MyLittleRequest
    {
        // URL upon first request. 
        public string base_uri;
        public CookieCollection cookie_jar;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">
        /// The extra parameters that should be appeneded 
        /// at the end of the url.
        /// </param>
        public MyLittleRequest(string uri)
        {
            this.base_uri = uri;
        }

        /// <summary>
        /// Give params appeneded to url, it will make a get request. 
        /// </summary>
        /// <remarks>
        ///     Method Tested Casually
        /// </remarks>
        /// <param name="Url_Params">
        /// String, representing the part at the end of the URL. 
        /// </param>
        /// <returns>
        /// A Http ResponseMessage for you to process. 
        /// </returns>
        public async Task<HttpResponseMessage> MakeGetRequestAsync
        (
            IDictionary<string, string> Uri_Params =null
        )
        {
            var p = Uri_Params == null? "" :new FormUrlEncodedContent(Uri_Params).ToString();
            Uri uri = new Uri(this.base_uri + p);
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            CookieContainer c = new CookieContainer();

            //prepare cookie
            if (this.cookie_jar != null)
            {
                // foreach (KeyValuePair<string, string> kvp in this.cookie_jar)
                // {
                //     c.Add(uri, new Cookie(kvp.Key, kvp.Value));
                // }
                handler.CookieContainer.Add(this.cookie_jar);
            }
            //prepare http client
            using (HttpClient client = new HttpClient(handler))
            {
                //client headers
                //----------------------------------------------------
                PrepareHeader(client.DefaultRequestHeaders);
                HttpResponseMessage response = await client.GetAsync(uri);
                return response;
            }
        }


        /// <summary>
        /// An internal method, used for preparing http headers for get and post request. 
        /// </summary>
        protected void PrepareHeader(HttpRequestHeaders header)
        {
            header.Add(
                "user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Safari/537.36"
                );
            header.Add(
                "accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"
                );
        }
        /// <summary>
        /// A method that makes a mpost request to the base url in the class. 
        /// </summary>
        /// <remarks>
        /// Method Casually Tested. 
        /// </remarks>
        /// <param name="formdata">
        /// A Dictionary representing the formdata you want to upload for the requst. 
        /// </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> MakePostRequestAsync
        (IDictionary<string, string> formdata)
        {
            if (formdata == null)
            {
                formdata = new Dictionary<string, string>();
                formdata[""] = "";
            }
            string url = this.base_uri;
            using (HttpClient client = new HttpClient())
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(formdata);
                HttpResponseMessage response = await client.PostAsync(url, content);
                return response;
            }
        }


    }

    /// <summary>
    /// Saving session and relative info for a webpage. 
    /// </summary>
    public class MyLittleWebPage
    {
        public string base_url;
        protected string sate = "not yet loaded";
        public HtmlWeb web { get; protected set; }
        public HtmlDocument doc { get; protected set; }
        //Session
        public CookieContainer cookie_pot {get; protected set;}
        protected IDictionary<string, string> customized_header;
        protected Uri refer_url;
        protected string user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Safari/537.36";

        protected HttpWebRequest request;
        protected HttpWebResponse post_reponse;

        public MyLittleWebPage(String baseurl)
        {
            base_url = baseurl;
        }



        /// <summary>
        /// Load the page.
        /// </summary>
        public void LoadPage()
        {
            try
            {
                web = new HtmlWeb();
                web.UseCookies = true;
                web.CaptureRedirect = true;
                web.PreRequest = new HtmlWeb.PreRequestHandler(PreRequestFiddling);
                web.PostResponse = new HtmlWeb.PostResponseHandler(PostResponseFiddling);
                doc = web.Load(base_url);
                sate = "loaded";
            }
            catch(System.Net.WebException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Response);
                Console.WriteLine(e.Status);
                Console.WriteLine(e.Data);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(request == null?"null":request.Headers.ToString());
            }
        }




        /// <summary>
        /// Get the info after the web page responded. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void PostResponseFiddling(HttpWebRequest request,HttpWebResponse response)
        {
            cookie_pot = request.CookieContainer;
            this.request = request;
            post_reponse = response;
        }


        /// <summary>
        /// Prepare prerequest cookies. Prepare refered url. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool PreRequestFiddling(HttpWebRequest request)
        {
            request.Headers.Clear();
            if(this.cookie_pot != null)request.CookieContainer = cookie_pot;
            if(this.refer_url != null)request.Headers.Add("referer", refer_url.AbsoluteUri);
            if(this.user_agent != null) request.UserAgent = this.user_agent;
            request.KeepAlive = false;
            request.Accept = 
                "text / html,application / xhtml + xml,application / xml; q = 0.9,image / webp,image / apng,*/*;q=0.8,application/signed-exchange;v=b3";
            request.Headers.Add("Upgrade-Insecure_requests", "1");
            this.request = request;
            
            return true;
        }


        override
        public string ToString()
        {
            if (sate != "not yet loaded")
            {
                
                var l = Environment.NewLine;
                var res = "";
                res += "Status code: " + this.post_reponse.Headers.Get("status");
                res += l;
                res += "Response Uri: " + web.ResponseUri;
                res += l;
                res += "Request Header: ---"+ l + request.Headers.ToString();
                res += l;
                res += "Response Header: ---" + l + post_reponse.Headers.ToString();
                res += l;
                res += "Cookies: " + l;
                if (cookie_pot != null)
                {
                    var collection = cookie_pot.GetCookies(web.ResponseUri);
                    foreach (Cookie c in collection)
                    {
                        res += c.Name + " = " + c.Value + "l";
                    }
                }
                //res += doc.Text;
                return res;
            }
            return "Page Link: " + base_url;
        }
    }

    /// <summary>
    /// A speicial mylittle webpage, it opens on deviant art page only. 
    /// </summary>
    public class DeviantArtPage : MyLittleWebPage
    {
        internal DeviantArtPage(string url):base(url)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapagelink">
        /// A da link
        /// </param>
        /// <returns>
        /// Null if the page is not in the da domain.
        /// </returns>
        public static DeviantArtPage GetInstance(string dapagelink)
        {
            Regex rx = new Regex(@"^https://www\.deviantart.*$");
            var mc = rx.Match(dapagelink);
            bool success = mc.Success;
            return success?new DeviantArtPage(dapagelink):null;
        }

        /// <summary>
        /// Transfer to a new page, session is preserved. 
        /// </summary>
        /// <param name="url">
        /// 
        /// </param>
        /// <returns></returns>
        public MyLittleWebPage Transfer(string url)
        {
            Regex rx = new Regex(@"^http://www.deviantart.*$");
            var mc = rx.Match(url);
            bool success = mc.Success;
            if (success)
            {
                DeviantArtPage newda = new DeviantArtPage(url);
                newda.refer_url = base.web.ResponseUri;
                newda.cookie_pot = this.cookie_pot;
                return newda;
            }
            return new MyLittleWebPage(url) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Null is returned if the download link is not there. 
        /// The dl link is absolute.
        /// </returns>
        public string GetDownloadLink()
        {
            CQ c = CQ.Create(doc.Text);
            var l = c["a.dev-page-download[href]"];
            return l.Attr("href");
        }


    }


}
