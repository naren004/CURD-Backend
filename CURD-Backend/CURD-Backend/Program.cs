using WebApiMongoDbDemo.Data;
using MongoDB.Driver;

// Hash password before saving

var builder = WebApplication.CreateBuilder(args);

// Get MongoDB connection string using the key "MongoDb"
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");

// Register MongoClient as a singleton using the correct connection string
builder.Services.AddSingleton<MongoClient>(sp => new MongoClient(mongoConnectionString));

// Register MongoDbService as a singleton
builder.Services.AddSingleton<MongoDbService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:7145") // Allow frontend URL
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); // Allow cookies/auth
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
