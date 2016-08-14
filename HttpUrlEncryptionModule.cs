using System;
using System.Text;
using System.Web;

namespace BidKaro
{
    public class HttpUrlEncryptionModule : IHttpModule
    {
        private const string EncryptionKey = "287C5D125D6B7E7223E1F719E3D58D17BB967703017E1BBE28618FAC6C4501E910C7E59800B5D4C2EDD5B0ED98874A3E952D60BAF260D9D374A74C76CB741803";
        private const string KeyForEncryptedQueryString = "D";
        private const string GetMethod = "GET";

        #region IHttpModule Members
        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ApplicationContextBeginRequest;
        }

        #endregion
        /// <summary>
        /// Begin Request event that encrypts or decrypts the URL.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationContextBeginRequest(object sender, EventArgs e)
        {
            var httpContext = HttpContext.Current;
            if (!httpContext.Request.RawUrl.Contains("?")) return;
            if (httpContext.Request.Url.Query.Length == 0) return;
            var query = httpContext.Request.Url.Query.Substring(1);
            var path = HttpContext.Current.Request.Path;

            if (query.StartsWith(KeyForEncryptedQueryString, StringComparison.OrdinalIgnoreCase))
            {
                // Decrypts the query string and rewrites the path.
                var rawQuery = query.Replace(KeyForEncryptedQueryString + "=", string.Empty);
                var decryptedQuery = EncryptDecryptQueryString(rawQuery, false);
                httpContext.RewritePath(path, string.Empty, decryptedQuery);
            }
            else if (httpContext.Request.HttpMethod.Equals(GetMethod, StringComparison.OrdinalIgnoreCase)
                     && !query.StartsWith(KeyForEncryptedQueryString, StringComparison.OrdinalIgnoreCase))
            {
                // Encrypt the query string and redirects to the encrypted URL.
                // Remove if you don't want all query strings to be encrypted automatically.
                if (path.ToLowerInvariant().EndsWith(".js") || path.ToLowerInvariant().EndsWith(".css"))
                {
                    return;
                }
                var encryptedQuery = EncryptDecryptQueryString(query, true);
                httpContext.Response.Redirect(path + encryptedQuery);
            }
            else if (httpContext.Request.HttpMethod.Equals(GetMethod, StringComparison.OrdinalIgnoreCase)
                     && query.StartsWith(KeyForEncryptedQueryString, StringComparison.OrdinalIgnoreCase))
            {
                // Encrypt the query string and redirects to the encrypted URL.
                // Remove if you don't want all query strings to be encrypted automatically.
                var encryptedQuery = EncryptDecryptQueryString(query, false);
                httpContext.Response.Redirect(path + encryptedQuery);
            }
        }

        /// <summary>
        /// Encrypts or Decrypts the Querystring.
        /// </summary>
        /// <param name="queryString">QueryString to encrypt or decrypt.</param>
        /// <param name="isEncryption">True if for encryption.</param>
        /// <returns>
        /// Encrypted/Decrypted string.
        /// </returns>
        private string EncryptDecryptQueryString(string queryString, bool isEncryption)
        {
            if (isEncryption)
            {
                return "?" + KeyForEncryptedQueryString + "="
                    + EncryptDecryptText(true, queryString, EncryptionKey);
            }
            return EncryptDecryptText(false, queryString, EncryptionKey);
        }

        /// <summary>
        /// Encrypts the decrypt text.
        /// </summary>
        /// <param name="isEncode">if set to <c>true</c> [is encode].</param>
        /// <param name="text">The text.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <returns></returns>
        private static string EncryptDecryptText(bool isEncode, string text, string encryptionKey)
        {
            // Step 1. Hash the encryptionKey using MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            var hashProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(Encoding.Unicode.GetBytes(encryptionKey));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            var tdesAlgorithm = new System.Security.Cryptography.TripleDESCryptoServiceProvider
            {
                Key = tdesKey,
                Mode =
                                        System.Security
                                        .Cryptography
                                        .CipherMode.ECB,
                Padding =
                                        System.Security
                                        .Cryptography
                                        .PaddingMode.PKCS7
            };

            // Step 3. Setup the encoder

            // Step 4. Convert the input text to a byte[]
            byte[] dataToEncryptDecrypt = isEncode ? Encoding.Unicode.GetBytes(text) : Convert.FromBase64String(text);

            // Step 5. Attempt to encrypt/Decrypt the string
            byte[] encryptDecryptedBytes;
            try
            {
                if (isEncode)
                {
                    var encryptor = tdesAlgorithm.CreateEncryptor();
                    encryptDecryptedBytes = encryptor.TransformFinalBlock(dataToEncryptDecrypt, 0,
                                                                          dataToEncryptDecrypt.Length);
                }
                else
                {
                    var decryptor = tdesAlgorithm.CreateDecryptor();
                    encryptDecryptedBytes = decryptor.TransformFinalBlock(dataToEncryptDecrypt, 0,
                                                                          dataToEncryptDecrypt.Length);
                }
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return isEncode ? Convert.ToBase64String(encryptDecryptedBytes) : Encoding.Unicode.GetString(encryptDecryptedBytes);
        }
    }
}