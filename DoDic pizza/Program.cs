using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dodic.DAL;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ � ���������
builder.Services.AddControllersWithViews();

// ��������� ��������� ���� ������
builder.Services.AddDbContext<ApplicationContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �������������� � �������������� cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // ������� ���� � �������� �����
        options.LogoutPath = "/Account/Logout"; // ������� ���� � �������� ������
    });

var app = builder.Build();

// ��������� HTTP-��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ���������� ��������������
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
