using EducationalInstitution.Core.Interfaces;
using EducationalInstitution.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalInstitution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudents _studentsService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudents studentsService, ILogger<StudentsController> logger)
        {
            this._studentsService = studentsService;
            _logger = logger;
        }

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            try
            {
                var newStudent = await _studentsService.Create(student);
                return CreatedAtAction("CreateStudent", new { studentID = newStudent.StudentID }, newStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }


        [HttpPut("{studentID:int}")]
        public async Task<ActionResult> UpdateStudent(int studentID, [FromBody] Student student)
        {
            try
            {
                if (studentID != student.StudentID)
                {
                    return BadRequest();
                }

                await _studentsService.Update(student);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }

        [HttpDelete("{studentID:int}")]
        public async Task<ActionResult> DeleteStudent(int studentID)
        {
            try
            {
                var StudentToDelete = await _studentsService.Get(studentID);
                if (StudentToDelete == null)
                    return NotFound();

                await _studentsService.Delete(StudentToDelete.StudentID);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }

        }

        private async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await _studentsService.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }
    }
}
