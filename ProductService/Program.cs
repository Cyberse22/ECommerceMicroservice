using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ProductService.Data.DbContext;
using ProductService.Data.MongoDB;
using ProductService.Helpers;
using ProductService.Repository;
using ProductService.Repository.Impl;
using ProductService.Service;
using ProductService.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
