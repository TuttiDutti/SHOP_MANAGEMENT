using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ShopManagement.DAL;
using ShopManagement.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using IronPdf.Extensions.Mvc.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<AbsenceService>();
builder.Services.AddScoped<AbsenceRepository>();
builder.Services.AddScoped<AbsenceTypeService>();
builder.Services.AddScoped<AbsenceTypeRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<DocumentTypeRepository>();
builder.Services.AddScoped<DocumentTypeService>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<PhotoRepository>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<AnnouncementRepositiory>();
builder.Services.AddScoped<AnnouncementService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConectionString")));

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("pl-PL"),

    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.AddSingleton<IRazorViewRenderer, RazorViewRenderer>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/";
                options.LoginPath = "/User/Login";
            });


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
