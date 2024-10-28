using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using CorporateWebProject.Persistence;
using UAParser;
using System.Text.Json;
using CorporateWebProject.Infrastructure;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Route;
using CorporateWebProject.WebUI.Models;
using CorporateWebProject.Persistence.Contexs;
using OfficeOpenXml;
using DinkToPdf.Contracts;
using DinkToPdf;
using jsreport.Binary;
using jsreport.Local;
using jsreport.AspNetCore;
using CorporateWebProject.WebUI.Models.Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.AddControllersWithViews()
//              .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBrowserDetection();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IServiceVM, ServiceVM>();
// Redis baðlantý dizesini appsettings.json'dan alýyoruz
var redisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value;



builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSingleton<Auth>();
builder.Services.AddSingleton<AuthHelper>();
builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
builder.Services.AddSingleton<RouteManager>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddJsReport(new LocalReporting()
        .UseBinary(JsReportBinary.GetBinary())
        .AsUtility()
        .Create());
builder.Services.AddCors();
builder.Services.AddMemoryCache();
var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    //app.UseExceptionHandler("/Error");
//    //app.UseDeveloperExceptionPage();

//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseStatusCodePages(async context =>
//{
//    switch (context.HttpContext.Response.StatusCode)
//    {
//        case 404:
//            await context.HttpContext.Response.WriteAsync("404 - Sayfa Bulunamadý");
//            break;
//        case 403:
//            await context.HttpContext.Response.WriteAsync("403 - Eriþim Engellendi");
//            break;
//        case 500:
//            await context.HttpContext.Response.WriteAsync("500 - Sunucu Hatasý");
//            break;
//    }
//});


//app.Use(async (context, next) =>
//{
//    try
//    {
//        if (context.Response.StatusCode == 404)
//        {
//            context.Request.Path = "/404";
//            await next();
//        }
//        else if (context.Response.StatusCode == 403)
//        {
//            context.Request.Path = "/403";
//            await next();
//        }
//        else if (context.Response.StatusCode == 500)
//        {

//            context.Request.Path = "/500/";
//            await next();
//        }
//        else
//        {

//            await next();
//        }
//    }
//    catch (Exception ex)
//    {

//        //context.Request.Path = "/manager/500/"+ex.Message;
//        await next();
//    }
//    //await next();

//});
var host = new WebHostBuilder().UseKestrel(options =>
{
    options.Limits.MaxRequestBufferSize = int.MaxValue;
    options.Limits.MaxRequestLineSize = int.MaxValue;
});
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );
app.UseEndpoints(endpoints =>
{
    //var pattern = "/{RouteValue}";
    //endpoints.MapDynamicControllerRoute<RouteManager>(pattern);
    endpoints.MapDynamicControllerRoute<RouteManager>("{RouteValue}");


    endpoints.MapGet("/admin", context =>
    {
        context.Response.Redirect("/manager/login", permanent: false);
        return Task.CompletedTask;
    });
    endpoints.MapGet("/manager", context =>
    {
        context.Response.Redirect("/manager/login", permanent: false);
        return Task.CompletedTask;
    });

    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=main}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
       "manager",
       "manager",
        pattern: "{area:exists}/{controller=login}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
   "client",
   "client",
    pattern: "{area:exists}/{controller=login}/{action=Index}/{id?}");


});

app.Run();
