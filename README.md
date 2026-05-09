````md
# ECOO 🌱
### Eco-Friendly E-Commerce Platform built with ASP.NET Core MVC

---

## 📌 Overview

ECOO is a modern eco-friendly e-commerce platform developed using **ASP.NET Core MVC** and **Clean Architecture** principles.

The project focuses on:

- Clean and scalable architecture
- Separation of concerns
- Maintainability
- Reusability
- Performance
- Best practices in ASP.NET Core development

ECOO allows users to browse eco-friendly products, manage shopping carts, and complete checkout operations in a simple and responsive interface.

---

# 🚀 Technologies Used

| Technology | Purpose |
|---|---|
| ASP.NET Core MVC (.NET 8) | Web Framework |
| Entity Framework Core | ORM |
| SQL Server | Database |
| Bootstrap 5 | Responsive UI |
| Razor Views | Frontend Rendering |
| Dependency Injection | Loose Coupling |
| Session State | Shopping Cart |
| LINQ | Querying Data |

---

# 🏗️ Clean Architecture

The solution is organized into multiple layers to achieve modularity and maintainability.

```text
ECOO/
│
├── ECOO.Domain
├── ECOO.Application
├── ECOO.Infrastructure
└── ECOO.Web
````

---

# 📂 Project Structure

```text
ECOO/
│
├── ECOO.Domain/
│   └── Entities/
│
├── ECOO.Application/
│   ├── Interfaces/
│   ├── Services/
│   └── ViewModels/
│
├── ECOO.Infrastructure/
│   ├── Data/
│   ├── Interfaces/
│   └── Repositories/
│
├── ECOO.Web/
│   ├── Controllers/
│   ├── Views/
│   ├── wwwroot/
│   └── Program.cs
│
└── README.md
```

---

# 🧩 Architecture Layers

---

## 1️⃣ Domain Layer

Contains only the core entities of the application.

### Entities

* Product
* Category
* Order
* OrderItem

This layer has:

* No business logic
* No database logic
* No dependencies on other layers

---

## 2️⃣ Application Layer

Contains all business logic.

### Includes

* Services
* Interfaces
* ViewModels

### Services

* ProductService
* CategoryService
* CartService
* OrderService

### Responsibilities

* Handle business rules
* Communicate with repositories
* Map entities to ViewModels

---

## 3️⃣ Infrastructure Layer

Responsible for data access and database operations.

### Includes

* AppDbContext
* Repositories
* EF Core Configurations

### Patterns Used

* Repository Pattern
* Generic Repository

---

## 4️⃣ Presentation Layer

The user interface layer.

### Includes

* MVC Controllers
* Razor Views
* Bootstrap UI
* Client-side validation

---

# ✨ Features

---

## 🛍️ Product Management

* View Products
* Product Details
* Create Product
* Edit Product
* Delete Product

---

## 🗂️ Category Management

* Create Category
* Edit Category
* Delete Category
* Display Categories

---

## 🛒 Shopping Cart

Session-based cart implementation.

### Features

* Add to Cart
* Remove from Cart
* Update Quantity
* Calculate Total Price

---

## 💳 Checkout System

* Create Orders
* Store Order Items
* Calculate Total Amount
* Clear Cart after Checkout

---

# 🗄️ Database Design

---

## Product

| Field       | Type    |
| ----------- | ------- |
| Id          | int     |
| Name        | string  |
| Description | string  |
| Price       | decimal |
| ImageUrl    | string  |
| CategoryId  | int     |

---

## Category

| Field | Type   |
| ----- | ------ |
| Id    | int    |
| Name  | string |

---

## Order

| Field       | Type     |
| ----------- | -------- |
| Id          | int      |
| OrderDate   | DateTime |
| TotalAmount | decimal  |

---

## OrderItem

| Field     | Type    |
| --------- | ------- |
| Id        | int     |
| ProductId | int     |
| Quantity  | int     |
| Price     | decimal |
| OrderId   | int     |

---

# 🔗 Relationships

| Relationship         | Type        |
| -------------------- | ----------- |
| Category → Products  | One-to-Many |
| Order → OrderItems   | One-to-Many |
| Product → OrderItems | One-to-Many |

---

# 🧠 Design Patterns Used

---

## ✅ Repository Pattern

Separates data access logic from business logic.

---

## ✅ Generic Repository Pattern

Reduces duplicated CRUD operations.

---

## ✅ Service Layer Pattern

Keeps controllers clean and thin.

---

## ✅ Dependency Injection

Improves testability and maintainability.

---

# 🧾 Validation

The project uses **Data Annotations** for model validation.

### Examples

```csharp
[Required]
[StringLength(100)]
public string Name { get; set; }

[Range(1, 100000)]
public decimal Price { get; set; }
```

---

# ⚡ Asynchronous Programming

All database operations use:

```csharp
async / await
```

This improves:

* Performance
* Scalability
* Responsiveness

---

# 🛠️ Entity Framework Core

---

## Database Provider

* SQL Server

---

## Migrations

### Create Migration

```bash
Add-Migration InitialCreate
```

### Update Database

```bash
Update-Database
```

---

# 🌱 Seeded Data

The project automatically seeds sample data into the database.

### Sample Categories

* Eco Bags
* Reusable Bottles
* Organic Products

### Sample Products

* Eco Bag
* Steel Bottle
* Bamboo Toothbrush

---

# 🎨 User Interface

The UI is built using:

* Bootstrap 5
* Razor Views
* Responsive Layout

### Pages

* Home Page
* Product Details
* Categories
* Shopping Cart
* Checkout

---

# ⚙️ Setup Instructions

---

## 1️⃣ Clone Repository

```bash
git clone <repository-url>
```

---

## 2️⃣ Open Solution

Open using:

* Visual Studio 2022
  OR
* Visual Studio Code

---

## 3️⃣ Configure Connection String

Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection":
  "Server=.;Database=ECOO_DB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

## 4️⃣ Apply Migrations

Open Package Manager Console:

```bash
Add-Migration InitialCreate
Update-Database
```

---

## 5️⃣ Run Project

```bash
dotnet run
```

OR press:

```text
Ctrl + F5
```

---

# 📌 Controllers

| Controller         | Responsibility    |
| ------------------ | ----------------- |
| HomeController     | Home Page         |
| ProductController  | Product CRUD      |
| CategoryController | Category CRUD     |
| CartController     | Cart Operations   |
| OrderController    | Checkout & Orders |

---

# 📦 ViewModels

* ProductViewModel
* CategoryViewModel
* CartItemViewModel
* CartViewModel
* CheckoutViewModel

---

# 🧪 Error Handling

The application handles:

* Invalid Inputs
* Validation Errors
* Database Exceptions
* 404 Errors
* Server Errors

---

# 🔒 Security Notes

Authentication and authorization are intentionally excluded based on project requirements.

---

# 🚀 Future Improvements

Possible enhancements:

* Authentication & Authorization
* Payment Gateway Integration
* Product Search & Filtering
* Wishlist System
* Product Reviews & Ratings
* Admin Dashboard
* REST API Version
* Image Upload Support
* Email Notifications

---

# 📖 Learning Objectives

This project demonstrates:

* ASP.NET Core MVC Architecture
* Entity Framework Core
* Repository Pattern
* Service Layer
* Dependency Injection
* Session Management
* Clean Architecture Principles

---

# 👨‍💻 Author

Developed as a clean layered ASP.NET Core MVC project for educational and professional practice purposes.

---

# ⭐ Final Notes

ECOO is designed to be:

✅ Clean
✅ Scalable
✅ Maintainable
✅ Beginner-Friendly
✅ Production-Ready Architecture Inspired

---

🌱 **Build a greener future with ECOO!**

```
```
