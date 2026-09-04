var builder = WebApplication.CreateBuilder(args);

// 1. Configuração obrigatória da porta para o Render funcionar
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// 2. Adicione os serviços ANTES do builder.Build()
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Configurações de Middlewares e Arquivos Estáticos DEPOIS do builder.Build()
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Mapeamento de Rotas e do Hub do SignalR
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ativando o endpoint do SignalR corretamente:
app.MapHub<ChatHub>("/chatHub");

app.Run();
