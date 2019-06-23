using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
///
/// </summary>
namespace LittleRestClient
{
    /// <summary>
    /// This is a swappable parts for the MyLittleRestClient class, using this
    /// to customized your own web request when making a request.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public delegate IRestRequest RequestCustomizer(IRestRequest request);

    /// <summary>
    /// This is a class that encapusulate a client, and it automate the process of making different
    /// kinds of request to different urls.
    /// - The cookies is saved and automatically used when using this client.
    /// - Support delegate for customizable headers for request.
    /// </summary>
    public class MyLittleRestClient
    {
        /// <summary>
        /// This is shared when it's specified.
        /// </summary>
        public static CookieContainer SharedCookies = new CookieContainer();

        public static string UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 " +
            "(KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10136";

        public static string UserAgent2 = "PostmanRuntime/7.15.0";
        public RequestCustomizer swappable_customizer = null;

        public MyLittleRestClient()
        {
            r_client = new RestClient();
            r_client.FollowRedirects = true;
            r_client.CookieContainer = MyLittleRestClient.SharedCookies;
            r_client.UserAgent = MyLittleRestClient.UserAgent2;
        }

        public RestClient r_client { get; protected set; }

        /// <summary>
        /// Make a get request for the given url
        /// </summary>
        /// <param name="url">
        /// Abosolute URL please.
        /// <param name ="parameters">
        /// A dictionary from string to string to represents the data parameters for
        /// the get request.
        /// </param>
        /// </param>
        /// <returns>
        ///
        /// </returns>
        public IRestResponse MakeGetRequest
            (string url, IDictionary<string, string> parameters = null)
        {
            var request = PrepareRequest(url + EncodeGetURLParameters(parameters));
            VerifyUrl(url);
            request.Method = Method.GET;
            var res = r_client.Get(request);
            return res;
        }

        public async Task<IRestResponse> MakeGetRequestAsync
                    (string url, IDictionary<string, string> parameters = null)
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
                Console.WriteLine(kvp.Key + " = " + kvp.Value);
                request.AddParameter(new Parameter(kvp.Key, kvp.Value, ParameterType.GetOrPost));
                Console.WriteLine(request.Parameters.Count);
            }
            return r_client.Post(request);
        }

        /// <summary>
        /// Make an async post request given the parameters for formddata.
        /// </summary>
        /// <returns>
        /// Async task object with a result: IRestResponse.
        /// </returns>
        public async Task<IRestResponse> MakPostRequestAsync
        (string url, IDictionary<string, string> parameters)
        {
            var t = await Task<IRestResponse>.Run
            (
                () =>
                {
                    return this.MakePostRequest(url, parameters);
                }

            );
            return t;
        }

        /// <summary>
        /// This method encode the key and value for the url.
        /// </summary>
        /// <param name="arg">
        /// A map that repesents the parameters, if it's null, then the function
        /// will just return "";
        /// </param>
        /// <returns></returns>
        protected static string EncodeGetURLParameters(IDictionary<string, string> arg)
        {
            if (arg == null || arg.Count == 0) return "";
            var res = new StringBuilder("?");
            foreach (var kvp in arg)
            {
                res.Append(Uri.EscapeDataString(kvp.Key));
                res.Append("=");
                res.Append(Uri.EscapeDataString(kvp.Value));
                res.Append("&");
            }
            string result = res.ToString();
            return result.Substring(0, res.Length - 1);
        }

        /// <summary>
        /// Internal Method only.
        /// </summary>
        protected void PrepareHeaders(IRestRequest request)
        {
            request.AddHeader("Accept", "*/*"
                );
            //request.AddHeader("accept-encoding",
            //    "gzip, deflate, br"
            //    );
            //request.AddHeader(
            //    "Cache-Control", "no-cache"
            //    );
        }

        /// <summary>
        /// Prepare the headers for the IRestRequest that are going to be sent.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected IRestRequest PrepareRequest(string url)
        {
            var request = new RestRequest(url);
            if (this.swappable_customizer == null)
            { PrepareHeaders(request); }
            else
            { return this.swappable_customizer(request); }
            return request;
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
    }

    [Serializable]
    internal class IncorrectURL : Exception
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