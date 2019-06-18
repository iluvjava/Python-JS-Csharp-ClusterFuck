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


    public delegate IRestRequest RequestCustomizer(IRestRequest request);

    class RestSharpAttempt
    {
    }

    /// <summary>
    /// This is a class that encapusulate a client, and it automate the process of making different 
    /// kinds of request to different urls. 
    /// - The cookies is saved and automatically used when using this client. 
    /// - 
    /// </summary>
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
        /// Abosolute URL please. 
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
            var res = r_client.Execute(request);
            return res;
        }

        public async Task<IRestResponse>  MakeGetRequestAsync(string url, string parameters="")
        {
            var t = await Task<IRestResponse>.Run
                (
                    () => 
                    {
                        return this.MakeGetRequest(url, parameters);
                    }
                );
           
            return t; 
        }

        /// <summary>
        /// This method makes a posts request with given formdata represented by the 
        /// a string to string dictionary. 
        /// </summary>
        /// <param name="url">
        /// A valide absolute url. 
        /// </param>
        /// <param name="parameters">
        /// A string => string dic, it has the formdata for the post request. 
        /// </param>
        /// <returns>
        /// IRestResponse responsed from the server. 
        /// </returns>
        public IRestResponse MakePostRequest(string url, IDictionary<string, string> parameters)
        {
            var request = PrepareRequest(url);
            
            request.Method = Method.POST;
            foreach (var kvp in parameters)
            {
                Console.WriteLine(kvp.Key+" = "+ kvp.Value);
                request.AddParameter(new Parameter(kvp.Key, kvp.Value, ParameterType.GetOrPost));
                Console.WriteLine(request.Parameters.Count);
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

        protected IRestRequest PrepareRequest(string url)
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
