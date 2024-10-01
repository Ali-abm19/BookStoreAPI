using System.Text;
using System.Text.Json.Serialization;
using BookStore.Services.book;
using BookStore.src.Database;
using BookStore.src.Database;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Repository;
using BookStore.src.Services.book;
using BookStore.src.Services.cart;
using BookStore.src.Services.category;
using BookStore.src.Services.order;
using BookStore.src.Services.user;
using BookStore.src.Utils;
using BookStore.src.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);


// connect database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);

dataSourceBuilder.MapEnum<Role>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
});

//add autoo mapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

//ADD DI
builder
    .Services.AddScoped<IOrderServices, OrderServices>()
    .AddScoped<OrderRepository, OrderRepository>();

builder.Services.AddScoped<IBookService, BookService>().AddScoped<BookRepository, BookRepository>();
builder
    .Services.AddScoped<ICategoryService, CategoryService>()
    .AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserService, UserService>().AddScoped<UserRepository, UserRepository>();

// add auto-mapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// add DI services
builder
    .Services.AddScoped<ICartService, CartService>()
    .AddScoped<CartRepository, CartRepository>();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

//this is for creating a book. it throws an exception due to a possible cycle if it not included.
// if you want to try and handle it with it this line, go to BookService.CreateOneAsync()
builder
    .Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

// step 1: add controller
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// test if database is connected or not
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    try
    {
        // Check if the application can connect to the database
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// step 2: use
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
