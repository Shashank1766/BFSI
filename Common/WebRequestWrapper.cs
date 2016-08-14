using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BidKaro.Common
{
    public class WebRequestWrapper : IDisposable
    {
        public WebRequest Request;
        private const string HeaderAuthorization = "Authorization";
        private const string HeaderAuthorizationType = "OAuth ";
        private const string ContentType = "application/json; charset=utf-8";
        private const string ByDefaultRequestMethod = "POST";
        private const string consumerKey = "Shashank";
        private const string consumerSecret = "SecretGivenToConsumer";

        public WebRequestWrapper(string method, Uri uri)
        {
            var requestUri = uri;
            Request = WebRequest.Create(requestUri);
            Request.Method = method;
            Request.ContentType = ContentType;
            AddRequestHeaders(method, uri);
        }

        public WebRequestWrapper(string method, Uri uri, string contentType, Int32 timeout, Int32 readtimeout)
        {
            var requestUri = uri;
            Request = WebRequest.Create(requestUri);
            Request.Method = method;
            Request.ContentType = contentType;
            Request.Timeout = timeout;
            //Request.ReadWriteTimeout = 1000 * (60 * 6);
            AddRequestHeaders(method, uri);
        }
        public WebRequestWrapper(Uri uri)
        {
            var requestUri = uri;
            Request = WebRequest.Create(requestUri);
            Request.Method = ByDefaultRequestMethod;
            Request.ContentType = ContentType;
            AddRequestHeaders(ByDefaultRequestMethod, uri);
        }

        private void AddRequestHeaders(string method, Uri uri)
        {
            //string url, param;
            //var oAuth = new OAuthBase();
            //var nonce = oAuth.GenerateNonce();
            //var timestamp = oAuth.GenerateTimeStamp();
            //var signature = oAuth.GenerateSignature(uri, consumerKey, consumerSecret, string.Empty, string.Empty, method, timestamp, nonce, OppmannEnums.SignatureTypes.HMACSHA1, out url, out param);
            //string svcCredentials = string.Format("{0}&oauth_signature={1}", param, HttpUtility.UrlEncode(signature));
            //Request.Headers.Add(HeaderAuthorization, HeaderAuthorizationType + svcCredentials);


            // basic authentication headers. 
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("serviceuser" + ":" + "~!YNe*(gWj]yf_c8qU"));
            Request.Headers.Add("Authorization", "Basic " + svcCredentials);
        }
        #region IDisposable Members
        public void Dispose()
        {
            Request = null;
        }
        #endregion
    }
}