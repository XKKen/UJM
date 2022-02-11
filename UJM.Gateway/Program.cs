using IdentityServer4.AccessTokenValidation;
using Ocelot.Cache;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using UJM.Gateway;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("configuration.json", false, true);//表示要读取这个配置文件
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot()
    .AddConsul()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();//默认字典存储---默认---换成分布式的--换个存储的介质
    })
    .AddPolly()
    ;//IOC注册，Ocelot如何处理

//来个扩展--非常常见的基于IOC的扩展---IOC控制反转，用来生成对象---会先注册映射关系，然后反射生成---那就可以去覆盖映射关系，换成自定义对象，就可以做到扩展

builder.Services.AddSingleton<IOcelotCache<CachedResponse>, CustomCache>();//IOC替换

#region Ids4
var authenticationProviderKey = "UserGatewayKey";
builder.Services.AddAuthentication("Bearer")
   .AddIdentityServerAuthentication(authenticationProviderKey, options =>
   {
       options.Authority = "http://172.16.32.169:49155";
       options.ApiName = "UserApi";
       options.RequireHttpsMetadata = false;
       options.SupportedTokens = SupportedTokens.Both;
   });
#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOcelot();//使用Ocelot的中间件来完成http响应


app.Run();
