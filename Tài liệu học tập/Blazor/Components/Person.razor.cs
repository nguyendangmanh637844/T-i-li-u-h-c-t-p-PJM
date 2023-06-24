using Blazored.LocalStorage;
using Blazored.Toast.Services;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazor.Components
{
    public partial class Person : ComponentBase
    {
        [Parameter] public string Id { get; set; }
        [Inject] private IToastService _toast { get; set; }
        [Inject] private DialogService _dialogService { set; get; }
        [Inject] private IStudentService _studentService { set; get; }

        private CreateDialog _createDialog { set; get; }
        private string _deleteIdSt { set; get; }
        private List<Student> _listStudent { get; set; }
        private UpdateDialog _updateDialog { set; get; }

        protected override async Task OnInitializedAsync()
        {
            _listStudent = await _studentService.GetStudentOfClass(Id);
        }

        public async Task OnConfirmDeleteStudent(bool deleteComfirmed)
        {
            if (deleteComfirmed)
            {
                var isSuccess = await _studentService.Delete(_deleteIdSt);
                if (isSuccess)
                {
                    _listStudent = await _studentService.GetStudentOfClass(Id);
                    StateHasChanged();
                    _toast.ShowSuccess("Delete success");
                }
                else _toast.ShowError("Delete false");
            }
        }

        public async Task ShowUpdateDialogAsync(string id)
        {
            await _updateDialog.ShowAsync(id);
        }

        public void ShowDialog(string id)
        {
            _createDialog.Show(id);
        }

        public async Task RefreshList()
        {
            _listStudent = await _studentService.GetStudentOfClass(Id);
        }

        public async Task DeleteStAsync(string deleteId)
        {
            _deleteIdSt = deleteId;
            await _dialogService.ShowPopup(async (res) =>
            {
                await OnConfirmDeleteStudent(res);
            });
        }
    }
}