# ProductManagement

.NET Core 2.2 Project, using SQL Server database


## Entity Framework Commands

### Update database command
Powershel:
```
Update-Database -Context ProductManagementDbContext
```

Console (on ProductManagement.Data project):
```
dotnet ef database update --startup-project ..\ProductManagement.App\ --context ProductManagementDbContext
```