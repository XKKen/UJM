using UJM.Services.Account.Dtos;

namespace UJM.Services.Account
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
