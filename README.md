# Bookstore Backend Project 📚

## Project Overview
This is a backend solution for online bookstores. Built with .Net 8 and includes many functionalities that are considered initial for online stores. 

## Features ✨

- **User Management**:
  - Register new user
  - User authentication with JWT token
  - Role-based access control (Admin, Customer)
  - Update user info
  - Delete User
- **Book Management**:
  - Add, update and delete books
  - Pagination to get all books
  - Search functionality implemented (Search by Title, Search by Author)
- **Category Management**:
  - Add, update and delete catagories
  - Get all categoies
- **Cart & CartItmes Management**:
  - Add, update and delete carts
  - Get all the carts with nested CartItems
- **Order Management**:
  - Create orders using Cart
  - Update Order status
  - Get all Orders for Admin
  - Get Orders using Customer Token

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
|-- DTOs # Data Transfer Objects
|-- Entities # Database Entities (User, Book, Category, Cart, CartItems, Order)
|-- Middleware # Logging request, response and Error Handler
|-- Repositories # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Utils # Common logics
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
```

## API Endpoints

### User

- **POST** `/api/users/signUp` – Register a new user.
- **POST** `/api/users/signIn` – Login and get JWT token.

## Deployment

The application is deployed and can be accessed at: [https://sda-3-online-backend-teamwork-ywpj.onrender.com](https://sda-3-online-backend-teamwork-ywpj.onrender.com)

## Team iDevelopers Members  💻

**Lead**
- Manar (@ManarkhalidA)

**Members**  
- Ali (@Ali-abm19)
- Feda (@fedamousa)
- Haya (@HayaTamimi)
- Raghad (@xcviRaghad)


## License

This project is licensed under the MIT License.
