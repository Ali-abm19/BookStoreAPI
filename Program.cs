using BookStore.Services.book;
using BookStore.src.Database;
using BookStore.src.Repository;
using BookStore.src.Services.book;
using BookStore.src.Services.category;
using BookStore.src.Services.order;
using BookStore.src.Utils;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// connect database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);

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
builder.Services.AddScoped<ICategoryService, CategoryService>().AddScoped<CategoryRepository, CategoryRepository>();

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
