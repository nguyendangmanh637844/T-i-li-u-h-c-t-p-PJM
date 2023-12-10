using Server_side.connectionManagers;
using Server_side.Hubs;
using Server_side.IServices;
using Server_side.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Cấu hình SignalR
builder.Services.AddSignalR();
builder.Services.AddTransient<ISignalRService, SignalRService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed((host) => true) // Cho phép tất cả các origin
            .AllowCredentials());
});
builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
var app = builder.Build();

// Kích hoạt SignalR middleware
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<DemoHub>("/demo-hub");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();