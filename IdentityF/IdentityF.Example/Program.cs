using IdentityF.Core.Constants;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Core.Services.Db;
using IdentityF.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using YaMu.Helpers;

namespace IdentityF.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonOptionsDefault.Default.DefaultIgnoreCondition;
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddIdentityFServices(configure =>
            {
                configure.Token.LifetimeInMinutes = 120;
                configure.Endpoints[HttpActions.ConfirmAction].IsAvailable = false;
            });

            builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

            builder.Services.AddEndpointsApiExplorer();


            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityF();


            app.MapGet("/hello", (IDateTimeProvider dateTimeProvider) =>
            {
                return Results.Ok(new { date = dateTimeProvider.UtcNow, message = "Hello world!" });
            });

            app.MapGet("/init-db", async (IDatabaseService dbService) =>
            {
                var createdDb = await dbService.CreateDbAsync();

                var s = await dbService.SeedSystemAsync();

                return Results.Ok(s);
            });

            app.Run();
        }
    }
}
