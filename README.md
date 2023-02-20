# Coords (Telegram part 4)
Telegram part of microservice project.

Solution use: 
* RabbitMQ for sending communicate with another microservices.
* EF_Core for storing data.
* SignalR for sending notifications to clients ([Angular app](https://github.com/Unguryan/Coords_Angular_p3))
* Telegram.Bot for manage bot.
* Ngrok for webHook.

Full project contains:
* [Coords API (part 1)](https://github.com/Unguryan/Coords_API_p1)
* [Confirm Coords API (part 2)](https://github.com/Unguryan/Coords_Confirm_p2)
* [Angular (part 3)](https://github.com/Unguryan/Coords_Angular_p3)
* [Telegram Service (part 4)](https://github.com/Unguryan/Coords_Telegram_p4)

# Installation

Create TelegramBot and paste token to appsettings.json:

```
BotOptions.BotToken
```


* For Default install: 

```bash
PM> ngrok http 5003
```

Paste link for forwarding to appsettings.json

```
BotOptions.HostAddress
```

Then just run the project.




# For using in Docker container: 

Create network:
    
```bash
PM> docker network create coords_net 
```

Add token to ngrok in docker-compose.yml:

```
AUTHTOKEN={YOUR TOKEN}
```

Build up:
    
```bash
PM> docker-compose up
```


