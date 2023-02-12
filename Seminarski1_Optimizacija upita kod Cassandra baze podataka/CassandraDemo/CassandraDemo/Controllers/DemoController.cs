using Cassandra;
using Cassandra.Mapping;
using CassandraDemo.DatabaseModels;
using CassandraDemo.Helpers;
using CassandraDemo.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public DemoController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("students")]
        public ActionResult GetStudents()
        {
            try
            {
                var students = _studentRepository.GetStudents();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("students-new")]
        public ActionResult GetStudentsNew()
        {
            try
            {
                var students = _studentRepository.GetStudentsNew();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("test-batch/{numberOfQueries}")]
        public async Task<ActionResult> TestBatch([FromRoute(Name = "numberOfQueries")] int numberOfQueries)
        {
            try
            {
                var elapsedMilliseconds = await _studentRepository.TestBatch(numberOfQueries);

                return Ok(elapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("test-multiple-queries/{numberOfQueries}")]
        public ActionResult TestMultipleQueries([FromRoute(Name = "numberOfQueries")] int numberOfQueries)
        {
            try
            {
                var elapsedMilliseconds = _studentRepository.TestMultipleQueries(numberOfQueries);

                return Ok(elapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("execute-with-prepare/{numberOfQueries}")]
        public ActionResult ExecuteWithPrepare([FromRoute(Name = "numberOfQueries")] int numberOfQueries)
        {
            try
            {
                var elapsedMilliseconds = _studentRepository.ExecuteWithPrepare(numberOfQueries);

                return Ok(elapsedMilliseconds);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("execute-without-prepare/{numberOfQueries}")]
        public ActionResult ExecuteWithoutPrepare([FromRoute(Name = "numberOfQueries")] int numberOfQueries)
        {
            try
            {
                var elapsedMilliseconds = _studentRepository.ExecuteWithoutPrepare(numberOfQueries);

                return Ok(elapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
