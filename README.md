#  Product Catalog API

The **Product Catalog API** is a RESTful backend application built with **.NET 8**, following a **clean, layered architecture** (Domain, Application, Infrastructure, and API layers).  
It provides full **CRUD operations** for managing **Products** and **Users**, with **JWT authentication** and **role-based authorization** (Admin / User).

---

##  Project Overview

This API simulates a simple product management system with different user roles:

-  **Admin users** can perform all CRUD operations on products and manage users.
-  **Regular users** can only **view** products.
-  Passwords are securely stored using **BCrypt hashing**.
-  Uses **Entity Framework Core** with **MySQL** as the database provider.
-  Integrated **Swagger UI** for easy testing of all endpoints.

---

##  Technologies Used

-  **.NET 8**
-  **Entity Framework Core**
-  **MySQL** (`Pomelo.EntityFrameworkCore.MySql`)
-  **JWT Authentication** (`Microsoft.AspNetCore.Authentication.JwtBearer`)
-  **BCrypt.Net-Next** for password hashing
-  **Swagger / Swashbuckle**
-  **Dependency Injection**
-  **Async / Await architecture**

---

##  Getting Started

### 1️ Prerequisites

Make sure you have installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [MySQL Server](https://dev.mysql.com/downloads/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Optional: [Postman](https://www.postman.com/) for testing API requests

---

###  Clone the Repository

```bash
git clone https://github.com/Andrea2301/SM.git
cd ProductCatalog
```

---

###  Configure Database Connection

In `appsettings.json` (inside **ProductCatalog.Api**), update the connection string:

```json
"ConnectionStrings": {
  "Default": "server=localhost;database=productdb;user=root;password=yourpassword;"
},
"Jwt": {
  "Key": "your_secret_key_here",
  "Issuer": "yourapp",
  "Audience": "yourapp_users"
}
```

---

###  Apply Database Migrations

Run from the root directory:

```bash
dotnet ef database update --project ProductCatalog.Infrastructure --startup-project ProductCatalog.Api
```

This creates all required tables (**Users**, **Products**) in your MySQL database.

---

###  Run the Application

```bash
cd ProductCatalog.Api
dotnet run
```

Swagger UI will be available at:

 **https://localhost:7069/swagger**

---

##  Authentication & Roles

The API uses **JWT Bearer Authentication**.

| Role  | Permissions                              |
|--------|-------------------------------------------|
| Admin  | Full CRUD on Products and Users           |
| User   | Read-only access to Products              |

---

##  API Endpoints

###  Authentication (`/api/Auth`)

| Method | Endpoint              | Description           |
|---------|------------------------|-----------------------|
| POST    | `/api/Auth/register`   | Register a new user   |
| POST    | `/api/Auth/login`      | Login and receive JWT |
| POST    | `/api/Auth/refresh`    | Refresh a token       |

**Example – Login Response:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
```

---

###  Users (`/api/User`)

| Method | Endpoint | Description | Role |
|---------|-----------|--------------|------|
| GET | `/api/User/GetAll` | Get all users | Admin |
| GET | `/api/User/GetById/{id}` | Get user by ID | Admin |
| POST | `/api/User/Create` | Create new user | Public |
| PUT | `/api/User/Update/{id}` | Update user info | Admin |
| DELETE | `/api/User/Delete/{id}` | Delete user | Admin |

---

###  Products (`/api/Product`)

| Method | Endpoint | Description | Role |
|---------|-----------|--------------|------|
| GET | `/api/Product/GetAllProducts` | Get all products | Admin / User |
| GET | `/api/Product/GetById/{id}` | Get product by ID | Admin / User |
| POST | `/api/Product/Create` | Add a new product | Admin |
| PUT | `/api/Product/Update/{id}` | Update existing product | Admin |
| DELETE | `/api/Product/Delete/{id}` | Delete product | Admin |

---

## Using JWT in Swagger or Postman

1. Login with `/api/Auth/login` to get a JWT token.  
2. Copy the token (without quotes).  
3. In **Swagger UI**, click **Authorize** (lock icon).  
4. Enter:

```
Bearer your_token_here
```

Now you can access protected endpoints.

---

##  Example Request (Admin Creates a Product)

**POST →** `/api/Product/Create`

```json
{
  "name": "Wireless Mouse",
  "description": "Ergonomic Bluetooth mouse",
  "price": 29.99
}
```

**Header:**
```
Authorization: Bearer <your_admin_token>
```

---

##  Folder Purpose Summary

| Folder | Responsibility |
|---------|----------------|
| **Domain** | Core entities and repository interfaces. Defines the business model without dependencies. |
| **Application** | Business logic, DTOs, validation, and use cases. Connects domain with infrastructure. |
| **Infrastructure** | Database connection, EF Core context, and repository implementations. |
| **API** | Controllers, authentication, and configuration. Entry point of the application. |

---

##  Testing

You can test endpoints with:

- **Swagger UI** → `https://localhost:7069/swagger`
- **Postman** → Import the API and send JWT in the `Authorization` header.

Example:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI...
```

---


