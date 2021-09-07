# Minetest_Project_WebAPI

This project is relate to [Minetest_Project - Classroom](https://github.com/Hope-Shen/Minetest_Project).

## Service (Back-end)
- ASP.NET Core Web API
- AutoMapper (object-to-object mapping library)
- Swagger

## Referenced libraries 
- AutoMapper.Extensions.Microsoft.DependencyInjection 8.1.1
- Microsoft.EntityFrameworkCore 5.0.8
- Microsoft.EntityFrameworkCore.Design 5.0.8
- Microsoft.EntityFrameworkCore.SqlServer 5.0.8
- Microsoft.EntityFrameworkCore.Tools 5.0.8
- Swashbuckle.AspNetCore 6.1.4


## Language
C#

## Build
1. This project is using Entity Franework's Database-First. Therefore, please make sure the database schema is ready or you can follow [this DDL](https://github.com/Hope-Shen/Minetest_Project_WebAPI/blob/main/Database_DDL.sql) created by the author.
2. Generate Code for DBContet and Entity Types for Existing Database: <br>
In terminal type:
``` 
Scaffold-DbContext "Data Source=141.125.110.71,1433;Database=Minetest_DB;Trusted_Connection=False;User ID=MinetestAPI;Password=<DB connection password>" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force 
```
*more detail: https://docs.microsoft.com/en-us/ef/core/cli/powershell#scaffold-dbcontext

3. Please make sure your db connection string is correct. <br>
For example, in the "appsetting.json" file, show follow as below:
```
 "AllowedHosts": "*",
  "ConnectionStrings": {
    "DBConnection": "Data Source=<141.125.110.71,1433>;Integrated Security=False;Initial Catalog=Minetest_DB;User ID=MinetestAPI;Password=<DB connection password>;"
  }
```
4. Run the project
```csharp
dotnet run --project Minetest_Project_WebAPI
```

## Swagger URL
https://localhost:5001/swagger/ <br>
or <br>
http://localhost:5000/swagger/