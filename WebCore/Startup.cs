using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using WebCore.ModelBinders;
using WebCore.Models;
using WebCore.Services;

namespace WebCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
            Debug.WriteLine($"DEBUG: {Configuration["Developer:Name"]}");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /************************************
                Esempio di Option pattern
            *************************************/
            services.AddOptions();

            //Configure Option using Extensions method  
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));

            //Configure Option using Extensions method  
            services.Configure<RemoteCredentials>(Configuration.GetSection("RemoteCredentials"));

            //services.AddSingleton<IConfiguration>(Configuration);

            // Aggiungo il servizio MusicAlbumRepository con lifetime Singleton
            services.AddSingleton(new MusicAlbumRepository());

            // Aggiungo il servizio MusicAlbumRepository con lifetime Transient
            //services.AddTransient(typeof(MusicAlbumRepository));

            // Add framework services.
            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.CookieHttpOnly = true;
            });

            var debug_string = Configuration["MasterKeyShare"];

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Configuration["MasterKeyShare"]));

            // CUSTOM MODEL BINDER
            services.AddMvc(config =>
                config.ModelBinderProviders.Insert(0, new StringArrayModelBinderProvider())
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            /************************************
            1)    Esempio di middleware custom
            *************************************/
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Sono l'unico custom Middleware");
            //});


            /************************************
            2)    Esempio di middleware che passa la chiamata al II middleware nella pipeline
            *************************************/
            //app.Use(async (context, next) =>
            //{
            //    // Fai qualche operazione.... es: loggin
            //    foreach (StringValues s in context.Request.Headers.Values)
            //    {
            //        Debug.WriteLine(s);    
            //    }

            //    // passa al 2° middleware della pipeline 
            //    await next.Invoke();
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Sono il II custom Middleware");
            //    // non chiamo il metodo next per cui ho finito la pipeline e torno indietro
            //});
            app.UseSession();

            Debug.WriteLine(Configuration["Developer:Name"]);
            Debug.WriteLine(Configuration["Developer:Surname"]);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Test: provare a commentare questa riga, si vedrà che i files statici non verranno serviti
            app.UseStaticFiles();

            // Cookie authentication: da mettere prima di UseMvc!
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    //AuthenticationScheme = "MyApplicationAuth",
            //    LoginPath = new PathString("/Account/Login/"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden/")
            //    //AutomaticAuthenticate = true,
            //   // AutomaticChallenge = true
            //});

            app.UseMvcWithDefaultRoute();

            /*******************************************
            -    Definizione di route (nuova sintassi)
            //*******************************************/
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}");
            //});


            /*************************************************
            -    Definizione di route (sintassi alternativa)
            *************************************************/
            //app.UseMvc(routes =>
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}")
            //);


            /***************************************
            -    Definizione di route (con vincoli)
            ***************************************/
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action}/{id?}",
            //        defaults: new {controller = "Home", action = "Index"},
            //        constraints: new {id = new IntRouteConstraint()});
            //});


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action}/{id?}",
            //        defaults: new { controller = "Home", action = "Index" },
            //        constraints: new
            //        {
            //            id = new CompositeRouteConstraint(
            //                new IRouteConstraint[]
            //                {
            //                    new AlphaRouteConstraint(),
            //                    new MinLengthRouteConstraint(6),
            //                })
            //        });
            //});

        }
    }
}
