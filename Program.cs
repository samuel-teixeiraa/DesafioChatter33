using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build(); 

builder.Services.AddSignalR();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chathub");

app.Run();

public class ChatHub : Hub
{
    public async Task EnviarMensagem(string remetente, string mensagem)
    {
        await Clients.All.SendAsync("ReceberMensagem", remetente, mensagem);
    }
} 