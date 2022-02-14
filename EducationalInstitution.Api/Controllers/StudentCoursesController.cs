using EducationalInstitution.Core.Interfaces;
using EducationalInstitution.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalInstitution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IStudentCourse _studentCourseService;
        private readonly ILogger<StudentCoursesController> _logger;

        public StudentCoursesController(IStudentCourse studentCourseService, ILogger<StudentCoursesController> logger)
        {
            this._studentCourseService = studentCourseService;
            _logger = logger;

        }

        [HttpGet("{studentID:int}")]
        public async Task<IEnumerable<StudentCoursesDTO>> GetStudentCourse(int studentID)
        {
            try
            {
                return await _studentCourseService.GetStudentCourse(studentID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }

        [HttpPost("AssignCourse")]
        public async Task<ActionResult<StudentCourses>> AssignCourse([FromBody] StudentCourses studentCourse)
        {
            try
            {
                var newCourse = await _studentCourseService.AssignCourse(studentCourse);
                return CreatedAtAction("AssignCourse", new { studentID = newCourse.ID }, newCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }

        [HttpDelete("{ID:int}")]
        public async Task<ActionResult> RemoveCourse(int ID)
        {
            try
            {
                var courseToDelete = await _studentCourseService.GetByID(ID);
                if (courseToDelete == null)
                    return NotFound();

                await _studentCourseService.RemoveCourse(courseToDelete.ID);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }

        }

        [HttpDelete]
        private async Task<ActionResult> RemoveCourseWithParameters(int studentID,int courseID)
        {
            try
            {
                var courseToDelete = await _studentCourseService.Get(studentID, courseID);
                if (courseToDelete == null)
                    return NotFound();

                await _studentCourseService.RemoveCourse(courseToDelete.ID);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }

        }
    }
}
