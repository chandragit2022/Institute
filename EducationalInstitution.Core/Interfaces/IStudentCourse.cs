using EducationalInstitution.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Interfaces
{
    public interface IStudentCourse
    {
        Task<StudentCourses> AssignCourse(StudentCourses studentCourses);
        Task RemoveCourse(int ID);
        Task<StudentCourses> Get(int StudentID, int CourseID);
        Task<StudentCourses> GetByID(int ID);
        Task<IEnumerable<StudentCoursesDTO>> GetStudentCourse(int studentID);

    }
}
