using CoordsTelegram.API.Controllers;
using CoordsTelegram.Domain.Options;
using Microsoft.Extensions.Options;
using CoordsTelegram.TelegramBot;
using CoordsTelegram.EF_Core;
using CoordsTelegram.Infrastructure;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.Notifications;
using CoordsTelegram.Notifications.Hubs;
using CoordsTelegram.EF_Core.Dbo;
using CoordsTelegram.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEFCore(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRabbit(builder.Configuration);
builder.Services.AddTelegramBot(builder.Configuration);
builder.Services.AddNotifications(builder.Configuration);

var policyName = "defaultAngularCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder =>
    {
        builder.WithOrigins("https://localhost:44439")
            .AllowAnyMethod()
            .AllowAnyHeader()
            //.AllowAnyOrigin()
            .AllowCredentials();
    });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);


var botOptions = app.Services.GetRequiredService<IOptions<TelegramBotOptions>>();
app.MapBotWebhookRoute<BotController>(route: botOptions.Value.Route);

app.MapControllers();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    using (var db = scope.ServiceProvider.GetRequiredService<AuthLinkContext>())
    {
        await db.Database.EnsureCreatedAsync();
    }

    using (var db = scope.ServiceProvider.GetRequiredService<TelegramUserContext>())
    {
        await db.Database.EnsureCreatedAsync();
    }

    using (var db = scope.ServiceProvider.GetRequiredService<TokenContext>())
    {
        await db.Database.EnsureCreatedAsync();
    }

    using (var db = scope.ServiceProvider.GetRequiredService<TelegramChatContext>())
    {
        await db.Database.EnsureCreatedAsync();
        if(!db.Admins.Any(x => x.Key == "360607028"))
        {
            await db.Admins.AddAsync(new AdminDbo() { Key = "360607028" });
            await db.SaveChangesAsync();
        }
    }
}

app.MapHub<NotifyUsersHub>("/notificationsHub");

app.Run();
