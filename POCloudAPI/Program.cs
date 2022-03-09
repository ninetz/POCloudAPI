using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using POCloudAPI.Data;
using POCloudAPI.Entities;
using POCloudAPI.Extensions;
using POCloudAPI.Middleware;

//TODO add certificates component ( CISCO, python etc )
// todo add CV component ( maybe PDF displayer or something? )
// TODO add file upload - done 
// TODO add file removal -> create get for all file names and icons in API, then verify user identity (check if user owns file
// -> userid = select (id) from users where id = userid and and filename == filename from Angular ) 
// TODO add file upload
// TODO 
// TODO add JWToken database verification - done
var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
});

// Configure the HTTP request pipeline

var app = builder.Build();


    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    //var userManager = services.GetRequiredService<UserManager<APIUser>>();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();