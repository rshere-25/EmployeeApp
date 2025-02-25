# Employee API

## 📖 Overview
The **Employee API** is a **RESTful Web API** built using **.NET Core 7** that provides CRUD operations for managing employees.  
It supports both **database storage (SQL Server)** and **JSON file-based storage**, based on a configurable setting.  

This API follows a **Repository Pattern** for data access and uses **Entity Framework Core** for database interactions.

---

## 📌 Features
- ✅ **CRUD Operations** for managing employees.
- ✅ **Supports both Database and JSON file-based storage**.
- ✅ **Uses Repository Pattern** to ensure a clean architecture.
- ✅ **Entity Framework Core** for database interactions.
- ✅ **Logging** using Serilog.
- ✅ **Dependency Injection (DI)** for better scalability and maintainability.

---

## 🚀 Technologies Used
| Technology | Version |
|------------|---------|
| .NET Core 7 | ✅ |
| ASP.NET Core Web API | ✅ |
| Entity Framework Core | ✅ |
| SQL Server | ✅ |
| Serilog | ✅ |
| xUnit (Unit Testing) | ✅ |

---

## 📦 Required NuGet Packages
Run the following commands to install the required packages:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Serilog.AspNetCore
dotnet add package System.Text.Json
dotnet add package Moq
dotnet add package xunit
