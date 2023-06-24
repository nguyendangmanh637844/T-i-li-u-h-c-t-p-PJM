using DemoBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazor.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();

        Task<List<Student>> GetStudentOfClass(string classId);

        Task<Student> GetStudentById(string studentId);

        Task<bool> Delete(string studentId);

        Task<int> Create(Student student);

        Task<int> UpdateStudent(Student student);
    }
}