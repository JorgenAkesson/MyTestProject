using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingApp
{
    public class AppSettings
    {
        public string BillingBaseAddress { get; set; } = string.Empty;
    }

    public class GetSettings
    {
        public GetSettings()
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();

            Settings = config.GetSection("Settings").Get<AppSettings>();

        }

        public AppSettings Settings { get; private set; }
    }
}
