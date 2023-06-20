using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Steps
{
    public class HttpRequestAuthorization
    {
        public HttpRequestAuthorization(string address)
        {
            Address = address.ToLower();
            RequireHttps = (address.ToLower().Contains("https"));
        }

        public string Address { get; private set; }
        public bool RequireHttps { get; private set; }
    }
}
