using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PcPartPicker.IService;
using PcPartPicker.Service;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<IBuildService, BuildService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<ICPUCoolerService, CPUCoolerService>();
builder.Services.AddScoped<ICPUService, CPUService>();
builder.Services.AddScoped<IGPUService, GPUService>();
builder.Services.AddScoped<IMemoryService, MemoryService>();
builder.Services.AddScoped<IMotherboardService, MotherboardService>();
builder.Services.AddScoped<IPSUService, PSUService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAyNTUzMUAzMjMwMmUzMzJlMzBqdnZ6cCtWUTJLQ3gyRit4Y0hRaFdtUVJ4VmdaU2R2Z0xkRmNaVUtZdHNFPQ==");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
