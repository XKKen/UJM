using IdentityServer4.Models;

namespace UJM.IdentityServer4
{
    public class ClientInitConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "用户获取API")
                {
                    Scopes={ "scope1" }//4.x必须写的
                }
            };
        }

        /// <summary>
        /// Api范围---4.x新增的
        /// </summary>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
              {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
              };
        }

        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "UJM.Auth",//客户端唯一标识
                    ClientName="Single AuthenticationCenter",
                    ClientSecrets = new [] { new Secret("ujm123456".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    //授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new [] { "scope1" },//允许访问的资源
                    
                }
            };
        }
    }
}
