using Cassandra;
using CassandraDemo.DatabaseModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CassandraDemo.Helpers
{
    public static class StudentHelper
    {
        public static Student CreateStudentFromRow(Row row)
        {
            Student student = new Student();

            student.Id = row["id"] != null ? new Guid(row["id"].ToString()) : Guid.Empty;
            student.Ime = row["ime"] != null ? row["ime"].ToString() : string.Empty;
            student.Prezime = row["prezime"] != null ? row["prezime"].ToString() : string.Empty;

            return student;
        }

        public static IList<Student> CreateStudentsFromRowSet(RowSet rowSet)
        {
            IList<Student> students = new List<Student>();

            foreach (var row in rowSet)
            {
                students.Add(CreateStudentFromRow(row));
            }

            return students;
        }
    }
}
