using BidKaro.Common;
using BidKaro.Model;
using BidKaro.Models;

namespace BidKaro.WebCommunicator
{
    public class HomeWebCommunicator : BaseWebCommunicator
    {
        /// <summary>
        /// Filters the activity and group by member identifier.
        /// </summary>
        /// <returns></returns>
        public Users Logout()
        {
            var uri = ServiceFunctions.Logout;
            return WebRequestJsSerializerMethod<Users>(null, uri);
        }
    }
}