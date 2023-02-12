using Cassandra;
using Cassandra.Mapping;
using CassandraDemo.Mappings;

namespace CassandraDemo
{
    public class SessionManager
    {
        static SessionManager()
        {
            MappingConfiguration.Global.Define<CassandraDemoMappings>();
        }

        private static ISession session;

        public static ISession GetSession()
        {
            if (session == null)
            {
                Cluster cluster = Cluster.Builder()
                    .AddContactPoint("127.0.0.1")
                    .Build();
                session = cluster.Connect(CassandraDemo.Constants.Constants.KeyspaceName);
            }
            return session;
        }
    }
}
