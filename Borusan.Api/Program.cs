using AutoMapper;
using Borusan.Api.Extensions;
using Borusan.Api.Middleware;
using Borusan.Data;
using Borusan.Interface;
using Borusan.Repository;
using Borusan.Repository.Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
#region controller json fluentVal service
builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
	.AddFluentValidation();
#endregion
#region dbcontext 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});
#endregion

#region logger service
//var logger = new LoggerConfiguration()
//  .WriteTo.MongoDB("", "log")
//  .CreateLogger();

builder.Host.UseSerilog((ctx,config)=>config.ReadFrom.Configuration(ctx.Configuration));
#endregion
#region swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion
#region automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var mapperConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region dependencies
builder.Services.AddTransient<MyExceptionMiddleware>();
builder.Services.AddScoped<IValidator<MaterialDTO>, MaterialValidator>();
builder.Services.AddScoped<IValidator<OrderDTO>, OrderValidator>();

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

builder.Services.AddCors(opt => opt.AddPolicy("allow", builder =>
{
	builder.AllowAnyOrigin();
	builder.AllowAnyMethod();
	builder.AllowAnyHeader();

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionMiddleware();
app.UseCors("allow");
DataSeed.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();

