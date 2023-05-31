using Routing_Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<TimingMiddleWare>();
//app.UseTiming();

app.Map("/map1", HandleMapTest1);
app.Map("/map2", HandleMapTest2);

app.Map("/level1", level1App => {
    level1App.Map("/level2a", level2AApp);
    level1App.Map("/level2b", level2BApp);
});

app.UseAuthorization();

app.MapControllers();

app.Run();

static void HandleMapTest1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        app.UseTiming();
        await context.Response.WriteAsync("Map Test 1");
    });
}
static void HandleMapTest2(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 2");
    });
}


static void level2AApp(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("level2AApp");
    });
}

static void level2BApp(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("level2BApp");
    });
}
