using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductProvider.Contexts;
using ProductProvider.Models;
using ProductProvider.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<ProductService>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudiences = builder.Configuration.GetSection("Jwt:Audiences").Get<IEnumerable<string>>(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };
});

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
builder.Services.AddAuthorizationBuilder().AddPolicy("admin_access", policy => policy.RequireClaim("scope", "admin_access_products").RequireRole("admin"));
builder.Services.AddAuthorizationBuilder().AddPolicy("read_access", policy => policy.RequireClaim("scope", "read_access_products"));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


app.MapPost("/api/products", async (ProductCreateRequest request, ProductService productService) =>
{    
    if (string.IsNullOrEmpty(request.ArticleNumber) || string.IsNullOrEmpty(request.Title))
        return Results.BadRequest();

    // logik för att skapa en produkt
    await productService.CreateProductAsync(request);
    return Results.Ok();
})
.RequireAuthorization("admin_access");

app.MapGet("/api/products", async (ProductService productService) =>
{
    // logik för att skapa en produkt
    var products = await productService.GetAllProductsAsync();
    return Results.Ok(products);
})
.RequireAuthorization("read_access"); ;

app.Run();
