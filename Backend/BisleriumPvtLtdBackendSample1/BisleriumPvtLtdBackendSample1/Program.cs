using BisleriumPvtLtdBackendSample1;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using BisleriumPvtLtdBackendSample1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependencyInjection
builder.Services.AddDbContext<BisleriumBlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("bisleriumBlogs1ConnectionString")));

//services dependencyInjection
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICommentService, CommentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
