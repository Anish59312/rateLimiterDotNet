using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure rate limiter service
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 5; // Allow 5 requests
        options.Window = TimeSpan.FromSeconds(10); // Per 10 seconds
        options.QueueLimit = 0; // QueueLimit = 0 => reject extra request
    });
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests; // status code to 429
});

/* each segement counts the requests made,
but queued request get replished only after widow is over*/
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddSlidingWindowLimiter("slidingWindow", options =>
    {
        options.PermitLimit = 2; // Allow requests
        options.Window = TimeSpan.FromSeconds(5); // window sliding rate
        options.SegmentsPerWindow = 5;
        options.QueueLimit = 5; // QueueLimit != 0 => for sliding window
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests; // status code to 429
});

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddTokenBucketLimiter("tokenBucket", options =>
    {
        options.TokenLimit = 2; //burst capacity
        options.TokensPerPeriod = 1;
        options.ReplenishmentPeriod = TimeSpan.FromSeconds(1);
        options.QueueLimit = 5;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.AutoReplenishment = true;

    });
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests; // status code to 429
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter(); // using rate limiter

app.MapControllers(); //mapped controllers in seprate file


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//api endpoint
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireRateLimiting("fixed")
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
