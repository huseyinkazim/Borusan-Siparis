using AutoMapper;
using Borusan.Api.Extensions;
using Borusan.Api.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//	options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
//});
#endregion
#region jwt token service
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
		   .AddJwtBearer(options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Keys:UserAuthSecretKey"])),
				   ValidIssuer = builder.Configuration["Keys:Issue"],
				   ValidAudience = builder.Configuration["Keys:Audience"],
				   ValidateLifetime = true
			   };
		   });
#endregion
#region logger service
//var logger = new LoggerConfiguration()
//  .WriteTo.MongoDB("databaseUrl")
//  .CreateLogger();

builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);
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
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

