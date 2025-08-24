# TaskManager

A simple **Task Manager** app with a **.NET 9 backend** and a **React frontend**, following **Clean Architecture** principles.  

This project allows you to manage tasks (CRUD) via a REST API and a web interface.

---

## Run with Docker

1. Build and start the containers
```
docker-compose up --build
```
2. Wait for both containers to start:
- API: http://localhost:5103/swagger/index.html
- Frontend: http://localhost:5173
3. Stop containers:
```
docker-compose down
```

---

## ⚡ Prerequisites

Before running this project, make sure you have:

1. **.NET 9 SDK**  
   [https://dotnet.microsoft.com/en-us/download/dotnet/9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

2. **Node.js & npm**  
   [https://nodejs.org/](https://nodejs.org/)

3. **Git** (optional, for cloning)  
   [https://git-scm.com/downloads](https://git-scm.com/downloads)

4. **SQLite** (optional, to inspect the database)  
   [https://www.sqlite.org/download.html](https://www.sqlite.org/download.html)

---

## 🚀 Backend Setup

1. **Restore NuGet packages**

``` bash
dotnet restore
```

2. **Run the API**
``` bash
dotnet run --project TaskManager.API

```
API runs at: http://localhost:5103/swagger/index.html.

## 🌐 Frontend Setup

1. **Go to the frontend folder and install dependencies**
```
cd frontend/taskmanager-frontend/
npm install
```
2. **Start the development server**
```
npm run dev
```

The frontend will open in your browser at http://localhost:5173 and automatically interact with the backend API.

Make sure the backend is running before using the frontend.

## 🧪 Run Backend Tests
```
cd ../TaskManager.Tests
dotnet test
```
Tests use xUnit and Moq.

## 🔧 API Endpoints

| Method | Endpoint           | Description        |
|--------|------------------|------------------|
| GET    | `/api/tasks`      | Get all tasks     |
| GET    | `/api/tasks/{id}` | Get a task by ID  |
| POST   | `/api/tasks`      | Create a new task |
| PUT    | `/api/tasks/{id}` | Update a task     |
| DELETE | `/api/tasks/{id}` | Delete a task     |
