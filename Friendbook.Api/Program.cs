using FluentValidation;
using Friendbook.Api;
using Friendbook.Api.Configuration;
using Friendbook.Api.Helpers;
using Friendbook.Api.Helpers.Errors;
using Friendbook.Api.Helpers.Json;
using Friendbook.Api.Hubs;
using Friendbook.BusinessLogic;
using Friendbook.DataAccess.PostgreSql;
using Friendbook.DataAccess.PostgreSql.Repositories;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Http.Connections;
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

builder.Services.AddCors();

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

builder.Services.AddSignalR();

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

// app.UseHttpsRedirection();
app.UseJwtMiddleware();

app.UseRouting();

app.UseAuthorization();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("https://gourav-d.github.io"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessagesHub>("/messages", options =>
    {
        options.Transports = HttpTransportType.WebSockets;
    });
});

if (app.Environment.IsDevelopment())
{
    await app.StartAsync();

    foreach (string url in app.Urls)
        app.Logger.LogInformation($"Swagger on {url}/swagger");
    
    await app.WaitForShutdownAsync();
}
else
{
    app.Run();
}
