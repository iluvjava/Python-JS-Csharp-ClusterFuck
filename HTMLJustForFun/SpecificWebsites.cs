using AngleSharp.Dom;
using LittleRestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Webpages;

namespace HTMLJustForFun
{
    class SpecificWebsites
    {
    }

    public class DA
    {
        string daurl;
        IDocument thepage;
        Webpage dapage;

        protected DA()
        {

        }

        public static DA GetInstance(string url)
        {
            Regex rx = new Regex("^https?://www.deviantart.com.*$");
            if (rx.IsMatch(url))
            {
                throw new IncorrectURL();
            }

            DA d = new DA();
            Webpage newdapage = new Webpage(url);
            IDocument doc = AngleSharpBridge.Get(newdapage);

        }

    }
}
