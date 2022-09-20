using JamesAPI; 
using JamesAPI.Auth;
using JamesAPI.Domian.Contracts;
using JamesAPI.Domian.Services;
using JamesAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ADD JWT
builder.Services.AddJwt(builder.Configuration);


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<JwtAuthManager>();
builder.Services.AddScoped<Db>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//added auth

app.UseAuthorization();

app.MapControllers();

app.Run();
