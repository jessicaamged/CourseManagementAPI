# Course Management System - Frontend

This project is a React-based frontend application connected to an ASP.NET Core backend API. It allows users to register, log in, and manage courses and enrollments.

---

## Features

### Authentication
- User Registration (Student, Instructor, Admin)
- User Login
- JWT Authentication
- Protected Routes (only logged-in users can access main pages)

### Courses Management
- View all courses
- Add new courses (Admin/Instructor only)
- Edit courses
- Delete courses

### Enrollments Management
- Students can enroll in courses
- Admin/Instructor can view all enrollments
- Admin can delete enrollments
- Prevent duplicate enrollments

### UI Features
- Modern responsive UI
- Dashboard with user info
- Styled forms and cards
- Navigation bar with role-based options

---

## Technologies Used

- React (Functional Components)
- React Router
- Axios
- React Hooks (useState, useEffect)
- CSS (custom styling)
- ASP.NET Core Web API (backend)

---

## Project Structure

src/
├── components/
│   ├── Navbar.jsx
│   └── ProtectedRoute.jsx
│
├── pages/
│   ├── Login.jsx
│   ├── Register.jsx
│   ├── Dashboard.jsx
│   ├── Courses.jsx
│   └── Enrollments.jsx
│
├── services/
│   └── api.js
│
├── App.jsx
├── main.jsx
└── styles.css

---

## Backend Setup

1. Navigate to backend folder:

cd CourseManagementAPI

2. Run backend:

dotnet run

3. Backend runs at:

https://localhost:7000

---

## ⚙️ Frontend Setup

1. Install dependencies:

npm install

2. Run frontend:

npm run dev

3. Frontend runs at:

http://localhost:5173

---

## API Endpoints Used

### Authentication

POST /api/Auth/register  
POST /api/Auth/login  

### Courses

GET /api/Courses  
POST /api/Courses  
PUT /api/Courses/{id}  
DELETE /api/Courses/{id}  

### Enrollments

GET /api/Enrollments  
POST /api/Enrollments  
DELETE /api/Enrollments/{userId}/{courseId}  

---

## User Roles

### Student
- View courses
- Enroll in courses
- Cannot edit/delete courses
- Cannot view all enrollments

### Instructor
- View courses
- Add, edit, delete courses
- View all enrollments

### Admin
- Full control over system
- Manage courses
- View and delete enrollments

---

## Screenshots



- Login Page
- Register Page
- Dashboard
- Courses Page (CRUD)
- Enrollments Page

---

## Notes

- Authentication is handled using JWT tokens stored in localStorage.
- Axios interceptor is used to send the token with every request.
- Backend enforces role-based authorization.
- Students cannot access restricted endpoints like viewing all enrollments.

---

## Conclusion

This project demonstrates full frontend-backend integration using React and ASP.NET Core, including authentication, routing, API communication, and CRUD operations.