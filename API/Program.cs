using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Data;
using RoomsReservations._1._Domain.Interfaces;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<HotelDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "DefaultPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            //Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<HotelDbContext>();
                DataSeeder.SeedData(context);
            }

            
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseCors("DefaultPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
      

            app.Run();
        }

        
    }
}