using System.Collections.Generic;
using Data;
using GraphQL.Configuration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Configuration.Services
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Envoy API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Name = "Bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        In = ParameterLocation.Header
                    }, new List<string>() }
                });
            });

            services.AddPooledDbContextFactory<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"));
            });

            // services.AddMediatR(typeof(Upload.Handler).Assembly);
            // services.AddAutoMapper(typeof(Upload.Handler));

            // services.Configure<IdentityOptions>(options =>
            // {
            //     options.Password.RequiredLength = 6;
            //     options.User.RequireUniqueEmail = true;
            // });

            // var builder = services.AddIdentityCore<AppUser>();
            // var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            // identityBuilder.AddEntityFrameworkStores<DatabaseContext>();
            // identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            // var jwtOptions = configuration.GetSection("JWTOptions");

            // var accessTokenSecret = Convert.FromBase64String(jwtOptions["AccessTokenSecret"]);
            // var refreshTokenSecret = Convert.FromBase64String(jwtOptions["RefreshTokenSecret"]);
            // var accessTokenKey = new SymmetricSecurityKey(accessTokenSecret);
            // var refreshTokenKey = new SymmetricSecurityKey(refreshTokenSecret);

            // services.Configure<JWTOptions>(options =>
            // {
            //     int.TryParse(jwtOptions["AccessTokenExpire"], out var accessExpireMinutes);

            //     if (accessExpireMinutes > 0)
            //         options.AccessTokenValidFor = TimeSpan.FromMinutes(accessExpireMinutes);

            //     int.TryParse(jwtOptions["RefreshTokenExpire"], out var refreshExpireMinutes);

            //     if (refreshExpireMinutes > 0)
            //         options.RefreshTokenValidFor = TimeSpan.FromMinutes(refreshExpireMinutes);

            //     options.Issuer = jwtOptions["Issuer"];
            //     options.Audience = jwtOptions["Audience"];
            //     options.AccessTokenSecret = accessTokenSecret;
            //     options.RefreshTokenSecret = refreshTokenSecret;
            //     options.AccessSigningCredentials = new SigningCredentials(accessTokenKey, SecurityAlgorithms.HmacSha256Signature);
            //     options.RefreshSigningCredentials = new SigningCredentials(refreshTokenKey, SecurityAlgorithms.HmacSha256Signature);
            // });

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(configureOptions =>
            // {
            //     configureOptions.ClaimsIssuer = jwtOptions["Issuer"];
            //     configureOptions.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidIssuer = jwtOptions["Issuer"],
            //         ValidAudience = jwtOptions["Audience"],
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = accessTokenKey,
            //         RequireExpirationTime = true,
            //         ValidateLifetime = true,
            //         ClockSkew = TimeSpan.Zero
            //     };
            // });

            // services.AddScoped<IJWTService, JWTService>();
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IImageService, ImageService>();
            // services.AddScoped<IEmailService, EmailService>();
            // services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
        }
    }
}