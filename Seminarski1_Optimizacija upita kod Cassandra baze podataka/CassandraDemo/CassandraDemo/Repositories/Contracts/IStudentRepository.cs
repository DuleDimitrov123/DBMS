using CassandraDemo.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraDemo.Repositories.Contracts
{
    public interface IStudentRepository
    {
        public IList<Student> GetStudents();

        public IList<Student> GetStudentsNew();

        public Task<long> TestBatch(int numberOfQueries);

        public long TestMultipleQueries(int numberOfQueries);

        public long ExecuteWithPrepare(int numberOfQueries);

        public long ExecuteWithoutPrepare(int numberOfQueries);
    }
}
