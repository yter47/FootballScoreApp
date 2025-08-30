using FluentValidation;
using FootballScoreApp.DbConnection;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories;
using FootballScoreApp.Repositories.IRepositories;
using FootballScoreApp.Services;
using FootballScoreApp.Services.IServices;
using FootballScoreApp.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FootballApp")));

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<ITeamService, TeamService>();

//Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserTeamRepository, UserTeamRepository>();

//Providers
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddHttpClient<TeamService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
//builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new ValidationProblemDetails(errors));
        }
    });
});

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
