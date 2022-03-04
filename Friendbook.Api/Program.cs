using System.Text;
using FluentValidation;
using Friendbook.Api;
using Friendbook.Api.Helpers.Errors;
using Friendbook.Api.Helpers.Json;
using Friendbook.Api.Hubs;
using Friendbook.BusinessLogic;
using Friendbook.DataAccess.PostgreSql;
using Friendbook.DataAccess.PostgreSql.Repositories;
using Friendbook.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Friendbook.Api.Middleware;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IFollowerPairRepository, FollowerPairRepository>();
builder.Services.AddTransient<IUserSessionRepository, UserSessionRepository>();
builder.Services.AddTransient<IChatsRepository, ChatsRepository>();
builder.Services.AddTransient<IMessagesRepository, MessagesRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFollowersService, FollowersService>();
builder.Services.AddTransient<IUserSessionService, UserSessionService>();
builder.Services.AddTransient<IMessagesService, MessagesService>();
    
builder.Services.AddTransient<IValidator<User>, UserValidator>();

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["JwtConfiguration:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["JwtConfiguration:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfiguration:Secret"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "**Access token** (_JWT_)",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddSignalR();

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

app.UseRouting();

app.StatusCodeMiddleware();
app.UseAuthentication();
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
