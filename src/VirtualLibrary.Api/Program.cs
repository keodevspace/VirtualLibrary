using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Api.Data;
using VirtualLibrary.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar o Oracle DB
builder.Services.AddDbContext<VirtualLibraryContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<VirtualLibraryContext>()
    .AddDefaultTokenProviders();

// Configurar os serviços de email
builder.Services.AddSingleton<IEmailService, EmailService>();

// Adicionar serviços e configuração da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
