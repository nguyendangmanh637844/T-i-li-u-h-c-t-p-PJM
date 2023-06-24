using Blazored.LocalStorage;
using DemoBlazor.Common;
using DemoBlazor.Constants;
using DemoBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazor.Service
{
    public class StudentService : IStudentService
    {
        private readonly ILocalStorageService _localStorage;

        public StudentService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Student>> GetAll()
        {
            return await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
        }

        public async Task<List<Student>> GetStudentOfClass(string classId)
        {
            var list = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
            return list.Where(cl => cl.ClassId == classId).ToList();
        }

        public async Task<bool> Delete(string studentId)
        {
            var isSuccess = true;
            try
            {
                var list = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
                var studentDelete = list.Where(cl => cl.Id == studentId).FirstOrDefault();
                list.Remove(studentDelete);
                await _localStorage.SetItemAsync(LocalStorageKey.Students, list);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<int> Create(Student student)
        {
            var list = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);

            student.FullName = student.FullName.Trim();
            student.ClassId = student.ClassId;

            if (String.IsNullOrWhiteSpace(student.FullName) || String.IsNullOrWhiteSpace(student.Birthday.ToString()) || String.IsNullOrWhiteSpace(student.ClassId))
            {
                return (int)ResponseEnum.INVALIDINPUT;
            }
            else
            {
                if (list.Any(e => e.FullName == student.FullName && e.Birthday == student.Birthday && e.ClassId == student.ClassId))
                {
                    return (int)ResponseEnum.DUPLICATE;
                }
                else
                {
                    try
                    {
                        list.Add(student);
                        await _localStorage.SetItemAsync(LocalStorageKey.Students, list);
                    }
                    catch (Exception)
                    {
                        return (int)ResponseEnum.ERROR;
                    }
                    return (int)ResponseEnum.SUCCESS;
                }
            }
        }

        public async Task<Student> GetStudentById(string studentId)
        {
            var list = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
            return list.Where(st => st.Id == studentId).SingleOrDefault();
        }

        public async Task<int> UpdateStudent(Student student)
        {
            student.FullName = student.FullName.Trim();
            var list = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
            var _currentStudent = await _localStorage.GetItemAsync<Student>(LocalStorageKey.CurrentStudent);
            if (student.FullName == _currentStudent.FullName && student.Birthday == _currentStudent.Birthday && student.ClassId == _currentStudent.ClassId)
            {
                await _localStorage.RemoveItemAsync(LocalStorageKey.CurrentStudent);
                return (int)ResponseEnum.NOTTHINGHASCHANGE;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(student.FullName) || String.IsNullOrWhiteSpace(student.Birthday.ToString()) || String.IsNullOrWhiteSpace(student.ClassId))
                {
                    await _localStorage.RemoveItemAsync(LocalStorageKey.CurrentStudent);
                    return (int)ResponseEnum.INVALIDINPUT;
                }
                else
                {
                    list = list.Where(st => st.ClassId == student.ClassId).ToList();
                    if (list.Any(e => e.FullName == student.FullName && e.Birthday == student.Birthday && e.ClassId == student.ClassId))
                    {
                        await _localStorage.RemoveItemAsync(LocalStorageKey.CurrentStudent);
                        return (int)ResponseEnum.DUPLICATE;
                    }
                    else
                    {
                        bool isSuccess = true;
                        try
                        {
                            var classToDelete = list?.Where(st => st.Id == student.Id).SingleOrDefault();
                            list.Remove(classToDelete);
                            list.Add(student);
                            await _localStorage.SetItemAsync(LocalStorageKey.Students, list);
                        }
                        catch (Exception)
                        {
                            await _localStorage.RemoveItemAsync(LocalStorageKey.CurrentStudent);
                            return (int)ResponseEnum.ERROR;
                        }
                        await _localStorage.RemoveItemAsync(LocalStorageKey.CurrentStudent);
                        return (int)ResponseEnum.SUCCESS;
                    }
                }
            }
        }
    }
}