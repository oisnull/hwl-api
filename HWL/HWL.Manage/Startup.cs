using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWL.Entity.Models;
using HWL.ShareConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HWL.Manage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ShareConfig.LogHelper.InitConfigure("mgr");
            ShareConfig.LogHelper.Debug($"environment:{ShareConfig.ShareConfiguration.CurrentEnvironment}");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDistributedMemoryCache();
            services.AddDistributedRedisCache(option =>
            {
                option.InstanceName = RedisConfigManager.HWLManageSessionRedisInstance;
                option.Configuration = RedisConfigManager.HWLManageSessionRedisHosts;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(RedisConfigManager.HWLManageSessionTimeOut);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<HWLEntities>(options => options.UseSqlServer(ShareConfiguration.DBConnectionString, b => b.UseRowNumberForPaging()));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    //app.UseExceptionHandler("/Home/Error");
            //    app.UseHsts();
            //}

            Models.AppHttpContext.Services = app.ApplicationServices;

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Default}/{id?}");
            });
        }
    }
}
