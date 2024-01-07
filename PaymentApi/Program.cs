using Services;
using Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace PaymentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPaymentDetails, PaymentsDetailDbService>();

            builder.Services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("PaymentDetail"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("PaymentApi");
                        sqlOptions.EnableRetryOnFailure();
                    });
            }, ServiceLifetime.Scoped);

            builder.Services.AddSwaggerGen(source =>
            {
                source.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Payment", Version = "v1.0" });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Payment v1");
                });
            }


            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
