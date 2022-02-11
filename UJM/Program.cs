using MASA.Utils.Caller.Core;
using UJM.Models.DataBase;
using UJM.Repositories;
using UJM.Services.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<UJMContext>();
builder.Services.AddTransient(typeof(IUJMRepository<>), typeof(UJMRepository<>));
builder.Services.AddSingleton<IUserServices,UserServices>();
builder.Services.AddScoped<I18n>();

//builder.Services.AddCaller();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaBlazor(builder =>
{
    builder.UseTheme(option =>
    {
        option.Primary = "#4318FF";
        option.Accent = "#4318FF";
    }
    );
});
builder.Services.AddGlobalForServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseGlobal();
app.UseMasaI18n();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();