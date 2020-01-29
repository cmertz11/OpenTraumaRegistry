using Blazored.SessionStorage;
using EmbeddedBlazorContent;
using MatBlazor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTraumaRegistry.Shared;

namespace OpenTraumaRegistry.UI.MD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var EmailProvider = Configuration.GetSection("EmailSettings")["Provider"];
            switch (EmailProvider)
            {
                case "SENDGRID":
                    services.AddScoped<IEmailHelper, _SendGrid>();
                    break;
                default:
                    break;
            }
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            }); 
            services.AddBlazoredSessionStorage();     
            services.AddScoped<AuthenticationStateProvider, _otrAuthenticationStateProvider>();
            services.AddAuthorization(config =>
            {
                config.AddPolicy("SystemAdminstrator", policy =>
                policy.Requirements.Add(new _otrSystemRoleRequirement("SystemAdministrator")));

                config.AddPolicy("Trauma Registrar", policy =>
                policy.Requirements.Add(new _otrSystemRoleRequirement("SystemAdministrator")));
            });

            services.AddSingleton<IAuthorizationHandler, _otrAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEmbeddedBlazorContent(typeof(MatBlazor.BaseMatComponent).Assembly);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
