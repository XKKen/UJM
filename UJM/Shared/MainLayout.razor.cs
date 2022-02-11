using MASA.Blazor.Presets;

namespace UJM.Shared
{
    public partial class MainLayout
    {
        #region Message

        private Message.Model _message = new();

        public void Message(string content, AlertTypes type = AlertTypes.None, int timeout = 3000)
        {
            _message = new Message.Model
            {
                Visible = true,
                Content = content,
                Timeout = timeout,
                Type = type
            };

            StateHasChanged();
        }

        #endregion
    }
}
