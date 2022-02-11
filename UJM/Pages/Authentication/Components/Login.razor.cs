using MASA.Blazor.Presets;
using UJM.Services.Account;
using UJM.Services.Account.Dtos;

namespace UJM.Pages.Authentication.Components
{
    public partial class Login : UJMPageBase
    {
        private bool _show;


        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private IUserServices _userServices { get; set; } = default!;

        [Parameter]
        public bool HideLogo { get; set; }

        [Parameter]
        public double Width { get; set; } = 410;

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public string CreateAccountRoute { get; set; } = $"pages/authentication/register-v1";

        [Parameter]
        public string ForgotPasswordRoute { get; set; } = $"pages/authentication/forgot-password-v1";

        //[Parameter]
        //public EventCallback<MouseEventArgs> OnLogin { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        [Parameter]
        public EventCallback<LoginViewModel> OnLogin { get; set; }

        private async void AccountLogin()
        {
            var result = await _userServices.Login(new LoginInputDto() 
            {
                PassWord = Password,
                Email = Email,
            });

            if (result.StatusCode != 200) 
            {
                Message(result.Message, AlertTypes.Error);
            }
        }
    }

    public partial class LoginViewModel 
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

}