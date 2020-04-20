using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary
{
    public static class GlobalConfig
    {
        /// <summary>
        /// Returns the requested connection string
        /// </summary>
        /// <param name="name">The name of a connection (ie. "BobsAwesomeEatery")</param>
        /// <returns></returns>
        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
