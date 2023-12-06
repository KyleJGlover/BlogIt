using BlogIt.Web.Data;
using BlogIt.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency injection into the SQL database
builder.Services.AddDbContext<BlogItDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogItDbConnectionString"))
);
// Dependency injection into the SQL database
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogItAuthDbConnectionString"))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();
// Limit the checks for the Password for the Identity object
builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
});

// Give the Tag Repository class object instead of the using just the Interface
builder.Services.AddScoped<ITagRepository, TagRepository>();
// Give the Blog Post Repository class object instead of the using just the Interface
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
// Give the Image Repository class object instead of the using just the Interface
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

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

// Authenticate before Authorizing
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
