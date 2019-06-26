using LittleRestClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIs
{
    /// <summary>
    /// This interface defines the field of an images json object in te
    /// </summary>
    public interface IDBImage
    {
        DateTime created_at { get; set; }
        string description { get; set; }
        int downvotes { get; set; }
        string file_name { get; set; }
        DateTime first_seen_at { get; set; }
        int id { get; set; }
        string image { get; set; }
        string tags { get; set; }
        DateTime updated_at { get; set; }
        int upvotes { get; set; }
    }
    /// <summary>
    /// This class contains static method that are associated with 
    /// parsing JSON. 
    /// </summary>
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
    /// <see cref="https://derpibooru.org/pages/api"/>
    /// </summary>
    ///
    public class DB
    {
        /// <summary>
        /// The MLC instance can be customized
        /// </summary>
        public static MyLittleRestClient MLC = new MyLittleRestClient();

        /// <summary>
        /// The DB search endpoint for the API. 
        /// </summary>
        public static string SearchEndpoint = "https://derpibooru.org/search.json";

        /// <summary>
        /// This is the endpoint to today's images to derpibooru.
        /// </summary>
        public static string TodayImages = "https://derpibooru.org/images.json";
        /// <summary>
        /// Given a Jtoken, this method will convert it to an instance of the IDBImage.
        /// </summary>
        /// <param name="j">
        /// The Jtoken as an instance representing the image object in the response from the
        /// DB api.
        /// </param>
        /// <returns>
        /// An DBImage referred as an IDBImage.
        /// </returns>
        public static IDBImage ConverToDBImage(JToken j)
        {
            return j.ToObject<IDBImage>();
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
                DB.MLC.MakeGetRequest(DB.TodayImages, new Dictionary<string, string>()
                {{ "page", pageoffset.ToString()}}).Content;
            return APIs.JsonToJObject(response);
        }

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
        /// <summary>
        /// Async method for getting today's images. 
        /// </summary>
        /// <returns></returns>
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
        /// This method performs a search on the DB api and return the result. 
        /// </summary>
        /// <param name="strquery">The query string</param>
        /// <returns></returns>
        public static JObject SearchDB(string strquery, int page =1, int perpage= 50)
        {
            var parameters = new Dictionary<string, string>()
            { { "page" , page.ToString()}, {"perpage", perpage.ToString() } };
            string response = MLC.MakeGetRequest(DB.SearchEndpoint, parameters).Content;
            return APIs.JsonToJObject(response);
        }
    }

    /// <summary>
    /// This class represents an image object in the json response from the derpibooru apis.
    ///
    /// </summary>
    public class DBImage : IDBImage
    {
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public int downvotes { get; set; }
        public string file_name { get; set; }
        public DateTime first_seen_at { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public string tags { get; set; }
        public DateTime updated_at { get; set; }
        public int upvotes { get; set; }
        public override string ToString()
        {
            string nl = Environment.NewLine;
            StringBuilder sb = new StringBuilder();
            sb.Append(id+ nl);
            sb.Append(created_at.ToString() + nl);
            sb.Append(updated_at.ToString() + nl);
            sb.Append(first_seen_at.ToString() + nl);
            sb.Append(upvotes.ToString() + nl);
            sb.Append(downvotes.ToString() + nl);
            sb.Append(tags + nl);
            sb.Append("https:" + image+nl);
            sb.Append(file_name+nl);
            sb.Append(description+nl);
            return sb.ToString();
        }

    }

    public class DBImageConverter : CustomCreationConverter<IDBImage>
    {
        public override IDBImage Create(Type objectType)
        {
            return new DBImage();
        }
    }
}