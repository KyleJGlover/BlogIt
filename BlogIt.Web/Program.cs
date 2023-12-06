using BlogIt.Web.Data;
using BlogIt.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency injection into the SQL database
builder.Services.AddDbContext<BlogItDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggItDbConnectionString"))
);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
