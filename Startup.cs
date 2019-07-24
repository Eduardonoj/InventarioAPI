using System;
using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using InventarioAPI.Models;

namespace InventarioAPI
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
            services.AddAutoMapper(options =>{
                options.CreateMap<CategoriaCreacionDTO, Categoria>(); // Esto lo hacemos para enlazar un DTO con una entidad
                options.CreateMap<TipoEmpaqueCreacionDTO, TipoEmpaque>();
                options.CreateMap<ProductoCreacionDTO, Producto>();
                options.CreateMap<ClienteCreacionDTO, Cliente>();
                options.CreateMap<ProveedorCreacionDTO, Proveedor>();
                options.CreateMap<InventarioCreacionDTO, Inventario>();
                options.CreateMap<EmailClienteCreacionDTO, EmailCliente>();
                options.CreateMap<EmailProveedorCreacionDTO, EmailProveedor>();
                options.CreateMap<TelefonoClienteCreacionDTO, TelefonoCliente>();
                options.CreateMap<TelefonoProveedorCreacionDTO, TelefonoProveedor>();
                options.CreateMap<CompraCreacionDTO, Compra>();
                options.CreateMap<DetalleCompraCreacionDTO, DetalleCompra>();
                options.CreateMap<FacturaCreacionDTO, Factura>();
                options.CreateMap<DetalleFacturaCreacionDTO, DetalleFactura>();

                //si ubiera que enlazar otros DTO con otra entidad se van colocando aqui...

            });

            
            services.AddDbContext<InventarioDBContext>(Options => 
            Options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddDbContext<InventarioIdentityContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("authtConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters
                = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience= false,
                    ValidateLifetime= false,
                    ValidateIssuerSigningKey= true,
                    IssuerSigningKey=
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                }) ;

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling
                =Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
