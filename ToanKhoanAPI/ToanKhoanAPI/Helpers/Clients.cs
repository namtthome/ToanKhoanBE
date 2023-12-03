using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace vn.com.pnsuite.toankhoan.Helpers
{
    public class Clients
    {
        public static string GetIp(HttpContext httpContext)
        {
            var remoteIpAddress = httpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }
    }
}
