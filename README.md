# Student Registration Management System

## 📌 Overview
The **Student Registration Management System** is designed to streamline course enrollment and student approvals through a web-based interface. It supports two primary user roles: **Teacher (Admin)** and **Student**.

This project is built using **ASP.NET Web Forms** and follows a **3-Tier Architecture** (Presentation Layer, Business Logic Layer, Data Access Layer).

---

## 👤 User Roles & Features

### 1. Teacher (Admin)
- Login with predefined credentials (stored in DB)
- View all student registrations (pending & approved)
- Approve or reject student registrations
- Add, remove, or update course information (DB-based)
- Edit or delete student records

### 2. Student
- Register with a form including multi-select course options (fetched from DB)
- Account remains in **pending state** until approved by the teacher
- Post-approval login access
- View personal profile and enrolled courses
- Edit their information and selected courses

---

## 🔧 Key Functional Requirements
- Dropdown and multi-select controls for course selection during registration
- Teacher-only course management (Add/Remove)
- Student login only after admin approval
- User-friendly UI for both dashboards
- Secure password handling and form validations

---

## 💡 Optional Enhancements Implemented
- Dashboard metrics: Total Students, Pending Approvals, Total Courses
- Success and error messages for all major actions
- Organized and scalable project structure using 3-Tier Architecture

---

## 🏗️ Architecture

This application uses a **3-Tier Architecture**:
- **Presentation Layer (UI)** – ASP.NET Web Forms (.aspx)
- **Business Logic Layer (BLL)** – Handles validation and business operations
- **Data Access Layer (DAL)** – Communicates with the database via stored procedures
- Communication via **ASMX Web Services**



---

## 👨‍💻 Developed by
Vaishnavi Gurav  
GitHub:https://github.com/VaishnaviGurav13
