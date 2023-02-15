using CoordsTelegram.API.Controllers;
using CoordsTelegram.Domain.Options;
using Microsoft.Extensions.Options;
using CoordsTelegram.TelegramBot;
using CoordsTelegram.EF_Core;
using CoordsTelegram.Infrastructure;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEFCore(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddRabbit(builder.Configuration);
builder.Services.AddTelegramBot(builder.Configuration);
builder.Services.AddNotifications(builder.Configuration);

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
}

app.Run();
