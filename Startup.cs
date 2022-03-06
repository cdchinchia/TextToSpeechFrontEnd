using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Repositorio;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;

namespace TextToSpeechFrontEnd
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

            //Agregamos autenticación
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//Como se esta utilizando JWT este se guarda como cookie en el navegador, el token se genera cuando el usuario ingresa el usuario y contraseña correcta
                .AddCookie(options => {    //Añadiendo la cookie
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);//Tiempo de duración de la cookie(30)min en este caso
                    options.LoginPath = "/Home/Login"; //Ruta del Login
                    options.AccessDeniedPath = "/Home/AccessDenied"; //Ruta acceso denegado
                    options.SlidingExpiration = true;
                });

            services.AddControllersWithViews();

            //Se agrega esta línea para los llamados HTTP
            services.AddHttpClient();

            //Agregar las Interfaces y servicios como inyección de dependencias
            services.AddScoped<IAudioInfoRepositorio, AudioInfoRepositorio>();            
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IAccountRepositorio, AccountRepositorio>();
            //Agregando el accesor para funcionalidad de login y logout
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Parte de la Autenticacion-session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Damos soporte para CORS para que pueda tener conexion desde cualquier dominio
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            //Añadiendo funcionalidad de session y autenticación
            app.UseSession();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
