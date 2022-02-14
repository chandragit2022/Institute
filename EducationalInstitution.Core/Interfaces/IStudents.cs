using EducationalInstitution.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Interfaces
{
    public interface IStudents
    {
        Task<Student> Create(Student student);        
        Task<Student> Get(int studentID);
        Task Update(Student student);
        Task Delete(int studentID);
        Task<IEnumerable<Student>> Get();
    }
}
