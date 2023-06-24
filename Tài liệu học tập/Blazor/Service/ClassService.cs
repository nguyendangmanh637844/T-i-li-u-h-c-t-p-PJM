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
    public class ClassService : IClassService
    {
        private readonly ILocalStorageService _localStorage;

        public ClassService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<MyClass>> GetAllAsync()
        {
            var list = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
            return list;
        }

        public async Task<List<MyClass>> Search(string searchName)
        {
            string searchString = searchName.Trim();
            var listClass = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
            if (String.IsNullOrWhiteSpace(searchName)) return listClass;
            return listClass.Where(cl => cl.ClassId.Contains(searchString) || cl.Description.Contains(searchString) || cl.Name.Contains(searchString)).ToList();
        }

        public async Task<bool> Delete(string classId)
        {
            var isSuccess = true;
            try
            {
                var listStudent = await _localStorage.GetItemAsync<List<Student>>(LocalStorageKey.Students);
                var studentsToDelete = listStudent.Where(st => st.ClassId == classId).ToList();
                if (studentsToDelete.Count > 0)
                {
                    studentsToDelete.ForEach(st => listStudent.Remove(st));
                    await _localStorage.SetItemAsync(LocalStorageKey.Students, listStudent);
                }

                var list = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
                var classToDelete = list.Where(cl => cl.ClassId == classId).SingleOrDefault();
                list.Remove(classToDelete);
                await _localStorage.SetItemAsync(LocalStorageKey.Classes, list);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<int> Update(MyClass myClass)
        {
            myClass.Name = myClass.Name.Trim();
            myClass.Description = myClass.Description.Trim();
            var response = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
            var currentClass = response?.Where(a => a.ClassId == myClass.ClassId).SingleOrDefault();

            if (myClass.Name == currentClass.Name && myClass.Description == currentClass.Description)
            {
                return (int)ResponseEnum.NOTTHINGHASCHANGE;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(myClass.Name) || String.IsNullOrWhiteSpace(myClass.Description))
                {
                    return (int)ResponseEnum.INVALIDINPUT;
                }
                else
                {
                    var classList = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
                    var otherClass = classList.Where(a => a.ClassId != currentClass.ClassId);

                    if (otherClass.Any(c => c.Name == myClass.Name))
                    {
                        return (int)ResponseEnum.DUPLICATE;
                    }
                    else
                    {
                        bool isSuccess = true;
                        try
                        {
                            var list = response;
                            var classToDelete = list?.Where(cl => cl.ClassId == myClass.ClassId).SingleOrDefault();
                            list.Remove(classToDelete);
                            list.Add(myClass);
                            await _localStorage.SetItemAsync(LocalStorageKey.Classes, list);
                        }
                        catch (Exception)
                        {
                            return (int)ResponseEnum.ERROR;
                        }
                        return (int)ResponseEnum.SUCCESS;
                    }
                }
            }
        }

        public async Task<int> CreateClass(MyClass myClass)
        {
            myClass.Name = myClass.Name.Trim();
            myClass.Description = myClass.Description.Trim();

            var list = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
            if (String.IsNullOrWhiteSpace(myClass.Name) || String.IsNullOrWhiteSpace(myClass.Description))
            {
                return (int)ResponseEnum.INVALIDINPUT;
            }
            else
            {
                if (list.Any(c => c.Name == myClass.Name))
                {
                    return (int)ResponseEnum.DUPLICATE;
                }
                else
                {
                    var isSuccess = true;
                    try
                    {
                        var listClass = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
                        if (list is null) list = new List<MyClass>();
                        list.Add(myClass);
                        await _localStorage.SetItemAsync(LocalStorageKey.Classes, list);
                    }
                    catch (Exception)
                    {
                        return (int)ResponseEnum.ERROR;
                    }
                    return (int)ResponseEnum.SUCCESS;
                }
            }
        }
    }
}