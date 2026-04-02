# Course Management API

## 📌 Project Overview

This project is a **Course Management System API** built using **ASP.NET Core Web API**.
It allows managing students, instructors, courses, and enrollments with proper relationships.

The API also includes **JWT Authentication and Role-Based Authorization**.

---

## 🚀 Technologies Used

* ASP.NET Core Web API (.NET 10)
* Entity Framework Core
* SQLite Database
* Swagger (OpenAPI)
* JWT Authentication

---

## 📂 Features

* Manage Students (Create, Read, Update, Delete)
* Manage Instructors (with profile)
* Manage Courses (linked to instructors)
* Manage Enrollments (many-to-many between students and courses)
* DTOs for data transfer and validation
* Service layer with dependency injection
* JWT Authentication (Login system)
* Role-based Authorization (Admin, Instructor, User)

---

## 🧱 Database Design

Entities used:

* Student
* Instructor
* InstructorProfile (1:1 with Instructor)
* Course (Many-to-1 with Instructor)
* Enrollment (Many-to-Many between Student and Course)

---

## 🔐 Authentication & Authorization

### Login Endpoint

```
POST /api/Auth/login
```

### Example Request

```json
{
  "username": "admin",
  "password": "admin123"
}
```

### Roles

* Admin
* Instructor
* User

### Authorization Rules

* Admin → full access
* Instructor → manage courses and enrollments
* User → read-only access

---

## ▶️ How to Run the Project

1. Open terminal in project folder

2. Run:

```
dotnet build
dotnet run
```

3. Open Swagger:

```
http://localhost:5136/swagger
```

---

## 🧪 Testing the API

### Step 1: Login

Use:

```
POST /api/Auth/login
```

### Step 2: Copy Token

From response:

```json
{
  "token": "YOUR_TOKEN",
  "role": "Admin"
}
```

### Step 3: Use Token

Add header manually:

```
Authorization: Bearer YOUR_TOKEN
```

---

## 🍪 HTTP-Only Cookies Explanation

HTTP-only cookies are used to store sensitive data such as authentication tokens securely.
They are not accessible via JavaScript, which helps protect against cross-site scripting (XSS) attacks.

In this project, JWT tokens are returned in the response and can be stored securely on the client side.

---

## 📸 Screenshots

* Swagger UI showing endpoints
* Login endpoint working
* Protected endpoints working

---

## ✅ Conclusion

This project demonstrates:

* Clean architecture using DTOs and services
* Proper use of Entity Framework Core
* Secure authentication with JWT
* Role-based authorization
* RESTful API design

---

## 👨‍💻 Author

Student Name: Jessica Amged
ID: 211006719
