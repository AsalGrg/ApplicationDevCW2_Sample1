using BisleriumPvtLtdBackendSample1;
using BisleriumPvtLtdBackendSample1.DbContext;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using BisleriumPvtLtdBackendSample1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using YourNamespace.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Enable CORS
builder.Services.AddCors();

// Add services to the container
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IConfigureOptions<SmtpClientOptions>, SmtpClientOptionsSetup>();

builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependency injection
builder.Services.AddDbContext<BisleriumBlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bisleriumBlogs1ConnectionString")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BisleriumBlogDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
    options.SignIn.RequireConfirmedEmail = true
);

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<CustomUserController>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<FileUploadService>();

var app = builder.Build();

// Enable CORS
app.UseCors(options =>
    options.WithOrigins("http://localhost:3006") // Specify the actual origin of your frontend
           .AllowAnyMethod() // Specify the allowed HTTP methods as needed
           .AllowAnyHeader()
);
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Procedure Service V1");
        c.RoutePrefix = "";
    });

    app.UseStaticFiles();

    app.UseRouting();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed default roles and user
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Blogger" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@admin.com";
    string password = "Test@1234";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                // Log or handle each error
                Console.WriteLine(error.Description);
            }
        }
    }
}

app.Run();
