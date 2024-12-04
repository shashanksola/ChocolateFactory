using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChocolateFactory.Data;
using ChocolateFactory.Helpers;
using Scrutor;
using ChocolateFactory.Services;
using ChocolateFactory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add Swagger configuration for JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token. Example: \"Bearer abc123\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configure ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Authentication (JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Register all services in the ChocolateFactory.Services namespace
builder.Services.AddScoped<MaintenanceRecordRepository>();
builder.Services.AddScoped<MaintenanceService>();
builder.Services.AddScoped<FinishedGoodsRepository>();
builder.Services.AddScoped<PackagingService>();
builder.Services.AddScoped<ProductionScheduleRepository>();
builder.Services.AddScoped<ProductionService>();
builder.Services.AddScoped<RawMaterialRepository>();
builder.Services.AddScoped<RawMaterialService>();
builder.Services.AddScoped<RecipeRepository>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<ReportRepository>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<SalesOrderRepository>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<WarehouseRepository>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<QualityCheckRepository>();
builder.Services.AddScoped<QualityControlService>();

// Register helper classes
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<JwtHelper>();

// Register NotificationService manually due to custom initialization logic
builder.Services.AddScoped<NotificationService>(provider =>
{
    var smtpServer = builder.Configuration["NotificationSettings:SmtpServer"];
    var smtpPort = int.Parse(builder.Configuration["NotificationSettings:SmtpPort"]);
    var emailFrom = builder.Configuration["NotificationSettings:EmailFrom"];
    var emailPassword = builder.Configuration["NotificationSettings:EmailPassword"];

    return new NotificationService(smtpServer, smtpPort, emailFrom, emailPassword);
});

var app = builder.Build();

// Configure middleware for error handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var errorResponse = new { message = ex.Message, details = ex.StackTrace };
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => 
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
