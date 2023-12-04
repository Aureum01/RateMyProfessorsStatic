using RateMyProfessorsStatic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using RateMyProfessorsStatic.Models;

namespace RateMyProfessors
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddSingleton<JsonFileProfessorService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                endpoints.MapGet("/professors", (context) =>
                {
                    var professorService = app.ApplicationServices.GetService<JsonFileProfessorService>();
                    if (professorService == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        return context.Response.WriteAsync("Professor service is not available.");
                    }

                    var professors = professorService.GetProfessors();
                    var json = JsonSerializer.Serialize(professors);
                    return context.Response.WriteAsync(json);
                });

            });
        }
    }
}
