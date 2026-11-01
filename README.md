![.NET Vesrion](https://img.shields.io/badge/.NET-9.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-blue)
![Database](https://img.shields.io/badge/Database-SQLite-green)
## :mag_right: About
Tracking Coding Sessions is a good way for the user to evaluate how often they code. **Coding Tracker** is a [TUI](https://en.wikipedia.org/wiki/Text-based_user_interface) application that offers simple [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations to track the user's Coding Sessions by either manually typing the start and end of a coding session, or by using a built-in Stopwatch to track the session in real time. 

## How to run locally
The app has not been published, so it needs to run locally by .NET's runtime.  
> [!IMPORTANT] Make sure [**.NET SDK 9**](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or higher is installed in order to run this project.

Clone the repository:
```
git clone https://github.com/Kunikazu723/TCSA.HabitTracker.git
cd CodeReviews.Console.CodingTracker
```

Restore dependencies:
```
dotnet restore
```

Run the project:
```
dotnet run --poject CodingTracker.Kunikazu723
```

To change databasePath, connection strings and date time format, refer to the [App Settings]() section.

## 💾 How it works
First the Database is generated with 100 random Coding Sessions. Then the user is presented with 5 options tha can be selected using the keyboard's arrows, 4 of them are CRUD operations.  
Each Coding Session needs a Start Date and End Date, both are stored in the database with a default date-time format. The duration is calculated during data presentation.

## :pencil2: Architectural Choices

TODO

## Reflection

TODO