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
builder.Services.AddIdentityServer()//ids4��ô�õ�
.AddDeveloperSigningCredential()//��ʱ���ɵ�֤��--��ʱ���ɵ�
.AddInMemoryClients(ClientInitConfig.GetClients())//InMemory �ڴ�ģʽ
.AddInMemoryApiScopes(ClientInitConfig.GetApiScopes())//ָ��������
.AddInMemoryApiResources(ClientInitConfig.GetApiResources())//�ܷ���ɶ��Դ
.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();//�û���֤
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region �м��

app.UseStaticFiles();
app.UseIdentityServer();//ʹ������м������������
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