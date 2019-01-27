using Application;
using Application.Interface;
using Application.Profile;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interface;
using Swashbuckle.AspNetCore.Swagger;
using IAuthenticationService = Application.Interface.IAuthenticationService;

namespace Carsales_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "User API", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationA.com"));
                options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationB.com"));
                options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationC.com"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API");
            });

            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
