using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieShop.Data;
using MovieShop.Services;
using MoviesShop.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShop.API
{

    public class Startup
    {
        // Sets the policy name to "_myAllowSpecificOrigins". The policy name is arbitrary.
        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)// dependcy injection, nijenct
        {
            services.AddControllers().AddNewtonsoftJson(option => { option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; }) ;
            //all dependence injection binding go here 
            //.net core has built-in dependency injection(DI)
            services.AddDbContext<MoviesShopDbcontext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MoviesShopDbcontext"));

            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSettings:PrivateKey"]))
                    };
                });
            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                                  });
            });
    //        services.AddControllers()
    //.AddNewtonsoftJson(options =>
    //{
    //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    //});
            services.AddScoped<IGenreRepositorycs, GenreRepository>();
            services.AddScoped<IGenreService,GenreService >();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICryptoService,CryptoService >();
            services.AddScoped<IUserRepository,UserRepository >();
            services.AddScoped<IUserService, UserService>();
       


        }

     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              // .net core has built-in develper exception page,which is develper fridenly.
            }
            if (env.IsProduction())
            { 
               // loh the exception with any log framework liek nlog 
               //send email notification 
               //.isstage is for the test enviroment
            }
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //);
            // .not core is cross-platform 
            //its open source and loosly coupled code and it is not ddependent 
            // it has built-in depandecy injection 
            // its easy to configure defferent  envirments 
            app.UseRouting();
            app.UseCors(_myAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
