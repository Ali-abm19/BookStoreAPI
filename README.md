# Bookstore Backend Project 📚

## Project Overview
This is a backend solution for online bookstores. Built with .Net 8 and includes many functionalities that are considered initial for online stores. 

## Features ✨
- **User Management**:
  - Register new user (Sign Up)
  - Sign In
  - User authentication with JWT token
  - User Password is hashed upon registering
  - Role-based access control (Admin, Customer)
  - Update user info
  - Delete User

- **Book Management**:
  - Add, update and delete books by Admin
  - Pagination to get all books (Limit & Offset) 
  - Search functionality implemented (Search by Title, Search by Author) 
  - Sort functionality implemented (By Price, By category)
  - Custom exception handling

- **Category Management**:
  - Add, update and delete catagories by Admin
  - Get all catagories
  - Custom exception handling


- **Cart Management**:
  - Add, update and delete carts by Admin
  - Get all the carts with nested CartItems by Admin
  - Custom exception handling


- **CartItmes Management**:
  - Add, update and delete cart
  - Get all carts by Admin
  - Custom exception handling


- **Order Management**:
  - Create orders using Cart
  - Update Order status
  - Get all Orders for Admin
  - Get Orders using Customer Token
  - Custom exception handling


## Technologies Used

- **.Net 8**: Web API Framework
- **Entity Framework Core**: ORM for database interactions
- **PostgreSQl**: Relational database for storing data
- **JWT**: For user authentication and authorization
- **AutoMapper**: For object mapping
- **Swagger**: API documentation

## Prerequisites

- .Net 8 SDK
- SQL Server
- VSCode

## Getting Started

### 1. Clone the repository:

```bash
git clone git@github.com:ManarkhalidA/sda-3-online-Backend_Teamwork.git
```

### 2. Setup database

- Make sure PostgreSQL Server is running
- Create `appsettings.json` file
- Update the connection string in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Local": "Server=localhost;Database=BookStoreDB;User Id=your_username;Password=your_password;"
  }
}
```

- Run migrations to create database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- Run the application

```bash
dotnet watch
```

The API will be available at: `http://localhost:5125`

### Swagger

- Navigate to `http://localhost:5125/swagger/index.html` to explore the API endpoints.

## Project structure

```bash
|-- Controllers: API controllers with request and response
|-- Database # DbContext and Database Configurations
|-- DTO # Data Transfer Objects
|-- Entity # Database Entities (User, Book, Category, Cart, CartItems, Order)
|-- Middlewares # Logging request, response and Error Handler
|-- Repository # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Utils # Customs Exception, Mapper Profile, Pagination Options, Password Utils, Token Utils
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
```

## API Endpoints
**Books**
  - `GET /api/v1/Books`
  - `POST /api/v1/Books`
  - `GET /api/v1/Books/{id}`
  - `DELETE /api/v1/Books/{id}`
  - `PUT /api/v1/Books/{id}`
  - `GET /api/v1/Books/category/{category}`

**CartItems**
  - `POST /api/v1/CartItems`
  - `GET /api/v1/CartItems`
  - `GET /api/v1/CartItems/{id}`
  - `DELETE /api/v1/CartItems/{id}`
  - `PUT /api/v1/CartItems/{id}`

**Carts**
  - `POST /api/v1/Carts`
  - `GET /api/v1/Carts`
  - `GET /api/v1/Carts/{id}`
  - `DELETE /api/v1/Carts/{id}`
  - `PUT /api/v1/Carts/{id}`

**Categories**
  - `POST /api/v1/Categories`
  - `GET /api/v1/Categories`
  - `GET /api/v1/Categories/{id}`
  - `PUT /api/v1/Categories/{id}`
  - `DELETE /api/v1/Categories/{id}`

**Orders**
  - `POST /api/v1/Orders`
  - `GET /api/v1/Orders`
  - `DELETE /api/v1/Orders/{id}`
  - `PUT /api/v1/Orders/{id}`
  - `GET /api/v1/Orders/userId/{id}`
  - `GET /api/v1/Orders/{orderId}`
  - `GET /api/v1/Orders/UserOrders`

**Users**
  - `GET /api/v1/Users`
  - `GET /api/v1/Users/{id}`
  - `PUT /api/v1/Users/{id}`
  - `DELETE /api/v1/Users/{id}`
  - `POST /api/v1/Users/signUp`
  - `POST /api/v1/Users/signIn`


## How to 📝
Endpoint adjustments in get `api/v1/Books` for Search functionality:
- title: `api/v1/Books?SearchByTitle=Algorithms%20in%20Depth`
- author : `api/v1/Books?SearchByAuthor=Jane%20Doe`

- price
  - high : `api/v1/Books?Limit=6&SortByPrice=High%20to%20low`
  - low : `api/v1/Books?Limit=6&SortByPrice=Low%20to%20high`

- limit : `api/v1/Books?Limit=3`
- offset : `api/v1/Books?Offset=2`
  
## Deployment

The application is deployed and can be accessed at: [https://sda-3-online-backend-teamwork-ywpj.onrender.com](https://sda-3-online-backend-teamwork-ywpj.onrender.com)

## Team iDevelopers Members  💻

**Lead:**
- Manar (@ManarkhalidA)

**Members:**  
- Ali (@Ali-abm19)
- Feda (@fedamousa)
- Haya (@HayaTamimi)
- Raghad (@xcviRaghad)


## License

This project is licensed under the MIT License.
