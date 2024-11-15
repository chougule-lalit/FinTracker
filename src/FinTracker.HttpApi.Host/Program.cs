using FinTracker.HttpApi.Host;

var builder = WebApplication.CreateBuilder(args);

// Configure services
FinTrackerHttpApiHostModule.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
FinTrackerHttpApiHostModule.Configure(app, app.Environment);

app.Run();