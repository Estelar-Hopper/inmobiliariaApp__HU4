#  APPLICATION TO MANAGE REAL ESTATE GOODS

**InmobiliariaApp** is a RESTful backend application built with **.NET 8**, following a **clean, layered architecture** (Domain, Application, Infrastructure, and API layers).  
It provides full **CRUD operations** for managing **Property** and **Users**, with **JWT authentication** and **role-based authorization** (Admin / Client).
This app works throught two parts, backend (.NET) and frontend (React).

Whe had created two deployments, one for back and another one for front.

Backend link: https://inmobiliaria-app-hu4-598dd1cade22.herokuapp.com/

Frontend link: https://inmobiliariaapphu4-front-production.up.railway.app/

Another useful resources of this project are:
1) Backend repo: https://github.com/Estelar-Hopper/inmobiliariaApp__HU4.git
2) Frontend repo: https://github.com/Estelar-Hopper/inmobiliariaApp__HU4-Front.git
3) Scrum board: https://dev.azure.com/riwi-cohorte-4/Ruta%20.NET%20-%20AM/_boards/board/t/Estelar/Backlog%20items

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
-  **React**
-  **Cloudinary**

---

##  Getting Started

### 1️ Prerequisites

Make sure you have installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [MySQL Server](https://dev.mysql.com/downloads/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Optional: [Postman](https://www.postman.com/) for testing API requests
- Cloudinary

---

###  Clone the Repository

```bash
https://github.com/Estelar-Hopper/inmobiliariaApp__HU4.git
cd inmobiliariaApp__HU4
```

---

If you want to works this project locally:

###  Configure Database Connection

In `appsettings.json` (inside **inmobiliariaApp.Api**), update the connection string:

```json
{
"Jwt": {
"Key": "your_secret_key_here",
"Issuer": "yourapp",
"Audience": "yourapp_users"
},
"ConnectionStrings": {
"DefaultConnection" : "server=localhost;database=inmobiliariaApp_db;user=root;password=yourpassword;"
},
"CloudinarySettings": {
"CloudName": "yourUserInCloudinary",
"ApiKey": "yourApyKeyInCloudinary",
"ApiSecret": "yourApiSecretInCloudinary"
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
},
"AllowedHosts": "*"
}
```

---

###  Apply Database Migrations

Run from the root directory:

```bash
dotnet ef database update --project inmobiliariaApp.Infrastructure --startup-project inmobiliariaApp.Api
```

This creates all required tables (**Users**, **Properties**) in your MySQL database.

---

###  Run the Application

```bash
cd inmobiliariaApp..Api
dotnet run
```

Swagger UI will be available locally in a port as the next one:

 **https://localhost:7069/swagger**

---

##  Authentication & Roles

The API uses **JWT Bearer Authentication**.

| Role   | Permissions                       |
|--------|-----------------------------------|
| Admin  | Full CRUD on Properties and Users |
| Client | Read-only access to Properties    |

---

##  API Endpoints

| Method | Endpoint                     | Description              |
|---------|------------------------------|--------------------------|
| GET | `/api/Property/getAll`       | Get all properties       |
| GET | `/api/Property/getById/{id}` | Get property by ID       | 
| POST | `/api/Property/create`       | Add a new Property       | 
| PUT | `/api/Property/update/{id}`  | Update existing Property | 
| DELETE | `/api/Property/delete/{id}`  | Delete Property          | 
| GET | `/api/User/getAll`           | Get all Users            |
| GET | `/api/User/getById/{id}` | Get User by ID           | 
| POST | `/api/User/create`       | Add a new User           | 
| PUT | `/api/User/update/{id}`  | Update existing User     | 
| DELETE | `/api/User/delete/{id}`  | Delete User              | 

---
On postman you must add a authetication

**Header:**
```
Authorization: Bearer <your_admin_token>
```

---

##  Folder Purpose Summary

| Folder             | Responsibility                                                                           |
|--------------------|------------------------------------------------------------------------------------------|
| **Domain**         | Core entities and repository interfaces. Defines the business model without dependencies. |
| **Application**    | Business logic, DTOs, validation, and use cases. Connects domain with infrastructure.    |
| **Infrastructure** | Database connection, EF Core context, and repository implementations.                    |
| **API**            | Controllers, authentication, and configuration. Entry point of the application.          |
| **Docs**           | Entire documentations and images of the correct use of the application.            |

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


