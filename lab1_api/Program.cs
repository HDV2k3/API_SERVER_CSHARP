using lab1_api.AutoMapper;
using lab1_api.Data;
using lab1_api.Facade;
using lab1_api.Interfaces;
using lab1_api.Middleware;
using lab1_api.Models.Domain;
using lab1_api.Repositories;
using lab1_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API documentation using Swagger"
    });
});
// đăng ký kết nối database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        }));
// đăng ký dịch vụ logic tương tác
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserFacade>();
// Đăng ký AutoMapper với Profile của bạn
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();
// Đảm bảo khởi tạo cơ sở dữ liệu và dữ liệu mẫu
// Đảm bảo khởi tạo cơ sở dữ liệu và dữ liệu mẫu khi ứng dụng bắt đầu
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Kiểm tra xem cơ sở dữ liệu đã được tạo chưa, nếu chưa thì tạo mới
    context.Database.Migrate();

    // Chèn dữ liệu mẫu vào cơ sở dữ liệu nếu chưa có dữ liệu
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { Name = "Huỳnh Đắc Việt", Email = "dacviethuynh@gmail.com", Password = "admin", Avatar = "null" },
            new User { Name = "Dev Backend", Email = "devjavahcmc@gmail.com", Password = "admin", Avatar = "null" }
        );
        context.SaveChanges();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "v1/swagger/{documentname}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/v1/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = "swagger";
    });

}
// đăng ký xử lý lỗi 
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
