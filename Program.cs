using Microsoft.EntityFrameworkCore;
using Api.Infrastructure.Data;
using Api.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtKey = "4d8f9a1c2e7b4f6a9d3e8c1b7a5f2e9d";//Correto eh colocar no Apppsettings.json

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
   options.TokenValidationParameters = new TokenValidationParameters
   {
     ValidateIssuer = false,
     ValidateAudience = false,
     ValidateLifetime = true,
     ValidateIssuerSigningKey = true,
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),  
   };
}); 

builder.Services.AddDbContext<AppDbContext>(Options => Options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection" //mostra onde está a string de conexão no appsettings.json
)));
builder.Services.AddScoped<UserService>(); //Permite injetar o service no controller
builder.Services.AddScoped<TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //Cria o usuario
app.UseAuthorization(); //Verifica

app.MapControllers();
app.Run();

//Para testar sem postman: http://localhost:5194/swagger/index.html