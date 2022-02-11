using UJM.Shared;
using PageBase = MASA.Blazor.PageBase;
namespace UJM.Pages
{
    public class UJMPageBase : PageBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [CascadingParameter]
        protected MainLayout App { get; set; } = default!;

        #region Message

        public void Message(string content, AlertTypes type = default, int timeout = 3000)
        {
            App.Message(content, type, timeout);
        }

        #endregion
    }
}
