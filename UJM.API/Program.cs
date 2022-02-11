using System.Net;
using UJM.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")//scheme--表示通过Bearer方式来解析用户信息
     .AddIdentityServerAuthentication(options =>
     {
         options.Authority = "http://172.16.32.169:49155";//ids4的地址--专门获取公钥
         options.ApiName = "UserApi";
         options.RequireHttpsMetadata = false;
     });//配置ids4

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapWhen(context => context.Request.Path.Equals("/Api/Health/Index"),
               applicationBuilder => applicationBuilder.Run(async context =>
               {
                   Console.WriteLine($"This is Health Check");
                   context.Response.StatusCode = (int)HttpStatusCode.OK;
                   await context.Response.WriteAsync("OK");
               }));
//初始化一次
app.Configuration.ConsulRegist();
app.Run();
