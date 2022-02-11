using System.Net;
using UJM.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")//scheme--��ʾͨ��Bearer��ʽ�������û���Ϣ
     .AddIdentityServerAuthentication(options =>
     {
         options.Authority = "http://172.16.32.169:49155";//ids4�ĵ�ַ--ר�Ż�ȡ��Կ
         options.ApiName = "UserApi";
         options.RequireHttpsMetadata = false;
     });//����ids4

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
//��ʼ��һ��
app.Configuration.ConsulRegist();
app.Run();
