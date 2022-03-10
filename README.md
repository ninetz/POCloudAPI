# POCloudAPI
What you need to build:

A valid microsoft sql server installation ( not needed if you intend to use different DB for datacontext ) -> add ConnectionString to appsettings so the API can connect to DB of your choice

Dotnet Entity-framework ( dotnet tool install --global dotnet-ef ) -> open command line in root dir of API -> dotnet ef migrations add init -> dotnet ef database update -> this generates and updates your DB

MS visual studio with asp.net development installed ( install asp.net development in Visual Studio installer )

Angular (Install npm and run npm install -g @angular/cli ) -> open client folder and run ng serve in command line

// 
Sample appsettings

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=XXXX;Database=master;User Id=sa;password=XXXXX;"
  },
  "TokenKey": "blablablablabla bla",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

