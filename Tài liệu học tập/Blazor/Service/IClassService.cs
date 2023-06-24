using DemoBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazor.Service
{
    public interface IClassService
    {
        Task<List<MyClass>> GetAllAsync();

        Task<bool> Delete(string classId);

        Task<List<MyClass>> Search(string searchName);

        Task<int> Update(MyClass myClass);

        Task<int> CreateClass(MyClass myClass);
    }
}