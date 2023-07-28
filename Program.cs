using NLog;
using NLog.Web;
using WebApplication1.Middleware;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Services;
using WebApplication1.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Insert injection for log
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//dependency injection
builder.Services.AddTransient<IEventRepo, EventRepo>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<ITicketCategoryRepo, TicketCategoryRepo>();
builder.Services.AddTransient<EventService, EventServiceImpl>();
builder.Services.AddTransient<OrderService, OrderServiceImpl>();

//builder.Services.AddTransient<..>();     //cel mai scurt lifeline, se instantiaza de fiecare data pentru o injectare
//builder.Services.AddSingleton<..>();     //se trimite mereu ac instanta de fiecare data
//builder.Services.AddScoped<..>();        //o noua instanta la fiecare request

//mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddTransient<..>();     //cel mai scurt lifeline, se instantiaza de fiecare data pentru o injectare
//builder.Services.AddSingleton<..>();     //se trimite mereu ac instanta de fiecare data
//builder.Services.AddScoped<..>();        //o noua instanta la fiecare request

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}

// "Server=localhost;Database=TicketSystem;User Id=SA;Password=reallyStrongPwd123;Encrypt=False"
// dotnet ef dbcontext scaffold "Server=localhost;Database=TicketsSystem;User Id=SA;Password=reallyStrongPwd123;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -o Models

//moq