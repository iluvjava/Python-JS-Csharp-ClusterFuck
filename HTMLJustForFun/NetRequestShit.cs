using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebRequest
{
    
    /// <summary>
    /// Creates a class for making api calls.
    /// </summary>
    public class MyLittleRequest
    {
        // URL upon first request. 
        public string base_uri;
        public IDictionary<string, string> cookie_jar;

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
            string Uri_Params = ""
        )
        {
            Uri uri = new Uri(this.base_uri + Uri_Params);
            HttpClientHandler handler = new HttpClientHandler();
            CookieContainer c = new CookieContainer();
            //prepare cookie
            if(this.cookie_jar!=null)
            foreach (KeyValuePair<string, string> kvp in this.cookie_jar)
            {
                c.Add(uri, new Cookie(kvp.Key, kvp.Value));
            }
            handler.CookieContainer = c;
            //prepare http client
            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return response;
            }
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
        string base_url;
        public HtmlWeb web;
        public HtmlDocument doc;
        //Saving cookies for the broswer session.
        CookieContainer cookie_pot = new CookieContainer();

        HttpWebRequest post_request;
        HttpWebResponse post_reponse;

        public MyLittleWebPage(String baseurl)
        {
            base_url = baseurl;
        }


        /// <summary>
        /// Load the page.
        /// </summary>
        public void LoadPage()
        {
            web = new HtmlWeb();
            web.UseCookies = true;
            web.PostResponse = new HtmlWeb.PostResponseHandler(PostResponseFiddling);
            doc = web.Load(base_url);
        }


        /// <summary>
        /// Get the info after the web page responded. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void PostResponseFiddling(HttpWebRequest request,HttpWebResponse response)
        {
            cookie_pot = request.CookieContainer;
            post_request = request;
            post_reponse = response;
        }
        
        override
        public string ToString()
        {
            var collection = cookie_pot.GetCookies(web.ResponseUri);
            var l = Environment.NewLine;
            var res = "";
            res += "Status code: " + web.StatusCode;
            res += l;
            res += "Response Uri: "+web.ResponseUri;
            res += l;
            res += post_reponse.Headers.ToString();
            res += l;
            res += "Cookies: "+l;
            foreach (Cookie c in collection)
            {
                res += c.Name + " = " + c.Value + "l";
            }
            res += doc.Text;
            return res;
        }
    }

    /// <summary>
    /// A speicial mylittle webpage, it opens on deviant art page only. 
    /// </summary>
    public class DeviantArtPage :MyLittleWebPage
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

            return null;
        }
    }


}
