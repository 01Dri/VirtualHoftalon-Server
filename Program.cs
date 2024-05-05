using System.Net.Mime;
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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ModelsContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IPatientQueuesContextModels, PatientQueuesContextModels>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientsQueuesRepository, PatientsQueuesRepository>();
builder.Services.AddScoped<IPDFGenerator, PDFGeneratorService>();

builder.Services.AddScoped<IPatientBuilder, PatientBuilder>();
builder.Services.AddScoped<IAppointmentBuilder, AppointmentBuilder>();

builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientQueuesService, PatientQueuesService>();


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

