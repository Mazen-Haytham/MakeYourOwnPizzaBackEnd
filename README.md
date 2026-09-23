<div align="center">
  <h1>🍕 Make Your Own Pizza - Backend API</h1>
  
  <p>
    <strong>A robust, scalable, and maintainable RESTful API built with .NET and Clean Architecture principles.</strong>
  </p>
  
  <p>
    <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet" alt=".NET 8" />
    <img src="https://img.shields.io/badge/Clean%20Architecture-Success-success?style=for-the-badge" alt="Clean Architecture" />
    <img src="https://img.shields.io/badge/Docker-Supported-2496ED?style=for-the-badge&logo=docker" alt="Docker" />
    <img src="https://img.shields.io/badge/EF%20Core-ORM-blue?style=for-the-badge" alt="EF Core" />
  </p>
</div>

<br />

## 📖 Overview

The **Make Your Own Pizza Backend** is the core API that powers the pizza ordering platform. It provides endpoints for managing ingredients, processing custom pizza orders, and handling delivery logistics. 

This project is meticulously crafted to serve as a showcase of modern software engineering practices, specifically focusing on **Clean Architecture**, **Domain-Driven Design (DDD)** concepts, and **Containerization (Docker)**. It is highly decoupled, testable, and ready for cloud deployment.

---

## ✨ Key Features

- **Custom Pizza Creation**: Mix and match ingredients to build a perfect pizza.
- **Order Management**: Robust tracking of order state and pricing.
- **Inventory & Ingredients Management**: Structured ingredient categories and pricing models.
- **Delivery Tracking**: Integrated delivery endpoints for driver updates.
- **Clean Architecture**: Strong separation of concerns across Domain, Application, Infrastructure, and Presentation layers.
- **Dockerized**: Fully containerized for seamless development and deployment.
- **Entity Framework Core**: Code-first database migrations and robust data access.

---

## 🏗️ Architecture

This project enforces **Clean Architecture**, ensuring that the core business logic (Domain) is completely isolated from external dependencies like databases, frameworks, or UI.

```text
Solution1/
├── 1. MakeYourOwnPizza.Domain        # Enterprise Logic (Entities, Enums, Exceptions) - No Dependencies
├── 2. MakeYourOwnPizza.Application   # Business Logic (Services, Interfaces, DTOs) - Depends on Domain
├── 3. MakeYourOwnPizza.Infrastructure# Data Access (EF Core, Repositories) - Depends on Application
└── 4. MakeYourOwnPizza.Presentation  # API Layer (Controllers) - Depends on Application & Infrastructure
```

### Why Clean Architecture?
- **Framework Independent**: The system does not depend on the existence of some library of feature-laden software.
- **Testable**: The business rules can be tested without the UI, Database, Web Server, or any other external element.
- **Independent of UI**: The UI can change easily without changing the rest of the system.
- **Independent of Database**: You can swap out MySQL for MongoDB or anything else without affecting business logic.

---

## 🛠️ Technology Stack

- **C# / .NET 8.0**: Latest Microsoft framework for high performance.
- **ASP.NET Core Web API**: For building RESTful HTTP services.
- **Entity Framework Core**: Object-Relational Mapper (ORM) for data access.
- **MySQL**: Relational database for persistent storage.
- **Docker & Docker Compose**: For containerization and environment consistency.
- **Swagger / OpenAPI**: For API documentation and testing.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- IDE (Visual Studio, Rider, or VS Code)

### 🐳 Run with Docker (Recommended)

The easiest way to get the project running with its database is via Docker.

1. Clone the repository:
   ```bash
   git clone https://github.com/Mazen-Haytham/MakeYourOwnPizzaBackEnd.git
   cd MakeYourOwnPizzaBackEnd/Solution1
   ```

2. Build and start the containers:
   ```bash
   docker-compose up -d --build
   ```

3. The API will be available at `http://localhost:<port>/swagger` (check docker-compose.yml for port).

### 💻 Run Locally (Without Docker)

1. Navigate to the Presentation layer:
   ```bash
   cd Solution1/MakeYourOwnPizza.Presentation
   ```

2. Update the `appsettings.json` connection string to point to your local MySQL instance.

3. Apply Entity Framework Migrations to create the database:
   ```bash
   dotnet ef database update --project ../MakeYourOwnPizza.Infrastructure --startup-project .
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

---

## 📦 Database Seeding

The application includes an automatic database seeder (`DbSeeder.cs`) that populates the database with initial ingredients (e.g., Dough types, Sauces, Cheeses, and Toppings) when the application starts in the development environment.

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! 
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

<br />
<div align="center">
  <i>Crafted with passion for clean code and great pizza. 🍕💻</i>
</div>