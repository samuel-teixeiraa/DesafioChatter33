using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task EnviarMensagem(string remetente, string mensagem)
    {
        await Clients.All.SendAsync("ReceberMensagem", remetente, mensagem);
    }
}
