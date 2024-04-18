using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services;
using VirtualHoftalon_Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// builder.Services.AddControllers().AddJsonOptions(x =>
    // x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
// );
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelsContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientBuilder, PatientBuilder>();


builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();


var app = builder.Build();
app.UseErrorHandlerMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

