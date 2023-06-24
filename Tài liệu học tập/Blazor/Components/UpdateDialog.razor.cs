using Blazored.LocalStorage;
using Blazored.Toast.Services;
using DemoBlazor.Common;
using DemoBlazor.Constants;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace DemoBlazor.Components
{
    public partial class UpdateDialog : ComponentBase
    {
        [Parameter] public EventCallback<bool> CloseEventCallback { set; get; }

        [Inject] private IToastService _toast { set; get; }
        [Inject] private NavigationManager _nav { set; get; }
        [Inject] private ILocalStorageService _localStorage { set; get; }
        [Inject] private IStudentService _studentService { set; get; }

        private Student _student = new Student();
        private bool _showDialog { set; get; }

        public async Task ShowAsync(string studentId)
        {
            _student = await _studentService.GetStudentById(studentId);
            await _localStorage.SetItemAsync(LocalStorageKey.CurrentStudent, _student);
            Show();
        }

        public void Show()
        {
            _showDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            _showDialog = false;
            StateHasChanged();
        }

        private async Task UpdateStudent(EditContext context)
        {
            var res = await _studentService.UpdateStudent(_student);
            switch (res)
            {
                case (int)ResponseEnum.NOTTHINGHASCHANGE:
                    _toast.ShowWarning("Not thing change");
                    _showDialog = false;
                    await CloseEventCallback.InvokeAsync(true);
                    StateHasChanged();
                    break;

                case (int)ResponseEnum.INVALIDINPUT:
                    _toast.ShowError("Invalid Input");
                    break;

                case (int)ResponseEnum.DUPLICATE:
                    _toast.ShowError("Duplicate student");
                    break;

                case (int)ResponseEnum.SUCCESS:
                    _nav.NavigateTo($"/edit/{_student.ClassId}");
                    _toast.ShowSuccess("Update success");
                    break;

                case (int)ResponseEnum.ERROR:
                    _nav.NavigateTo($"/edit/{_student.ClassId}");
                    _toast.ShowError("An error occured.");
                    break;

                default:
                    _nav.NavigateTo($"/edit/{_student.ClassId}");
                    _toast.ShowError("An error occured.");
                    break;
            }
            Close();
            await CloseEventCallback.InvokeAsync(true);
        }
    }
}