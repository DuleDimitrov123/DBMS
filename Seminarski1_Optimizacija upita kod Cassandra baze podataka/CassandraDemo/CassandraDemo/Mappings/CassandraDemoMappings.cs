using CassandraDemo.DatabaseModels;

namespace CassandraDemo.Mappings
{
    public class CassandraDemoMappings : Cassandra.Mapping.Mappings
    {
        public CassandraDemoMappings()
        {
            For<Student>()
                .TableName(Constants.Constants.StudentColumnFamily)
                .PartitionKey(s => s.Id)
                .Column(s => s.Id, cm => cm.WithName(Constants.StudentColumnConstants.Id)).CaseSensitive()
                .Column(s => s.Ime, cm => cm.WithName(Constants.StudentColumnConstants.Ime)).CaseSensitive()
                .Column(s => s.Prezime, cm => cm.WithName(Constants.StudentColumnConstants.Prezime)).CaseSensitive();
        }
    }
}
