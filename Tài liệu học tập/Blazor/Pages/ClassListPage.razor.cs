using Blazored.LocalStorage;
using Blazored.Toast.Services;
using DemoBlazor.Common;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazor.Pages
{
    public partial class ClassListPage : ComponentBase
    {
        [Inject] private IToastService _toastService { get; set; }
        [Inject] private ILocalStorageService _localStorage { set; get; }
        [Inject] private DialogService _dialogService { set; get; }
        [Inject] private IClassService _classService { set; get; }

        private List<MyClass> _listClass;
        private string _searchName;

        private string _deleteId { set; get; }

        public List<MyClass> _initList = new List<MyClass>();

        protected override async Task OnInitializedAsync()
        {
            var amountOfKey = await _localStorage.LengthAsync();
            if (amountOfKey == 0)
            {
                await _localStorage.SetItemAsync(LocalStorageKey.Classes, new List<MyClass>());
                await _localStorage.SetItemAsync(LocalStorageKey.Students, new List<Student>());
            }
            _listClass = await _classService.GetAllAsync();
        }

        private async Task Delete(string deleteId)
        {
            _deleteId = deleteId;
            await _dialogService.ShowPopup(async (res) =>
            {
                await OnConfirmDelete(res);
            });
        }

        private async Task SearchIssues()
        {
            string searchString = _searchName.Trim();
            _listClass = await _classService.Search(_searchName);
        }

        private async Task OnConfirmDelete(bool deleteComfirmed)
        {
            if (deleteComfirmed)
            {
                var isSuccess = await _classService.Delete(_deleteId);
                if (isSuccess)
                {
                    _listClass = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
                    StateHasChanged();
                    _toastService.ShowSuccess("Delete success");
                }
                else
                {
                    _toastService.ShowError("An error has occurred");
                }
            }
        }
    }
}