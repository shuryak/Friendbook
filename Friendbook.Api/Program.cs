using FluentValidation;
using Friendbook.Api;
using Friendbook.Api.Configuration;
using Friendbook.Api.Helpers;
using Friendbook.Api.Helpers.Errors;
using Friendbook.Api.Helpers.Json;
using Friendbook.BusinessLogic;
using Friendbook.DataAccess.PostgreSql;
using Friendbook.DataAccess.PostgreSql.Repositories;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IFollowerPairRepository, FollowerPairRepository>();
builder.Services.AddTransient<IChatsRepository, ChatsRepository>();
builder.Services.AddTransient<IMessagesRepository, MessagesRepository>();

builder.Services.AddTransient<IUserProfileService, UserProfileService>();
builder.Services.AddTransient<IFollowersService, FollowersService>();
builder.Services.AddTransient<IMessagesService, MessagesService>();
    
builder.Services.AddTransient<IValidator<UserProfile>, UserProfileValidator>();

builder.Services.AddAutoMapper(typeof(DataAccessMappingProfile), typeof(DtoMappingProfile));
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    });
builder.Services
    .Configure<ApiBehaviorOptions>(x =>
    {
        x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<JwtConfiguration>()
    .Bind(configuration.GetSection("JwtConfiguration"));

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
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseJwtMiddleware();
app.MapControllers();

app.Run();
