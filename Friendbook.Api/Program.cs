using FluentValidation;
using Friendbook.DataAccess.PostgreSql;
using Friendbook.DataAccess.PostgreSql.Repositories;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IFollowerPairRepository, FollowerPairRepository>();

builder.Services.AddTransient<IValidator<UserProfile>, UserProfileValidator>();

builder.Services.AddAutoMapper(typeof(DataAccessMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FriendbookDbContext>(x =>
    x.UseNpgsql(
        configuration.GetConnectionString("FriendbookContext")
        // b => b.MigrationsAssembly("Friendbook.Api")
    )
    );

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
