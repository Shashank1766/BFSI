using System;
using System.Net;
using System.Text;

namespace BidKaro.Common
{
    public class WebClientWrapper : IDisposable
    {
        public WebClient WebClient;
        private const string ConsumerKey = "Shashank";
        private const string ConsumerSecret = "SecretGivenToConsumer";
        private const string ByDefaultRequestMethod = "GET";
        private const string HeaderAuthorization = "Authorization";
        private const string HeaderAuthorizationType = "OAuth ";
        public WebClientWrapper(Uri uri)
        {
            WebClient = new WebClient();
            AddRequestHeaders(ByDefaultRequestMethod, uri);
        }

        private void AddRequestHeaders(string method, Uri uri)
        {
            //string url, param;
            //var oAuth = new OAuthBase();
            //var nonce = oAuth.GenerateNonce();
            //var timestamp = oAuth.GenerateTimeStamp();
            //var signature = oAuth.GenerateSignature(uri, ConsumerKey, ConsumerSecret, string.Empty, string.Empty, method, timestamp, nonce, OppmannEnums.SignatureTypes.HMACSHA1, out url, out param);
            //string svcCredentials = string.Format("{0}&oauth_signature={1}", param, HttpUtility.UrlEncode(signature));
            //WebClient.Headers.Add(HeaderAuthorization, HeaderAuthorizationType + svcCredentials);

            // basic authentication headers. 
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("serviceuser" + ":" + "~!YNe*(gWj]yf_c8qU"));
            WebClient.Headers.Add("Authorization", "Basic " + svcCredentials);
        }
        public WebClientWrapper(WebClient webClient, Uri uri)
        {
            WebClient = webClient;
            AddRequestHeaders(ByDefaultRequestMethod, uri);
        }
        #region IDisposable Members
        public void Dispose()
        {
            WebClient = null;
        }
        #endregion
    }
}