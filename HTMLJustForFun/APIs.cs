using Json;
using LittleRestClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificWebpages
{
    public class APIs
    {
        /// <summary>
        /// Give a string, the method change json into a dictionary, 
        /// mapping from string to object, could be list, json, or string 
        /// </summary>
        /// <remarks>
        /// object might be the following, but not limitd to
        ///     Newtonsoft.Json.Linq.JObject => Acts like a Idict
        ///     string. 
        /// </remarks>
        /// <param name="jsonstr">
        /// A string representation of the json object to parse. 
        /// </param>
        /// <returns>
        /// An Idict. 
        /// </returns>
        public static IDictionary<string, object> JsonDecode(string jsonstr)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonstr);
        }

        /// <summary>
        /// This function will parse the string representing a json to an 
        /// instance of the JObject. 
        /// </summary>
        /// <param name="arg">
        /// A string representation of the JObject. 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public static JObject JsonToJObject(string arg)
        {
            return JsonConvert.DeserializeObject<JObject>(arg);
        }
    }


    /// <summary>
    /// A class contains all the method for the Derpibooru API. 
    /// </summary>
    public class DB
    {
        /// <summary>
        /// The MLC instance can be customized
        /// </summary>
        public static MyLittleRestClient MLC = new MyLittleRestClient();
        /// <summary>
        /// This is the endpoint to today's images to derpibooru. 
        /// </summary>
        public static string TodayImages = "https://derpibooru.org/images.json";
        /// <summary>
        /// Gets today's images from derpibooru, the end point targeted is: 
        /// https://derpibooru.org/images.json
        /// </summary>
        /// <returns>
        /// The object representing the Json. 
        /// </returns>
        public static JObject GetTodayImages()
        {
            string response = MLC.MakeGetRequest(TodayImages).Content;
            return APIs.JsonToJObject(response);
        }
        public static Task<JObject> GetTodayImagesAsync()
        {
            var t = Task.Run
                (
                    () =>
                    {
                        return DB.GetTodayImages();
                    }

                );
            return t;
        }
        /// <summary>
        /// Get the main page images converted to JSON, given a page offset 
        /// </summary>
        /// <param name="pageoffset">
        /// <param name="">
        /// <returns>
        /// The JSON object from the API. 
        /// </returns>
        public static JObject GetMainpageImages(int pageoffset)
        {
            string response = 
                DB.MLC.MakeGetRequest(DB.TodayImages,new Dictionary<string, string>()
                {{ "page", pageoffset.ToString()}}).Content;
            return APIs.JsonToJObject(response);
        }

    }


    
}
