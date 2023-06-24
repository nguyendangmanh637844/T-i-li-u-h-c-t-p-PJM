using Blazored.Toast.Services;
using DemoBlazor.Constants;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace DemoBlazor.Components
{
    public partial class CreateDialog : ComponentBase
    {
        [Inject] private IToastService _toast { set; get; }
        [Inject] private IStudentService _studentService { set; get; }
        [Parameter] public EventCallback<bool> CloseEventCallback { set; get; }

        private Student student = new Student();
        private bool _showDialog { set; get; }
        private string _classId { set; get; }

        public void Show(string classId)
        {
            ResetDialog();
            _classId = classId;
            _showDialog = true;
            StateHasChanged();
        }

        private void Close()
        {
            _showDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            student = new Student();
        }

        private async Task CreateSt(EditContext context)
        {
            student.FullName = student.FullName.Trim();
            student.ClassId = _classId;

            var res = await _studentService.Create(student);
            switch (res)
            {
                case (int)ResponseEnum.INVALIDINPUT:
                    _toast.ShowError("Invalid Input");
                    break;

                case (int)ResponseEnum.DUPLICATE:
                    _toast.ShowError("Duplicate student");
                    break;

                case (int)ResponseEnum.SUCCESS:
                    _toast.ShowSuccess("Create student success");
                    break;

                case (int)ResponseEnum.ERROR:
                    _toast.ShowError("Fail to create student");
                    break;

                default:
                    _toast.ShowError("An error has ocurred.");
                    break;
            }
            await CloseEventCallback.InvokeAsync(true);
            Close();
        }
    }
}