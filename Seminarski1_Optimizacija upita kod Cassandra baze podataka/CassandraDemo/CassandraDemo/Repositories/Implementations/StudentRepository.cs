using Cassandra;
using Cassandra.Mapping;
using CassandraDemo.DatabaseModels;
using CassandraDemo.Helpers;
using CassandraDemo.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraDemo.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        public IList<Student> GetStudents()
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }

            var studentRowSet = session.Execute($"SELECT * from student");
            IList<Student> students = StudentHelper.CreateStudentsFromRowSet(studentRowSet);

            return students;
        }

        public IList<Student> GetStudentsNew()
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }

            IMapper mapper = new Mapper(session);
            IList<Student> students = mapper.Fetch<Student>().ToList();

            return students;
        }

        public async Task<long> TestBatch(int numberOfQueries)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }

            IMapper mapper = new Mapper(session); 
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var batch = mapper.CreateBatch(BatchType.Logged);

            for (int i = 0; i <= numberOfQueries - 1; i++)
            {
                Student student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Ime = $"Ime{i + 1}",
                    Prezime = $"Prezime{i + 1}"
                };

                batch.Insert(student);
            }

            await mapper.ExecuteAsync(batch).ConfigureAwait(false);

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        public long TestMultipleQueries(int numberOfQueries)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }

            IMapper mapper = new Mapper(session);
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            for (int i = 0; i <= numberOfQueries - 1; i++)
            {
                Student student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Ime = $"Ime{i + 1}",
                    Prezime = $"Prezime{i + 1}"
                };

                mapper.Insert(student);
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        public long ExecuteWithoutPrepare(int numberOfQueries)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            for (int i = 0; i <= numberOfQueries - 1; i++)
            {
                Student student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Ime = $"Ime{i + 1}",
                    Prezime = $"Prezime{i + 1}"
                };
                string cql = $"INSERT INTO student (id, ime, prezime) VALUES ({student.Id}, '{student.Ime}', '{student.Prezime}')";
                session.Execute(cql);
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        public long ExecuteWithPrepare(int numberOfQueries)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                throw new Exception("Database isn't available at the moment, please try again later.");
            }
  
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            PreparedStatement preparedStatement = session.Prepare("INSERT INTO student (id, ime, prezime) VALUES (?, ?, ?)");
            for (int i = 0; i <= numberOfQueries - 1; i++)
            {
                Student student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Ime = $"Ime{i + 1}",
                    Prezime = $"Prezime{i + 1}"
                };
                BoundStatement bound = preparedStatement.Bind(student.Id, student.Ime, student.Prezime);
                session.Execute(bound);
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }
}
