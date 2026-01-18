
using BLL.MapesterConfigration;
using BLL.Services;
using DAL.Data;
using DAL.Models;
using DAL.Repository;
using DAL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace KASHPE.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddLocalization(options => options.ResourcesPath = "");

            builder.Services.AddDbContext<ApplicationDpContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                    new CultureInfo(defaultCulture),
                    new CultureInfo("ar")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider
                {
                    QueryStringKey = "lang"
                });

            });
            /*  builder.Services.AddScoped<ICategoryRepository,CategoryRepository>(); 
              builder.Services.AddScoped<ICategorySevices, CategoryService>();

              builder.Services.AddScoped<IAuthanication, Authanication>();
              builder.Services.AddTransient<IEmailSender, EmailSender>();
              builder.Services.AddScoped<ISeedData, RoleSeedData>();
              builder.Services.AddScoped<ISeedData, UserSeedDate>();*/ //session 12 part 2

            builder.Services.AddSwaggerGen();
            AppConfigration.config(builder.Services); //session 12 part 2
            MapesterConfig.MapesterConfiguration();
            
            builder.Services.AddIdentity<ApplicationUsers, IdentityRole>(

                Options => {
                    Options.Password.RequireDigit = true;
                    Options.Password.RequireLowercase = true;
                    Options.Password.RequireUppercase = true;
                    Options.Password.RequireNonAlphanumeric = true;
                    Options.Password.RequiredLength = 6;
                    Options.User.RequireUniqueEmail = true;
                    Options.Lockout.MaxFailedAccessAttempts = 5;
                    Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    Options.SignIn.RequireConfirmedEmail = true;

                }
                
                )
                .AddEntityFrameworkStores<ApplicationDpContext>()
                .AddDefaultTokenProviders();

         

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                    };
                });


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KASHOP API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,        
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            var app = builder.Build();

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseStaticFiles();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();




            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var seedDatas = services.GetServices<ISeedData>();

                    foreach (var seedData in seedDatas)
                    {
                        await seedData.DataSeed();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SEED ERROR: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                    throw; 
                }
            }

            app.Run();


        }
    }
}
