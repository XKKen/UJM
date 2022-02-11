using UJM.IdentityServer4.Services.Account.Dtos;

namespace UJM.IdentityServer4.Services.Account
{
    public interface IUserServices
    {     
        /// <summary>
           /// 用户登录
           /// </summary>
           /// <param name="dto"></param>
           /// <returns></returns>
        Task<ApiResult<UserOutPutDto>> Login(LoginInputDto dto);
    }
}
