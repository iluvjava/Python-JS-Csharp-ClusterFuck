using CsQuery;
using LittleRestClient;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webpages
{
    class Webpages
    {
    }

    /// <summary>
    /// This class represents a single webpage. 
    /// </summary>
    public class Webpage
    {
        public static MyLittleRestClient Client = new MyLittleRestClient();

        /// <summary>
        /// The url of the web page that is going to be load. 
        /// </summary>
        public string base_url;

        /// <summary>
        /// The content type as from the header of the response. 
        /// </summary>
        public string content_type;
        public bool is_successful{ get; protected set; }
        public IRestResponse response; 

        /// <summary>
        /// The raw content gotten from the page in the form of a byte array. 
        /// </summary>
        byte[] raw_content;

       
       /// <summary>
       /// 
       /// </summary>
       /// <param name="baseurl">
       /// A url with the correct heading. 
       /// </param>
       /// <exception>
       /// An Incorrect URL exception is thrown if the input url is not a valid url. 
       /// </exception>
        public Webpage(string baseurl)
        {
            Regex rx = new Regex("^(https{0,1}://.*)|(localhost:.*)$");
            if (! rx.IsMatch(baseurl))
            {
                throw new IncorrectURL();
            }
            base_url = baseurl;
            GetWebPage();
        }


        /// <summary>
        /// GET, byte array, and content type will be established. 
        /// </summary>
        protected void GetWebPage()
        {
            IRestResponse res = Webpage.Client.MakeGetRequest(this.base_url);
            this.is_successful = res.IsSuccessful;
            if (!is_successful)
                return;
            this.raw_content = res.RawBytes;
            this.content_type = res.ContentType;
            this.response = res; 
        }


        override
        public string ToString()
        {
            var nl = Environment.NewLine;
            var res = "";
            res += "Base URL: " + this.base_url+ nl;
            res += "Status: " + (this.is_successful ? "Loaded" : "Failed to load")+ nl;
            if (!is_successful)
            {
                return res;
            }
            res += "Content Type: " + this.content_type + nl;
            res += "Content length: " + this.raw_content.Length;
            return res; 
        }

        /// <summary>
        /// This method will try to save the content of the website as a file on the disk. 
        /// if file name null
        ///     - If html-> tile and title and use it as the file name. 
        ///     - If content-disposition is in the header, use that. 
        ///     - else, ues the hashcode as the filename. 
        /// </summary>
        /// <param name="path">
        /// The string of the directory. 
        /// </param>
        /// <returns>
        /// Boolean to indicate the status. 
        /// </returns>
        public bool SaveAsFile(string path, string filename= null)
        {

            DirectoryInfo dirinfo = new DirectoryInfo(path);
            if (!dirinfo.Exists)return false;
            if (filename == null) filename = GetFileName();
            return false;
        }

        /// <summary>
        /// Returns the file name. 
        /// </summary>
        public string GetFileName()
        {
            var contenttype = this.content_type;
            contenttype = contenttype.Substring(contenttype.IndexOf("/")+1);
            contenttype = contenttype.Replace("-","");

            if (contenttype == "html")
            {
                string s =  System.Text.Encoding.UTF8.GetString(this.raw_content);
                CQ q = new CQ(s);
            }

            string filename = this.GetHashCode().ToString();
            foreach (Parameter param in this.response.Headers)
            {
                string k = param.Name;
                if (k.ToLower() == "content-disposition")
                {
                    string v = (string)param.Value;
                    filename = v.Substring(v.LastIndexOf("="));
                    filename = filename.Substring(0,filename.Length-1);
                }

            }
            return filename+ "."+contenttype; 
        }

        
    }
}
