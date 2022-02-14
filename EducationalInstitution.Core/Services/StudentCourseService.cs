using EducationalInstitution.Core.Interfaces;
using EducationalInstitution.Data;
using EducationalInstitution.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Services
{
    public class StudentCourseService : IStudentCourse
    {
        private readonly SQLDBContext _DBContext;

        public StudentCourseService(SQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public async Task<StudentCourses> AssignCourse(StudentCourses studentCourse)
        {
            _DBContext.StudentCourses.Add(studentCourse);
            await _DBContext.SaveChangesAsync();

            return studentCourse;
        }

        public async Task RemoveCourse(int ID)
        {
            var courseToDelete = await _DBContext.StudentCourses.FindAsync(ID);
            _DBContext.StudentCourses.Remove(courseToDelete);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<StudentCourses> Get(int StudentID, int CourseID)
        {
            StudentCourses StudentCoursesRec = _DBContext.StudentCourses.Where(x => x.StudentID == StudentID && x.CourseID == CourseID).FirstOrDefault();

            return await Task.FromResult(StudentCoursesRec);
        }

        public async Task<StudentCourses> GetByID(int ID)
        {
            return await _DBContext.StudentCourses.FindAsync(ID);
        }

        public async Task<IEnumerable<StudentCoursesDTO>> GetStudentCourse(int studentID)
        {
            List<StudentCoursesDTO> studentCourses = (from SC in _DBContext.StudentCourses
                                                      join S in _DBContext.Students on SC.StudentID equals S.StudentID
                                                      join C in _DBContext.Courses on SC.CourseID equals C.CourseID
                                                      where SC.StudentID == studentID
                                                      orderby SC.StudentID
                                                      select new StudentCoursesDTO
                                                      {
                                                          StudentName = S.FirstName + " " + S.LastName,
                                                          CourseCode = C.Code,
                                                          ID = SC.ID
                                                      }).ToList();

            return await Task.FromResult(studentCourses);

        }
    }
}
