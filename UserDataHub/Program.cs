using RabbitMQ.Client;
using UserDataHub.WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

//Register RabbitMQ
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
var factory = new ConnectionFactory()
{
    HostName = rabbitMQSettings.HostName,
    Port = rabbitMQSettings.Port,
    UserName = rabbitMQSettings.UserName,
    Password = rabbitMQSettings.Password,
    VirtualHost = rabbitMQSettings.VirtualHost
};

builder.Services.AddSingleton(factory);
builder.Services.AddSingleton(sp => new RabbitMQService(sp.GetRequiredService<ConnectionFactory>(), "UserRegistrationQueue"));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
