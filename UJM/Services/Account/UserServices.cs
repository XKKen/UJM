using UJM.Models.Entities;
using UJM.Repositories;
using UJM.Services.Account.Dtos;

namespace UJM.Services.Account
{
    public class UserServices : IUserServices
    {
        private readonly IUJMRepository<UserEntity> _userRepository;
        public UserServices(IUJMRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult<UserOutPutDto>> Login(LoginInputDto dto)
        {
            var result = new ApiResult<UserOutPutDto>();

            var user = await _userRepository.FirstOrDefaultAsync(c => c.Email == dto.Email);

            if (user == null)
            {
                result.Message = "用户不存在";
                result.StatusCode = 400;
                return result;
            }

            if (user.Password != dto.PassWord)
            {
                result.Message = "密码错误";
                result.StatusCode = 400;
                return result;
            }

            result.Data = new UserOutPutDto()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name
            };

            result.StatusCode = 200;
            return result;
        }
    }
}
