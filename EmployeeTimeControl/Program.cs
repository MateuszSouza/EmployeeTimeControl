using Core.Handlers.Login;
using Core.Handlers.TimerRegister;
using Domain.Token;
using EmployeeTimeControl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;
using Repository.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Services = builder.Services;
Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TimerRegisterCommandHandler).Assembly));

Services.AddControllers();
Services.AddScoped<TokenService>();
Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
Services.AddScoped<IEmployeeTimeRegister, EmployeeTimerRegister>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();

var key = Encoding.ASCII.GetBytes(Settings.TokenSecret);

Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("maneger"));
    options.AddPolicy("employee", policy => policy.RequireRole("employee"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x => x.
    AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();