# 🎬 CinemAPI

**CinemAPI** — backend-сервис на ASP.NET Core (.NET 10), построенный по трёхслойной архитектуре с выделенным доменным слоем. Предоставляет Web API для работы с данными (CRUD), хранит файлы и обрабатывает фоновые операции через Azure Storage.

## 📐 Архитектура

Проект построен по принципу **трёхслойной архитектуры (3-Layer Architecture)** с дополнительным выделенным **Domain**-слоем, что приближает решение к подходу Clean Architecture:

```
CinemAPI/
├── CinemAPI.Domain/            # Доменный слой
│   ├── Entities/                # Сущности предметной области
│   ├── Interfaces/              # Контракты репозиториев и сервисов
│   └── Exceptions/              # Доменные исключения
│
├── CinemAPI.DataAccess/        # Слой доступа к данным
│   ├── Context/                 # DbContext (Entity Framework Core)
│   ├── Repositories/            # Реализации Repository Pattern
│   ├── Migrations/              # EF Core миграции
│   └── Configurations/          # Fluent API конфигурации сущностей
│
├── CinemAPI.BusinessLogic/     # Слой бизнес-логики
│   ├── Services/                 # Сервисы (бизнес-правила, оркестрация)
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Mapping/                  # AutoMapper-профили
│   └── Validators/               # Валидация (FluentValidation)
│
└── CinemAPI.WebAPI/             # Presentation-слой
    ├── Controllers/              # API-контроллеры
    ├── Middlewares/              # Обработка ошибок и пр.
    ├── Extensions/                # DI-регистрация, конфигурация Scalar
    └── Program.cs
```

**Принцип зависимостей:** `WebAPI → BusinessLogic → DataAccess → Domain`
Domain не зависит ни от одного из слоёв и содержит только бизнес-сущности и интерфейсы — это позволяет легко подменять реализации (например, репозитории) без изменения бизнес-логики.

## 🛠 Технологический стек

| Категория | Технология |
|---|---|
| Язык | C# |
| Платформа | ASP.NET Core (.NET 10) |
| Тип приложения | Web API |
| ORM | Entity Framework Core |
| База данных | Azure SQL Server |
| Файловое/очередное хранилище | Azure Storage (Blob + Queue) |
| Документация API | Scalar |
| Паттерны | Repository Pattern, Dependency Injection |

## ✨ Возможности

- ✅ CRUD-операции над сущностями предметной области
- ✅ Трёхслойная архитектура с изоляцией Domain-слоя
- ✅ Repository Pattern поверх EF Core для абстракции доступа к данным
- ✅ Dependency Injection для всех сервисов и репозиториев
- ✅ Хранение файлов (изображения, медиа) в **Azure Blob Storage**
- ✅ Асинхронная обработка фоновых задач через **Azure Queue Storage**
- ✅ Интерактивная документация и тестирование API через **Scalar**
- ✅ Миграции базы данных через EF Core Migrations

## 📋 Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Azure SQL Server (либо локальный SQL Server для разработки)
- Учётная запись Azure Storage (Blob + Queue)
- Visual Studio 2022+ / VS Code / Rider

## 🚀 Быстрый старт

### 1. Клонирование репозитория

```bash
git clone https://github.com/<your-username>/CinemAPI.git
cd CinemAPI
```

### 2. Настройка конфигурации

Добавьте строки подключения в `CinemAPI.WebAPI/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:<your-server>.database.windows.net,1433;Database=CinemAPI;User ID=<user>;Password=<password>;Encrypt=True;",
    "AzureStorage": "DefaultEndpointsProtocol=https;AccountName=<account-name>;AccountKey=<account-key>;EndpointSuffix=core.windows.net"
  },
  "AzureStorage": {
    "BlobContainerName": "cinemapi-files",
    "QueueName": "cinemapi-queue"
  }
}
```

> 💡 Для production рекомендуется использовать **Azure Key Vault** или **User Secrets** вместо хранения ключей в `appsettings.json`.

### 3. Применение миграций

```bash
dotnet ef database update --project CinemAPI.DataAccess --startup-project CinemAPI.WebAPI
```

### 4. Запуск приложения

```bash
dotnet run --project CinemAPI.WebAPI
```

После запуска API будет доступен по адресу `https://localhost:<port>`, а интерактивная документация Scalar — по адресу:

```
https://localhost:<port>/scalar/v1
```

## 📦 Работа с миграциями EF Core

Создать новую миграцию:

```bash
dotnet ef migrations add <MigrationName> --project CinemAPI.DataAccess --startup-project CinemAPI.WebAPI
```

Применить миграции к базе данных:

```bash
dotnet ef database update --project CinemAPI.DataAccess --startup-project CinemAPI.WebAPI
```

Откатить миграцию:

```bash
dotnet ef database update <PreviousMigrationName> --project CinemAPI.DataAccess --startup-project CinemAPI.WebAPI
```

## ☁️ Интеграция с Azure Storage

| Сервис | Назначение |
|---|---|
| **Blob Storage** | Хранение файлов (постеры фильмов, изображения, документы) |
| **Queue Storage** | Асинхронная обработка фоновых операций (например, обработка загруженных файлов, рассылка уведомлений) |

Доступ к Storage реализован через абстракции в `Domain.Interfaces` (`IBlobStorageService`, `IQueueStorageService`) и регистрируется через DI в `CinemAPI.WebAPI/Extensions`.

## 🧩 Паттерны проектирования

### Repository Pattern
Каждая сущность Domain-слоя имеет соответствующий репозиторий с интерфейсом в `Domain.Interfaces` и реализацией в `DataAccess.Repositories`, что изолирует бизнес-логику от деталей работы с EF Core.

### Dependency Injection
Все сервисы, репозитории и клиенты Azure Storage регистрируются в контейнере DI в `Program.cs` / `Extensions`, что обеспечивает слабую связанность компонентов и упрощает тестирование.

## 🧪 Тестирование

```bash
dotnet test
```

## 📄 Лицензия

Укажите лицензию проекта (например, MIT).

---

Сделано с ❤️ на ASP.NET Core
