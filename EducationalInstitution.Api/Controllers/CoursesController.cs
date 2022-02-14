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
    public class CoursesController : ControllerBase
    {
        private readonly ICourses _coursesService;
        private readonly ILogger<CoursesController> _logger;


        public CoursesController(ICourses coursesService, ILogger<CoursesController> logger)
        {
            this._coursesService = coursesService;
            _logger = logger;
        }

        [HttpPost("CreateCourse")]
        public async Task<ActionResult<Courses>> CreateCourse([FromBody] Courses course)
        {
            try
            {
                var newCourse = await _coursesService.Create(course);
                return CreatedAtAction(nameof(GetCourses), new { courseID = newCourse.CourseID }, newCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }

        [HttpGet("GetCourses")]
        public async Task<IEnumerable<Courses>> GetCourses()
        {
            try
            {                
                return await _coursesService.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                throw ex;
            }
        }

        [HttpDelete("{courseID:int}")]
        public async Task<ActionResult> DeleteCourse(int courseID)
        {
            try
            {
                var CoursesToDelete = await _coursesService.Get(courseID);
                if (CoursesToDelete == null)
                    return NotFound();

                await _coursesService.Delete(CoursesToDelete.CourseID);
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
