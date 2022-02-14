using EducationalInstitution.Core.Interfaces;
using EducationalInstitution.Data;
using EducationalInstitution.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Services
{
    public class StudentsService : IStudents
    {
        private readonly SQLDBContext _DBContext;

        public StudentsService(SQLDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public async Task<Student> Create(Student student)
        {
            _DBContext.Students.Add(student);
            await _DBContext.SaveChangesAsync();

            return student;
        }     
        public async Task<Student> Get(int studentID)
        {
            return await _DBContext.Students.FindAsync(studentID);
        }
        public async Task Update(Student student)
        {
            _DBContext.Entry(student).State = EntityState.Modified;
            await _DBContext.SaveChangesAsync();
        }
        public async Task Delete(int studentID)
        {
            var studentToDelete = await _DBContext.Students.FindAsync(studentID);
            _DBContext.Students.Remove(studentToDelete);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> Get()
        {
            return await _DBContext.Students.ToListAsync();
        }
    }
}
