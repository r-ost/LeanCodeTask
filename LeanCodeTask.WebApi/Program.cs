var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/random", () => "Hello World random!");
app.MapGet("/history", () => "Hello World history!");

app.Run();
