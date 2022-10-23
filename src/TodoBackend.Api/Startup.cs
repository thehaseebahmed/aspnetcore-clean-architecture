using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Starter.Infra;
using Starter.WebAPI.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Starter.Domain;
using Starter.Infra.Data;
using Starter.Application;

namespace Starter.WebAPI;

public class Startup
{
    private const string ApiTitle = "ASP.NET Core WebAPI Starter";
    private const string ApiVersion = "v1";
    private const string ApiDocsRoutePrefix = "";
    private const string AllowTodoBackendOrigin = "com_todobackend";

    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(ConfigureControllers);
        services.AddCors(ConfigureCors);

        services.AddApplicationInsightsTelemetry();
        services.AddSwaggerGen(ConfigureSwaggerGen);

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddApplication();
        services.AddInfrastructure();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            SeedDatabase(app);
        }

        app.UseHttpsRedirection();
        
        app.UseCors(AllowTodoBackendOrigin);

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"{ApiTitle} {ApiVersion}");
            c.RoutePrefix = ApiDocsRoutePrefix;
        });

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void ConfigureCors(CorsOptions options)
    {
        options.AddPolicy(
            AllowTodoBackendOrigin,
            policy =>
            {
                policy
                    // .WithOrigins("http://todobackend.com", "https://todobackend.com")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    }

    public void ConfigureControllers(MvcOptions options)
    {
        options.Filters.Add(typeof(GlobalExceptionFilter));
    }

    public void ConfigureSwaggerGen(SwaggerGenOptions options)
    {
        options.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiTitle, Version = ApiVersion });
    }

    public void SeedDatabase(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetService<ApiDbContext>();
        Seeder.Seed(context);
    }
}