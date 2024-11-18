using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Text.Json;
using UserService.Data;
using UserService.Repositories;
using UserService.Repositories.Impl;
using UserService.Services;
using UserService.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql(connectionString)
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddHttpContextAccessor();

// Dependency Injection: AddScoped, AddTransient, AddSingleTon
builder.Services.AddScoped<IAccountRepository, AccountRepositoryImpl>();
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("UserService", new OpenApiInfo { Title = "User Service API", Version = "v1" });
    option.SwaggerDoc("ProductService", new OpenApiInfo { Title = "Product Service API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    // Lọc controller theo tên namespace để thêm vào tài liệu tương ứng
    option.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (docName == "UserService")
        {
            return apiDesc.ActionDescriptor.RouteValues["controller"]?.Contains("Account") == true;
        }
        else if (docName == "ProductService")
        {
            return apiDesc.ActionDescriptor.RouteValues["controller"]?.Contains("Product") == true;
        }
        return false;
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();
// Jwt, Authentication and Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        if (builder.Configuration["EF_MIGRATE"] == "true")
        {
            Console.WriteLine("Migrates");
            var context = services.GetRequiredService<UserDbContext>();
            context.Database.Migrate();
            await Seed.SeedDataAsync(userManager, roleManager);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding data: {ex.Message}");
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/UserService/swagger.json", "User Service API");
        options.SwaggerEndpoint("/swagger/ProductService/swagger.json", "Product Service API");
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
