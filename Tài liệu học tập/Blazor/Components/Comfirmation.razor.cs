using DemoBlazor.Service;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DemoBlazor.Components
{
    public partial class Comfirmation : ComponentBase
    {
        [Parameter] public string ConfirmationTitle { get; set; } = "Confirm Delete";
        [Parameter] public string ConfirmationMessage { get; set; } = "Are you sure you want to delete";

        [Inject] private DialogService _dialogService { set; get; }

        private bool _showConfirmation { get; set; }
        private System.Action<bool> _action;

        protected override Task OnInitializedAsync()
        {
            _dialogService.Show += _dialogService_Show;
            return base.OnInitializedAsync();
        }

        private Task _dialogService_Show(System.Action<bool> action)
        {
            Show();
            _action = action;
            return Task.Delay(500);
        }

        public void Show()
        {
            _showConfirmation = true;
            StateHasChanged();
        }

        public void Close()
        {
            _showConfirmation = false;
            StateHasChanged();
        }

        protected async Task OnConfirmationChange(bool value)
        {
            Close();
            _action.Invoke(value);
        }
    }
}