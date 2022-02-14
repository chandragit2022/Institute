using EducationalInstitution.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Core.Interfaces
{
    public interface ICourses
    {
        Task<Courses> Create(Courses course);
        Task<IEnumerable<Courses>> Get();
        Task<Courses> Get(int courseID);
        Task Update(Courses course);
        Task Delete(int courseID);
    }
}
