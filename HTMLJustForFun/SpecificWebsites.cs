using AngleSharp.Dom;
using LittleRestClient;
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
        /// </returns>
        public string GetDownloadLink()
        {
            var collection = this.doc.QuerySelectorAll("a.dev-page-download");
            if (collection.Length == 0)
            {
                collection = this.doc.QuerySelectorAll(".dev-content-normal");
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
        public void SaveImageAsync(string path)
        {
            var t =Task.Run
            (
                () =>
                {
                    this.SaveImage(path);
                }

            );
        }

        protected void SaveImage(string path)
        {
            string dllink = this.GetDownloadLink();
            Webpage wp = new Webpage(dllink);
            wp.SaveAsFile(path);
        }


        /// <summary>
        /// Create an instance of the DA object
        /// </summary>
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
            Webpage newdapage = new Webpage(url);
            IDocument doc = AngleSharpBridge.Get(newdapage.raw_content_string);
            d.dapage = newdapage;
            d.doc = doc;
            return d;
        }




    }
}
