using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsQuery;

namespace HTMLJustForFun
{
    class RestSharpAttempt
    {
    }

    public class MyLittleRestClient
    {
        public RestClient r_client;
        
        public MyLittleRestClient()
        {
            r_client = new RestClient();
            r_client.FollowRedirects = true;
            r_client.CookieContainer = new System.Net.CookieContainer();
        }

        /// <summary>
        /// Make a get request for the given url 
        /// </summary>
        /// <param name="url">
        /// Encoded get request url. 
        /// <param name ="parameters">
        /// The parameters with the get reqeust, 
        /// </param>
        /// </param>
        /// <returns></returns>
        public IRestResponse MakeGetRequest(string url, string parameters = "")
        {
            parameters = Uri.EscapeDataString(parameters);
            var request = PrepareRequest(url+parameters);
            VerifyUrl(url);
            request.Method = Method.GET;
            var res = r_client.Get(request);
            return res;
        }

        public IRestResponse MakePostRequest(string url, IDictionary<string, string> parameters)
        {
            var request = PrepareRequest(url);
            request.Method = Method.POST;
            foreach (var kvp in parameters)
            {
                request.AddParameter(kvp.Key,kvp.Value);
            }
            return r_client.Post(request);
        }

        protected void VerifyUrl(string baseurl)
        {
            Regex rx = new Regex(@"(^https?://.*$)|(^localhost.*$)");
            Match m = rx.Match(baseurl);
            if (!m.Success)
            {
                throw new IncorrectURL();
            }
        }

        protected  IRestRequest PrepareRequest(string url)
        {
            var request = new RestRequest(url);
            PrepareHeaders(request);
            return request;
        }

        /// <summary>
        /// Internal Method only. 
        /// </summary>
        protected void PrepareHeaders(IRestRequest request)
        {
            request.AddHeader("User-Agent",
                " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) "
                +
                "Chrome/75.0.3770.90 Safari/537.36"
                +
                "accept: text / html, application / xhtml + xml, application / xml; q = 0.9, image"+
                " / webp, image / " +
                "apng, */*; q=0.8, application/signed-exchange; v=b3"              
                );
            request.AddHeader("Accept",
                "text/html, application/xhtml+xml, application/xml; " +
                "q=0.9, image/webp, image/apng, */*; " +
                "q=0.8, application/signed-exchange; v=b3"
                );
            request.AddHeader("accept-encoding",
                "gzip, deflate, br"
                );
        }
    }
    [Serializable]
    class IncorrectURL : Exception
    {
        public IncorrectURL()
        {
        }

        public IncorrectURL(string message) : base(message)
        {
        }

        public IncorrectURL(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectURL(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
