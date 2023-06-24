using Blazored.LocalStorage;
using Blazored.Toast.Services;
using DemoBlazor.Common;
using DemoBlazor.Constants;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazor.Pages
{
    public partial class UpdatePage : ComponentBase
    {
        [Parameter] public string Id { get; set; }
        [Inject] private NavigationManager _navigationManager { set; get; }
        [Inject] private DialogService _dialogService { set; get; }
        [Inject] private IToastService _toast { get; set; }
        [Inject] private ILocalStorageService _localStorage { set; get; }
        [Inject] private IClassService _classService { set; get; }

        private MyClass myClass = new MyClass();
        private string _deleteId { set; get; }

        protected override async Task OnInitializedAsync()
        {
            var res = await _localStorage.GetItemAsync<List<MyClass>>(LocalStorageKey.Classes);
            myClass = res?.Where(a => a.ClassId == Id).SingleOrDefault();
        }

        private async Task Update(EditContext context)
        {
            var res = await _classService.Update(myClass);
            switch (res)
            {
                case (int)ResponseEnum.NOTTHINGHASCHANGE:
                    _toast.ShowWarning("Not thing has change");
                    break;

                case (int)ResponseEnum.INVALIDINPUT:
                    _navigationManager.NavigateTo("/");
                    _toast.ShowError("Invalid Input");
                    break;

                case (int)ResponseEnum.DUPLICATE:
                    _toast.ShowError("Duplicate class");
                    break;

                case (int)ResponseEnum.SUCCESS:
                    _navigationManager.NavigateTo("/");
                    _toast.ShowSuccess("Update success.");
                    break;

                case (int)ResponseEnum.ERROR:
                    _navigationManager.NavigateTo("/");
                    _toast.ShowError("An error occured.");
                    break;

                default:
                    _toast.ShowError("An error occured.");
                    break;
            }
        }

        private int Age(DateTime birthday)
        {
            return DateTime.Now.Year - birthday.Year;
        }

        private async Task DeleteClassAsync(string deleteId)
        {
            _deleteId = deleteId;
            await _dialogService.ShowPopup(async (res) =>
            {
                await OnConfirmDelete(res);
            });
        }

        private async Task OnConfirmDelete(bool deleteComfirmed)
        {
            if (deleteComfirmed)
            {
                var res = await _classService.Delete(_deleteId);
                if (res)
                {
                    _navigationManager.NavigateTo("/");
                    _toast.ShowSuccess("Delete success");
                }
                else
                {
                    _toast.ShowError("An error has occurred.");
                }
            }
        }
    }
}