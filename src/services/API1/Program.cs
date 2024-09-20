using Recilency.Core.Policies;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5005", "https://localhost:5025");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("api2", config =>
{
  config.BaseAddress = new Uri("https://localhost:5010");

})
.AddPolicyHandler(RecilencyHelper.CreateRetryPolicy(retryCount: 3, sleepDuration: TimeSpan.FromSeconds(2)))
.AddPolicyHandler(RecilencyHelper.CreateTimeoutPolicy(TimeSpan.FromSeconds(6)))
.AddPolicyHandler(RecilencyHelper.CreateCircuitBrakerPolicy(errorCount: 2, durationOfBreak: TimeSpan.FromSeconds(30)));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
