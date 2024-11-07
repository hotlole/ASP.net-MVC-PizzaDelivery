using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dodic.DAL;
using DoDic_pizza.Service;

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

// ���������� ������
builder.Services.AddSession();

// ���������� CartService � HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();

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

// ���������� �������������� � ������
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
