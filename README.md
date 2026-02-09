



# User Management API

This project is a simple ASP.NET Core Web API for managing users. It includes JWT authentication, role-based authorization, validation, and custom middleware for error handling and logging.

---
## Features

- Full CRUD operations for users (Create, Read, Update, Delete).
- Login endpoint with password hashing and JWT generation.
- Role-based authorization using `[Authorize]` and `[Authorize(Roles = "...")]`.
- **Middleware for:**
  - Global error handling.
  - Request/response logging.
- Swagger/OpenAPI integration for testing and documentation.

---

## Project Structure

```text
UserManagementApi/
│
├── Controllers/
│   └── UsersController.cs
├── DTO/
│   ├── RegisterDto.cs
│   ├── UpdateDto.cs
│   └── LoginDto.cs
├── Models/
│   └── User.cs
├── Middleware/
│   ├── ErrorHandlingMiddleware.cs
│   ├── LoggingMiddleware.cs
├── Services/
│   ├── IUserRepository.cs
│   └── InMemoryUserRepository.cs
└── Program.cs
```
___

## Authentication and Authorization

### **1. Login** (the user below has the admin role)
**Endpoint:** POST /api/users/login
**Request body:**
```JSON
{
  "userEmail": "Admin123@gmail.com",
  "password": "123456"
}
```

**Response:**

```JSON
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### **2. Using the token**
Include the token in the request header:

```HTTP
Authorization: Bearer <token>
````
#### **Protected endpoints**
  ```[Authorize]``` requires any authenticated user.
  
  ```[Authorize(Roles = "Admin")]``` requires a user with the Admin role.

---
## **Example Endpoints**
- GET /api/users → List all users.
- POST /api/users → Create a new user.
- PUT /api/users/{id} → Update a user.
- DELETE /api/users/{id} → Delete a user.
- POST /api/users/login → Generate a JWT token.
- GET /api/users/profile → Get information about the authenticated user.
- GET /api/users/admin → Accessible only to Admin users.

## Swagger Integration
- Open Swagger at https://localhost:5001/swagger.
- Use the login endpoint to obtain a JWT token.
- Click Authorize in Swagger UI.
- Paste the token in the format:
  
```Plaintext
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```
Swagger will automatically include the token in subsequent requests to protected endpoints.

---
## How Copilot Helped

### Copilot assisted throughout the development process by:

 - Explaining best practices for middleware (global error handling vs.
    try/catch in each endpoint).
 - Guiding the implementation of JWT authentication and role-based
    authorization.
 - Helping debug common issues:
	 - **401 Unauthorized**: by checking issuer, audience, and claims. 
	 - **415 Unsupported Media Type**: by correcting Content-Type.
	  - **IDX10720**: by ensuring the secret key length was sufficient for HS256.
	  - **Suggesting project organization into folders** (Controllers, DTO, Middleware, Services).
	   
 - Providing examples of validation with DataAnnotations. 
 - Documenting how to configure Swagger with JWT support. 
 - Offering clear explanations and structured summaries to ensure the project met evaluation criteria.
___

## Summary

### This API demonstrates:
- Complete CRUD functionality.
- Middleware for error handling and logging.
- Validation of input data.
- Secure authentication and authorization with JWT.
- Clear documentation and testing support via Swagger.
- Effective use of Copilot for debugging, design decisions, and documentation.

