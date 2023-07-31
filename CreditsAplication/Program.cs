using CreditsAplication.Api.Interface.Facades;
using CreditsAplication.Api.Interface.Helpers;
using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Business.Facades;
using CreditsAplication.Business.Helpers.Authentication;
using CreditsAplication.Business.Services;
using CreditsAplication.Configs.Middleware;
using CreditsAplication.DatabaseContext;
using CreditsAplication.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);

//Add Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Dependency injection
//Facades
builder.Services.AddScoped<IClientAccountFacade, ClientAccountFacade>();
// Helpers
builder.Services.AddScoped<IPasswordHasherHelper, PasswordHasherHelper>();
//Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
//Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ExceptionMiddleware, ExceptionMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
