using Blazored.Toast.Services;
using DemoBlazor.Constants;
using DemoBlazor.Models;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace DemoBlazor.Pages
{
    public partial class CreatePage : ComponentBase
    {
        [Inject] private NavigationManager _navigationManager { set; get; }
        [Inject] private IToastService _toastService { get; set; }
        [Inject] private IClassService _classService { set; get; }

        private MyClass myClass = new MyClass();

        private async Task CreateClass(EditContext context)
        {
            var res = await _classService.CreateClass(myClass);
            switch (res)
            {
                case (int)ResponseEnum.INVALIDINPUT:
                    _toastService.ShowError("Invalid Input");
                    break;

                case (int)ResponseEnum.DUPLICATE:
                    _toastService.ShowError("Duplicate class.");
                    break;

                case (int)ResponseEnum.SUCCESS:
                    _navigationManager.NavigateTo("/");
                    _toastService.ShowSuccess("Create class success.");
                    break;

                case (int)ResponseEnum.ERROR:
                    _navigationManager.NavigateTo("/");
                    _toastService.ShowError("An error ocurred.");
                    break;

                default:
                    _navigationManager.NavigateTo("/");
                    _toastService.ShowError("An error ocurred.");
                    break;
            }
        }
    }
}