using EntityFrameworkCore.Domain.Interfaces;
using EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add Entity Framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? "Data Source=.;Initial Catalog=EfCoreDemoDb;Integrated Security=True;TrustServerCertificate=True;"));

            // Add UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Services
            //builder.Services.AddScoped<IOrganizationRepository, StudentService>();
            //builder.Services.AddScoped<ICourseService, CourseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
