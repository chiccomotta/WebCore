using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class ConnectionString
    {
        public ConnectionString()
        {
        }

        public string DefaultConnection { get; set; }
        public string MainDBConnectionString { get; set; }

        public Server ServerInfo { get; set; }
    }
}
