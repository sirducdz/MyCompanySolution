using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyCompany.Api.Middleware;
using MyCompany.Application.Exceptions;
using MyCompany.Domain.Interfaces;
using MyCompany.Infrastructure.DbContexts;
using MyCompany.Infrastructure.Persistence.Repositories;
using System.Net;

namespace MyCompany.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        // Quan trọng: Chỉ định assembly chứa migrations
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            var applicationAssembly = typeof(MyCompany.Application.Mappings.MappingProfile).Assembly;
            builder.Services.AddAutoMapper(applicationAssembly);
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is NotFoundException) // Bắt NotFoundException
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Đặt mã 404
                        }
                        // Có thể thêm các loại exception khác ở đây (BadRequestException, ValidationException...)
                        // else if (contextFeature.Error is ValidationException validationEx)
                        // {
                        //     context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        //     // Serialize lỗi validation nếu cần
                        // }

                        // Trả về thông báo lỗi (có thể là một cấu trúc JSON chuẩn)
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
