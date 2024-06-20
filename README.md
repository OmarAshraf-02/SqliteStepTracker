# SQLite Step Tracker

A C# console application made to practice SQLite and CRUD operations as well as using external NuGet packages, in this case Spectre.Console.

## Features

The app is a simple daily step tracker where the user can:

- Log their steps for the day.
- Update previous entries.
- Delete logged entries.
- View all their entries in a nicely formatted table, printed with Spectre.Console.

## Technologies & Packages

- **SQLite**: This project was mainly made as a prototype to practice using SQLite with the ADO.NET C# SQLite provider. SQLite was used to create a .db file along with a table to persistently store and query tracked user data locally.
- **Spectre.Console**: A NuGet package used for printing figures such as tables in the console, as well as easier formatting and customization of text in the console.

## How to Run

### Ensure you have .NET 8.0 installed

Follow these steps to run:

1. **Clone this repository**:

```bash
git clone https://github.com/OmarAshraf-02/SqliteStepTracker.git
```

2. **Navigate to the project directory**:

```bash
cd SqliteStepTracker
```

3. **Build the project**:

```bash
dotnet build
```

4. **Run the project**:

```bash
dotnet run
```
