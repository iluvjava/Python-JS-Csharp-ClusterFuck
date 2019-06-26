using AngleSharp.Dom;
using LittleRestClient;
using RestSharp;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Webpages;

/// <summary>
/// </summary>

namespace APIs
{
    /// <summary>
    /// This class represents a specific domain.
    /// - There is a possibility that we get the new beta theme for the DA
    ///   this means we have to distinguish them.
    /// </summary>
    public class DAArtistwork
    {
        public Webpage dapage;
        private string daurl;
        private IDocument doc;

        protected DAArtistwork()
        {
        }

        /// <summary>
        /// Create an instance of the DA object
        /// </summary>
        /// <remarks>
        /// This function is pivotal.
        /// </remarks>
        /// <param name="url"></param>
        /// <returns>
        /// An instance of DA opened with the given URL.
        /// </returns>
        /// <excpetion>
        /// An incorrect URL exeception is thrown if the input url doesn't match the
        /// regex: ^https?://www.deviantart.com.*$
        /// Other excpetion might be thrown from Webpage class.
        /// </excpetion>
        public static DAArtistwork GetInstance(string url)
        {
            Regex rx = new Regex("^https?://www.deviantart.com/.+/art/.+$");
            if (!rx.IsMatch(url))
            {
                throw new IncorrectURL();
            }
            DAArtistwork d = new DAArtistwork();
            d.daurl = url;
            RequestCustomizer rc = delegate (IRestRequest request)
            {
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Host", "www.deviantart.com");
                // Important to each specific websites.
                request.AddHeader("Postman-Token",
                  "444dc8e3-1a2c-4802-8df7-234017033b7a,ede1a149-0f07-4750-85e2-f03030318567");
                request.AddHeader("Cache-Control", "no-cache");
                //request.AddHeader
                //("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Mobile Safari/537.36");
                return request;
            };
            Webpage.Client.swappable_customizer = rc;
            Webpage newdapage = new Webpage(url);
            IDocument doc = AngleSharpBridge.Get(newdapage.raw_content_string);
            d.dapage = newdapage;
            d.doc = doc;
            return d;
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
            var element = this.doc.QuerySelector("[href *=\"download\"]");
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

    }

    internal class SpecificWebsites
    {
    }
}