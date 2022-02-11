using UJM.IdentityServer4;
using UJM.IdentityServer4.Model.DataBase;
using UJM.IdentityServer4.Model.Repositories;
using UJM.IdentityServer4.Services.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthorzeContext>();
builder.Services.AddTransient(typeof(IUJMRepository<>), typeof(UJMRepository<>));
builder.Services.AddSingleton<IUserServices, UserServices>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region IOC
builder.Services.AddIdentityServer()//ids4怎么用的
.AddDeveloperSigningCredential()//临时生成的证书--即时生成的
.AddInMemoryClients(ClientInitConfig.GetClients())//InMemory 内存模式
.AddInMemoryApiScopes(ClientInitConfig.GetApiScopes())//指定作用域
.AddInMemoryApiResources(ClientInitConfig.GetApiResources())//能访问啥资源
.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();//用户验证
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region 中间件

app.UseStaticFiles();
app.UseIdentityServer();//使用这个中间件来处理请求
#endregion

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}