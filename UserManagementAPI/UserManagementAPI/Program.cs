using UserManagementAPI.MiddlewareTrackers; //This is to call a custom library/script made

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionHandlingMiddleware>(); //Since this is a Middleware, it's calling the Middleware and the name of the class
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();


app.MapControllers();

app.Run();