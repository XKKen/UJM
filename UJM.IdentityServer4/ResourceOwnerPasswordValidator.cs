using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using UJM.IdentityServer4.Model.DataBase;
using UJM.IdentityServer4.Model.Entities;
using UJM.IdentityServer4.Services.Account;

namespace UJM.IdentityServer4
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserServices _userServices;

        public ResourceOwnerPasswordValidator(IUserServices userServices) 
        {
            _userServices = userServices;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ///这里为了省事才这样把用户验证的过程给写了，优雅的方式最好还是通过接口去实现
            using (var dbcontext = new AuthorzeContext())
            {
                var user = (_userServices.Login(new Services.Account.Dtos.LoginInputDto() 
                {
                    Email = context.UserName,
                    PassWord = context.Password
                })).Result;

                if (user.StatusCode==200)
                {
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                        claims: new Claim[]
                        {
                            new Claim("user_id", user.Data.Id.ToString()),
                            new Claim("user_name", user.Data.Email)
                        });
                }
                else
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid");
            }
            return Task.FromResult("");
        }
    }
}
