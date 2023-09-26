# To Do

## Backedn
- przetestowanie aktualnych endpointow
- relacje między tabelami
- middleware
- panel admina
- autoryzacja i autentykacja

## Frontend
- 
-
-


---
# Restaurant API with C# and Vue.js

This is a simple restaurant management application that consists of a back-end API built with C# and a front-end user interface developed using Vue.js with Bootstrap for styling. This application allows restaurant owners to manage their menu items and orders.

## Features

- **Menu Management**: Add, edit, and delete menu items.
- **Order Management**: Create and manage customer orders.
- **User Authentication**: Secure user authentication for restaurant staff.

## Technologies Used

- **Back-end**:
  - C# (.NET Core)
  - Entity Framework Core
  - RESTful API
  - Swagger for API documentation

- **Front-end**:
  - Vue.js
  - Bootstrap
  - Axios for API communication

## Getting Started

### Prerequisites

- Visual Studio (or Visual Studio Code) for C# development
- Node.js and npm for Vue.js development
- SQL Server for the database (or use an alternative database)
- Git for version control

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Ainsik/restaurant-api.git
   ```

2. Open the `restaurant-api` folder in your preferred code editor.

3. Set up the database:
   - Create a new SQL Server database or configure your preferred database.
   - Update the database connection string in the `appsettings.json` file.

4. Build and run the back-end API:
   - Open the solution in Visual Studio (or Visual Studio Code).
   - Build and run the API project.

5. Install the necessary Node.js packages for the front-end:

   ```bash
   cd src
   cd Api
   cd client-app
   npm install
   ```

6. Build and run the front-end:

   ```bash
   npm run serve
   ```

7. Access the application in your web browser at `http://localhost:8080`.

## API Documentation

You can access the API documentation and test the endpoints using Swagger. Visit `http://localhost:5000/swagger` when the API is running.
