using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraDemo.DatabaseModels
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }
    }
}
