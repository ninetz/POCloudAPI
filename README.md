# POCloudAPI - <strong>Static website ( frontend ) is hosted [here](https://pocloud.azureedge.net)</strong>
<h1>API can be run on docker in Visual Studio</h1>
<h2>Front end ( Angular )  :</h2>
<ol>
  <li>Error interceptor</li>
  <li>Auth guard</li>
  <li>Models</li>
  <li>Services</li>
</ol>
<h2>Back end ( C# ASP.net ):</h2>
<ol>
  <li><h4><strong><em>Unit of work and repository pattern</em></strong></li></h4>
  <li>Upload a file (into a DB),download file and list all files that user uploaded</li>
  <li>Implementation of JWT authentication ( including verifying user's token with a DB when attempting to perform priviliged functions - for example changing the password)
</li>
  <li>Database(datacontext) used is MSSQL</li>

</ol>




<h2>What you need to build:</h2>

A valid microsoft sql server installation ( not needed if you intend to use different DB for datacontext ) -> add ConnectionString to appsettings so the API can connect to DB of your choice

Dotnet Entity-framework ( dotnet tool install --global dotnet-ef ) -> open command line in root dir of API -> dotnet ef migrations add init -> this generates and updates your DB (or optionally if you have a migrations already )dotnet ef database update 

MS visual studio with asp.net development installed ( install asp.net development in Visual Studio installer )

Angular (Install npm and run npm install -g @angular/cli ) -> open client folder -> npm install in command line ->  ng serve 

Create appsettings.json in root directory of project
<h2>Sample appsettings</h2>

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

