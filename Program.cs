using System.Text.Json;
using bookTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Carts
List<Cart> carts = new List<Cart>
{
    new Cart
    {
        CartId = 1,
        UserId = 3,
        Quantity = 3,
        Price = 100,
    },
    new Cart
    {
        CartId = 2,
        UserId = 2,
        Quantity = 5,
        Price = 250,
    },
};

app.MapPost(
    "/api/v1/carts",
    (Cart newCart) =>
    {
        carts.Add(newCart);
        return Results.Created("New cart", newCart);
    }
);

app.MapGet(
    "/api/v1/carts/{id}",
    (int id) =>
    {
        var cart = carts.FirstOrDefault(p => p.CartId == id);
        return Results.Ok(cart);
    }
);//?
// Users
List<User> users = new List<User>
{
    new User
    {
        Id = 1,
        Name = "Mohammed",
        Address = "Dammam",
        Phone = 0593939393,
        Email = "xxx@gmail.com",
        Password = "azaz",
    },
    new User
    {
        Id = 2,
        Name = "Aya",
        Address = "Jeddah",
        Phone = 0574747474,
        Email = "yyy@gmail.com",
        Password = "trtr",
    },
    new User
    {
        Id = 3,
        Name = "Sara",
        Address = "Ryadh",
        Phone = 0565656565,
        Email = "zzz@gmail.com",
        Password = "jgjh",
    },
};

//POST - create new user

app.MapPost(
    "/api/v1/users",
    (User newUser) =>
    {
        users.Add(newUser);
        return Results.Created("new user", newUser);
    }
);

//GET - get user by User_Id
app.MapGet(
    "/api/v1/users/{id}",
    (int id) =>
    {
        User? foundUser = users.FirstOrDefault(u => u.Id == id);

        if (foundUser == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(foundUser);
    }
);

//GET - get all users
// app.MapGet(
//     "/api/v1/users",
//     () =>
//     {
//         return Results.Ok(users);
//     }
// );
app.MapGet("/api/v1/users", () => Results.Ok(users));
//PUT - update user Name by User_Id

app.MapPut(
    "/api/v1/users/{id}",
    (int id, User updatedUser) =>
    {
        User? foundUser = users.FirstOrDefault(u => u.Id == id);
        if (foundUser == null)
        {
            return Results.NotFound();
        }
        foundUser.Name = updatedUser.Name;
        return Results.Ok(foundUser);//        return Results.NotFound();>manar

    }
);

//PUT - update user Address&phone&email
app.MapPatch(
    "/api/v1/users/{id}",
    (int id, JsonElement updatedFields) =>
    {
        User? foundUser = users.FirstOrDefault(u => u.Id == id);
        if (foundUser == null)
        {
            return Results.NotFound();
        }

        // Check if the Address property is present and update it
        if (updatedFields.TryGetProperty("Address", out var addressUpdated) && addressUpdated.ValueKind == JsonValueKind.String)
        {
            foundUser.Address = addressUpdated.GetString();
        }

        // Check if the Phone property is present and update it
        if (updatedFields.TryGetProperty("Phone", out var phoneUpdated) && phoneUpdated.ValueKind == JsonValueKind.Number)
        {
            foundUser.Phone = phoneUpdated.GetInt32();
        }

        // Check if the Email property is present and update it
        if (updatedFields.TryGetProperty("Email", out var emailUpdated) && emailUpdated.ValueKind == JsonValueKind.String)
        {
            foundUser.Email = emailUpdated.GetString();
        }

        return Results.Ok(foundUser);
    }
);
// //PUT - update user Address*******
// app.MapPatch(
//     "/api/v1/users/{id}",
//     (int id, JsonElement updatedFields) =>
//     {
//         User? foundUser = users.FirstOrDefault(u => u.Id == id);
//         if (foundUser == null)
//         {
//             return Results.NotFound();
//         }
//         if (updatedFields.TryGetProperty("Address", out var addressUpdated))
//         //        if (updatedFields.TryGetProperty("Adress", out var addressUpdated))>manar

//         {
//             foundUser.Address = addressUpdated.GetString();
//         }
//         return Results.Ok(foundUser);
//     }
// );

// //PUT - update user phone*******
// app.MapPatch(
//     "/api/v1/users/{id}",
//     (int id, JsonElement updatedFields) =>
//     {
//         User? foundUser = users.FirstOrDefault(u => u.Id == id);
//         if (foundUser == null)
//         {
//             return Results.NotFound();
//         }
//         if (updatedFields.TryGetProperty("Phone", out var phoneUpdated))
//         {
//             foundUser.Phone = phoneUpdated.GetInt32();
//         }
//         return Results.Ok(foundUser);
//     }
// );

// //PUT - update user email******
// app.MapPatch(
//     "/api/v1/users/{id}",
//     (int id, JsonElement updatedFields) =>
//     {
//         User? foundUser = users.FirstOrDefault(u => u.Id == id);
//         if (foundUser == null)
//         {
//             return Results.NotFound();
//         }
//         if (updatedFields.TryGetProperty("Email", out var emailUpdated))
//         {
//             foundUser.Email = emailUpdated.GetString();
//         }
//         return Results.Ok(foundUser);
//     }
// );

//DELETE - delete a user by User_Id

app.MapDelete(
    "/api/v1/users/{id}",
    (int id) =>
    {
        User? foundUser = users.FirstOrDefault(u => u.Id == id);
        if (foundUser == null)
        {
            return Results.NotFound();
        }
        users.Remove(foundUser);
        return Results.NoContent();
    }
);

app.MapDelete(
    "/api/v1/carts/{id}",
    (int id) =>
    {
        var cart = carts.FirstOrDefault(p => p.CartId == id);
        if (cart == null)
        {
            return Results.NotFound(); //404
        }
        carts.Remove(cart);
        return Results.NoContent(); //204
    }
);
List<Book> booksList = new List<Book>
{
    new Book(1, "Yellowface", "R.F. Kuang", "9780063250833", 5, 15.81f, Book.Format.hardcover),
    new Book(2, "Weyward", "Emilia Hart", "9781250280800", 3, 13.76f, Book.Format.paperback),
    new Book(3, "The Hunger Games", "Suzanne Collins", "0439023483", 3, 12.79f, Book.Format.hardcover),
    new Book(4, "Catching Fire", "Suzanne Collins", "0439023491", 15, 14.99f, Book.Format.audio),
   // new Book(5, "The Hunger Games", "Suzanne Collins", "1338334921", 15, 44.99f, Book.Format.audio)
};


app.MapGet(
    "/api/v1/books",
    () =>
    {
        return Results.Ok(booksList);
    }
);

app.MapGet(
    "/api/v1/book/{id}",
    (int id) =>
    {
        return Results.Ok(booksList.FirstOrDefault(x => x.Id == id));
    }
);

app.MapPost(
    "/api/v1/book",
    (Book b) =>
    {
        booksList.Add(b);
        return Results.Created($"{b.Title} was added", b);
    }
);

app.MapPut(
    "/api/v1/book/{id}",
    (int id, Book updateB) =>
    {
        Book? book = booksList.FirstOrDefault(i => i.Id == id);
        if (book == null)
        {
            return Results.NotFound();
        }
        book.Id = updateB.Id;
        book.Title = updateB.Title;
        book.Author = updateB.Author;
        book.Price = updateB.Price;
        book.StockQuantity = updateB.StockQuantity;
        book.ISBN = updateB.ISBN;
        book.Format0 = updateB.Format0;
        return Results.Ok(book);
    }
);

app.MapDelete(
    "api/v1/book/{id}",
    (int id) =>
    {
        Book? book = booksList.FirstOrDefault(i => i.Id == id);
        if (!booksList.Any(x => x.Id == id))
        {
            return Results.NotFound();
        }
        booksList.Remove(book);

        return Results.NoContent();
    }
);
//Order 
List<Order> orders = new List<Order>();
// POST - Create New Order>Under trial
app.MapPost("/api/v1/orders", (Order newOrder) =>
{
    newOrder.OrderId = orders.Count + 1;
    newOrder.DateCreated = DateTime.Now;
    newOrder.OrderStatus = Order.Status.Pending;
    orders.Add(newOrder);
    return Results.Created($"/api/v1/orders/{newOrder.OrderId}", newOrder);
});

// GET - Get Order by OrderId>Under trial
app.MapGet("/api/v1/orders/{orderId}", (int orderId) =>
{
    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
    return order == null ? Results.NotFound() : Results.Ok(order);
});

// GET - Get All Orders>Under trial
app.MapGet("/api/v1/orders", () => Results.Ok(orders));
// DELETE - Cancel Order>Under trial
app.MapDelete("/api/v1/orders/{orderId}", (int orderId) =>
{
    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
    if (order == null) return Results.NotFound();
    orders.Remove(order);
    return Results.NoContent();
});
// PUT - Update Order Status>Under trial
app.MapPut("/api/v1/orders/{orderId}", (int orderId, Order.Status status) =>
{
    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
    if (order == null) return Results.NotFound();
    order.OrderStatus = status;
    return Results.Ok(order);
});
app.Run();

