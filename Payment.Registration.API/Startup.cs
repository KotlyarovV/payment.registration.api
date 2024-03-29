﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Payment.Registration.App.Builders;
using Payment.Registration.App.DTOs;
using Payment.Registration.App.Services;
using Payment.Registration.Domain.Models;
using Payment.Registration.Ef.DbContexts;
using Payment.Registration.Infrastructure;

namespace Payment.Registration.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            
            Configuration = builder.Build();
        }
        
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services
                .AddDbContext<PaymentFormDbContext>(
                    options => options
                        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services
                .AddSwaggerGen(c => {  
                    c.SwaggerDoc("v1", new OpenApiInfo {  
                        Version = "v1",  
                        Title = "Payment registration API"
                    });  
                })
                .AddScoped<IPaymentFormDataService, PaymentFormDataService>()
                .AddScoped<FileAppService>()
                .AddScoped<IBuilder<ApplicantSaveDto, Applicant>, ApplicantBuilder>()
                .AddScoped<IPaymentPositionUpdateBuilder, PaymentPositionUpdateBuilder>()
                .AddScoped<IMapper<PaymentFormUpdateDto, IReadOnlyCollection<PaymentPosition>, PaymentForm>, PaymentFormMapper>()
                .AddScoped<IBuilder<Applicant, ApplicantDto>, Builder<Applicant, ApplicantDto>>()
                .AddScoped<IBuilder<File, FileDto>, Builder<File, FileDto>>()
                .AddScoped<IBuilder<PaymentPosition, PaymentPositionDto>, PaymentPositionDtoBuilder>()
                .AddScoped<IBuilder<PaymentForm, PaymentFormDto>, PaymentFormDtoBuilder>()
                .AddScoped<IBuilder<FileSaveDto, File>, FileBuilder>()
                .AddScoped<IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition>, PaymentPositionBuilder>()
                .AddScoped<IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, int, PaymentForm>, PaymentFormBuilder>()
                .AddScoped<IFileStorageService, FileStorage.FileStorage>()
                .AddScoped<PaymentFormAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCors(b => b
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseSwagger()
                .UseSwaggerUI(swaggerUiOptions => swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment registration API"))
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}