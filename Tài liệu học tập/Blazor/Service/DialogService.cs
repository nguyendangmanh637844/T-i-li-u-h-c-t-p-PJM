using System;
using System.Threading.Tasks;

namespace DemoBlazor.Service
{
    public class DialogService
    {
        public event Func<Action<bool>, Task> Show;

        public async Task ShowPopup(Action<bool> func)
        {
            await Show.Invoke(func);
        }
    }
}