using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Kvetch.Linq;

namespace Kvetch.Domain
{
    public static class DomainConfiguration
    {

        public static readonly DateTime DefaultDateTime = new DateTime(1754, 1, 1);

        public static string KvetchConnectionStringName
        {
            get
            {
                return "Kvetch.Properties.Settings.KvetchConnectionString";
            }
        }

        public static string KvetchConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[KvetchConnectionStringName].ConnectionString;
            }
        }

        public static Database CreateDatabase()
        {
            return DatabaseFactory.CreateDatabase(KvetchConnectionStringName);
        }

        public static KvetchDataContext CreateDataContext()
        {
            return new KvetchDataContext(KvetchConnectionString);
        }

    }
}
