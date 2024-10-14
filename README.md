# ASP.NET Static CRUD Application with MySQL

This project is a simple **Static CRUD (Create, Read, Update, Delete)** web application built using **ASP.NET** with **MySQL** as the database. It allows users to perform basic data operations like creating, reading, updating, and deleting records in a MySQL database through a user-friendly web interface.

## Project Overview

The primary goal of this project is to demonstrate the implementation of a static CRUD application using ASP.NET, connected to a MySQL database. The application covers:

- **Create**: Adding new records to the database.
- **Read**: Retrieving and displaying records from the database.
- **Update**: Modifying existing records.
- **Delete**: Removing records from the database.

## Technologies Used

- **ASP.NET**: The framework used to build the web application.
- **MySQL**: The database for storing the records.
- **Entity Framework**: Used as an ORM (Object-Relational Mapper) to interact with the MySQL database.
- **Bootstrap**: For responsive UI design.

## Features

- **Add New Records**: Users can input data through a form and add new records to the MySQL database.
- **View All Records**: The system displays a list of all records from the database in a table format.
- **Edit Records**: Users can update existing records.
- **Delete Records**: Users can remove records from the database.
  
### Advantages

- **Simple and Efficient**: The application is easy to use and demonstrates core CRUD operations.
- **Responsive UI**: Built using Bootstrap for a clean and mobile-responsive interface.
- **Scalable**: Though it’s static, the CRUD operations provide a foundation for more advanced dynamic systems.

## Getting Started

### Prerequisites

1. **ASP.NET Core SDK**: Install the latest version of [ASP.NET Core SDK](https://dotnet.microsoft.com/download).
2. **MySQL Database**: Install and configure [MySQL](https://www.mysql.com/) on your local machine or use a cloud-based MySQL instance.
3. **Visual Studio**: A development environment like [Visual Studio](https://visualstudio.microsoft.com/).

### Installation Steps

1. **Clone the Repository**:
   ```
   git clone https://github.com/yourusername/static-crud-aspnet-mysql.git
   ```

2. **Set Up MySQL Database**:
   - Create a MySQL database.
   - Update the **connection string** in `appsettings.json` with your MySQL server details:
     ```
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=your-database;User=your-username;Password=your-password;"
     }
     ```

3. **Apply Migrations**:
   Use Entity Framework to create the database tables.
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**:
   ```
   dotnet run
   ```

   The application should now be running at `http://localhost:5000/`.
## Conclusion

This project demonstrates a static CRUD application using ASP.NET and MySQL, providing a solid foundation for building more complex applications in the future. It’s an ideal starting point for anyone looking to understand the basics of CRUD operations in a web environment using .NET and MySQL.

---

Feel free to fork the repository or submit a pull request to improve the functionality!
