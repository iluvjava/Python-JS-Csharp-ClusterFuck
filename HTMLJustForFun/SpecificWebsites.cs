using AngleSharp.Dom;
using LittleRestClient;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Webpages;

namespace SpecificWebpages
{
    class SpecificWebsites
    {
    }

    public class DA
    {
        string daurl;
        IDocument doc;
        Webpage dapage;

        protected DA()
        {

        }

        /// <summary>
        /// ----------------Method under testing--------------------------------
        /// An internal method that searches for the download link for the image on the page. 
        ///     - If the download btn is on the page
        ///         - the link for the dl btn is returned 
        ///     - the src link for the main image on the page is returned. 
        /// </summary>
        /// <returns>
        /// A string that contains the resource of the image. 
        /// Null will be returned if it's not there: 
        /// <remarks>
        /// It heppens if mobile client content is loaded. 
        /// </remarks>
        /// </returns>
        public string GetDownloadLink()
        {
            var collection = this.doc.QuerySelectorAll("a.dev-page-download");
            if (collection.Length == 0)
            {
                collection = this.doc.QuerySelectorAll(".dev-content-normal");
                if (collection.Length == 0) return null; 
                return collection[0].GetAttribute("src");
            }
            var element = collection[0];
            return element.GetAttribute("href");
        }

        /// <summary>
        /// This method attemps to save images from the DA website. 
        /// </summary>
        /// <returns>
        /// True or false to indicate the status. 
        /// </returns>
        public Task<bool> SaveImageAsync(string path)
        {
            var t = Task.Run
            (
                () =>
                {
                    return this.SaveImage(path);
                }

            );
            return t;
        }

        protected bool SaveImage(string path)
        {
            string dllink = this.GetDownloadLink();
            if (dllink == null) return false; //failed case. 
            Webpage wp = new Webpage(dllink);
            wp.SaveAsFile(path);
            return true; 
        }


        /// <summary>
        /// Create an instance of the DA object
        /// </summary>
        /// <remarks>
        /// This function is pivotal. 
        /// </remarks>
        /// 
        /// <param name="url"></param>
        /// <returns>
        /// An instance of DA opened with the given URL. 
        /// </returns>
        /// <excpetion>
        /// An incorrect URL exeception is thrown if the input url doesn't match the 
        /// regex: ^https?://www.deviantart.com.*$
        /// Other excpetion might be thrown from Webpage class. 
        /// </excpetion>
        public static DA GetInstance(string url)
        {
            Regex rx = new Regex("^https?://www.deviantart.com/.*$");
            if (!rx.IsMatch(url))
            {
                throw new IncorrectURL();
            }
            DA d = new DA();
            d.daurl = url;
            RequestCustomizer rc = delegate (IRestRequest request)
            {
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Host", "www.deviantart.com");
                request.AddHeader("Postman-Token",
                  "444dc8e3-1a2c-4802-8df7-234017033b7a,ede1a149-0f07-4750-85e2-f03030318567");
                request.AddHeader("Cache-Control", "no-cache");
                return request;
            };
            Webpage.Client.swappable_customizer = rc;
            Webpage newdapage = new Webpage(url);
            IDocument doc = AngleSharpBridge.Get(newdapage.raw_content_string);
            d.dapage = newdapage;
            d.doc = doc;
            return d;
        }




    }
}
