using IdentityServer4.AccessTokenValidation;
using Ocelot.Cache;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using UJM.Gateway;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("configuration.json", false, true);//��ʾҪ��ȡ��������ļ�
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot()
    .AddConsul()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();//Ĭ���ֵ�洢---Ĭ��---���ɷֲ�ʽ��--�����洢�Ľ���
    })
    .AddPolly()
    ;//IOCע�ᣬOcelot��δ���

//������չ--�ǳ������Ļ���IOC����չ---IOC���Ʒ�ת���������ɶ���---����ע��ӳ���ϵ��Ȼ��������---�ǾͿ���ȥ����ӳ���ϵ�������Զ�����󣬾Ϳ���������չ

builder.Services.AddSingleton<IOcelotCache<CachedResponse>, CustomCache>();//IOC�滻

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

app.UseOcelot();//ʹ��Ocelot���м�������http��Ӧ


app.Run();
