using IdentityF.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

            builder.Services.AddIdentityFServices();

            builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(jwt =>
                        {
                            jwt.RequireHttpsMetadata = false;
                            jwt.SaveToken = true;
                            jwt.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidIssuer = builder.Configuration["Token:Issuer"],
                                ValidateAudience = true,
                                ValidAudience = builder.Configuration["Token:Audience"],
                                ValidateLifetime = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Token:SecretKey"]!)),
                                ValidateIssuerSigningKey = true,
                            };
                        });



            var app = builder.Build();



            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityF();

            app.MapGet("/hello", (IDateTimeProvider dateTimeProvider) =>
            {
                return Results.Ok(new { date = dateTimeProvider.UtcNow, message = "Hello world!" });
            });

            app.Run();
        }
    }
}
