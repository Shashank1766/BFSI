using BidKaro.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;

namespace BidKaro.WebCommunicator
{
    public class BaseWebCommunicator
    {

        #region Common Function for Web Communication
        internal static string CommonWebRequestMethod(string strUri)
        {
            string outResult;
            using (var webRequestWrapper = new WebRequestWrapper("GET", new Uri(strUri)))
            {
                using (var response = webRequestWrapper.Request.GetResponse())
                {
                    //webRequestWrapper.Request.Timeout = 600000;
                    var reader = response.GetResponseStream();
                    if (reader == null) return null;
                    using (var sReader = new StreamReader(reader))
                    {
                        outResult = sReader.ReadToEnd();
                        sReader.Close();
                    }
                    reader.Close();
                    response.Close();
                }
            }
            return outResult;
        }
        /// <summary>
        /// Post verb to save new object in services
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strUri"></param>
        /// <returns></returns>
        internal static string CommonWebRequestJsSerializerMethod(object obj, string strUri)
        {
            string outResult;
            using (var webRequestWrapper = new WebRequestWrapper(new Uri(strUri)))
            {
                var jsonSerializer = new JavaScriptSerializer();
                jsonSerializer.MaxJsonLength = Int32.MaxValue;
                var serOut = jsonSerializer.Serialize(obj);
                using (var writer = new StreamWriter(webRequestWrapper.Request.GetRequestStream()))
                {
                    writer.Write(serOut);
                }
                using (var response = webRequestWrapper.Request.GetResponse())
                {
                    //swebRequestWrapper.Request.Timeout = 600000;
                    var reader = response.GetResponseStream();
                    if (reader == null) return string.Empty;
                    using (var sReader = new StreamReader(reader))
                    {
                        outResult = sReader.ReadToEnd();
                        sReader.Close();
                    }
                    reader.Close();
                    response.Close();
                }
            }
            return outResult;
        }
        /// <summary>
        /// Get list of object from web
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceUrl"></param>
        /// <returns></returns>
        internal static List<T> GetListOfObjects<T>(string serviceUrl)
        {
            try
            {
                return GetListOfObjectsExt<T>(serviceUrl);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Encountered unexpected character '<'"))
                {
                    return GetListOfObjectsExt<T>(serviceUrl);
                }
                throw;
            }
        }
        private static List<T> GetListOfObjectsExt<T>(string serviceUrl)
        {
            using (var wc = new WebClientWrapper(new Uri(serviceUrl)))
            {
                var res1 = wc.WebClient.DownloadData(serviceUrl);
                List<T> response;
                using (Stream res2 = new MemoryStream(res1))
                {
                    var res3 = new DataContractJsonSerializer(typeof(List<T>));
                    response = (List<T>)res3.ReadObject(res2);
                }
                return response;
            }
        }


        internal static IEnumerable<T> GetEnumerableOfObjects<T>(string serviceUrl)
        {
            using (var wc = new WebClientWrapper(new Uri(serviceUrl)))
            {
                var res1 = wc.WebClient.DownloadData(serviceUrl);
                IEnumerable<T> response;
                using (Stream res2 = new MemoryStream(res1))
                {
                    var res3 = new DataContractJsonSerializer(typeof(IEnumerable<T>));
                    response = (IEnumerable<T>)res3.ReadObject(res2);
                }
                return response;
            }
        }
        /// <summary>
        /// Post an object to service and get respont in object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="post"></param>
        /// <param name="funtionUrl"></param>
        /// <returns></returns>
        internal static T WebRequestJsSerializerMethod<T>(object post, string funtionUrl)
        {
            try
            {
                return WebRequestJsSerializerMethodExt<T>(post, funtionUrl);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Encountered unexpected character '<'"))
                {
                    return WebRequestJsSerializerMethodExt<T>(post, funtionUrl);
                }
                throw;
            }
        }
        /// Get response form URL
        internal static T CommonWebClientMethod<T>(string strUri)
        {
            try
            {
                return CommonWebClientExtMethod<T>(strUri);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Encountered unexpected character '<'"))
                {
                    return CommonWebClientExtMethod<T>(strUri);
                }
                throw;
            }
        }

        private static T CommonWebClientExtMethod<T>(string strUri)
        {
            T response;
            using (var wc = new WebClientWrapper(new Uri(strUri)))
            {
                var res1 = wc.WebClient.DownloadData(strUri);
                using (Stream res2 = new MemoryStream(res1))
                {
                    var res3 = new DataContractJsonSerializer(typeof(T));
                    response = (T)res3.ReadObject(res2);
                }
            }
            return response;
        }
        private static T WebRequestJsSerializerMethodExt<T>(object post, string funtionUrl)
        {
            object response = null;
            using (var webRequestWrapper = new WebRequestWrapper(new Uri(funtionUrl)))
            {
                var jsonSerializer = new JavaScriptSerializer();
                var serOut = jsonSerializer.Serialize(post);
                //webRequestWrapper.Request.Timeout = 600000;
                using (var writer = new StreamWriter(webRequestWrapper.Request.GetRequestStream()))
                {
                    writer.Write(serOut);
                }
                using (var webResponse = webRequestWrapper.Request.GetResponse())
                {
                    using (var reader = webResponse.GetResponseStream())
                    {
                        var res2 = new DataContractJsonSerializer(typeof(T));
                        if (reader != null) response = (T)res2.ReadObject(reader);
                    }
                    webResponse.Close();
                }
                return (T)response;
            }
        }
        #endregion
    }
}