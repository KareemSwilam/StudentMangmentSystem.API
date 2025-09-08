# 🎓 Student Management System (EF Core + Repository Pattern)



## 🚀 Features
- Manage **Students** (Add, Update, Delete, Get All, Get By Id, Get By Name).
- Manage **Courses** and link them to **Departments**.
- Manage **Enrollments** (Student ↔ Course relationship with Grade).
- One-to-Many relationship:
  - **Department → Courses**
- Many-to-Many relationship (with join entity):
  - **Student ↔ Enrollment ↔ Course**
- Repository Pattern implementation for clean architecture.
- Uses **Migrations** to generate database schema.

---

## 🗄️ Database Schema
### Entities
- **Student**
  - `Id`, `Name`, `Email`
- **Course**
  - `Id`, `Title`, `Credits`, `DepartmentId`
- **Department**
  - `Id`, `Name`, `Budget`, `StartDate`
- **Enrollment**
  - `Id`, `StudentId`, `CourseId`, `Grade`

---

## 📡 API Endpoints

### Students
- `GET /api/students` → Get all students  
- `GET /api/students/{id}` → Get student by Id
- `GET /api/students/{name}` → Get student by name
- `GET /api/students/{id}` → Get ؤourses registered by the student
- `POST /api/students` → Create new student  
- `POST /api/students/{id}` → Update student  
- `DELETE /api/students/{id}` → Delete student  

### Courses
- `GET /api/courses` → Get all courses  
- `GET /api/courses/{id}` → Get course by Id
- `GET /api/courses/{name}` → Get course by Name
- `GET /api/courses/{id}` → Get students who registered for this course  
- `POST /api/courses` → Create new course  
- `POST /api/courses/{id}` → Update course  
- `DELETE /api/courses/{id}` → Delete course  

### Enrollments
- `GET /api/enrollments` → Get all enrollments
- `GET /api/enrollments/{id}` → Get  enrollment by Id  
- `POST /api/enrollments` → Enroll a student in a course  
- `DELETE /api/enrollments/{id}` → Remove enrollment  
- `POST /api/enrollments/{id}` → Update enrollment  
### Departments
- `GET /api/departments` → Get all departments
- `GET /api/departments/{name}` → Get department by Name
- `GET /api/departments/{id}` → Get department by Id   
- `POST /api/departments` → Create new department  
- `DELETE /api/departments/{id}` → Remove department  
---

## 🏗️ Project Structure
