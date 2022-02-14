using EducationalInstitution.Core.Interfaces;
using EducationalInstitution.Data;
using EducationalInstitution.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Services
{
    public class CoursesService : ICourses
    {
        private readonly SQLDBContext _DBContext;

        public CoursesService(SQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public async Task<Courses> Create(Courses courses)
        {
            _DBContext.Courses.Add(courses);
            await _DBContext.SaveChangesAsync();

            return courses;
        }
        public async Task<IEnumerable<Courses>> Get()
        {
            return await _DBContext.Courses.ToListAsync();
        }
        public async Task<Courses> Get(int courseID)
        {
            return await _DBContext.Courses.FindAsync(courseID);
        }
        public async Task Update(Courses course)
        {
            _DBContext.Entry(course).State = EntityState.Modified;
            await _DBContext.SaveChangesAsync();
        }
        public async Task Delete(int courseID)
        {
            var courseToDelete = await _DBContext.Courses.FindAsync(courseID);
            _DBContext.Courses.Remove(courseToDelete);
            await _DBContext.SaveChangesAsync();
        }
    }
}
