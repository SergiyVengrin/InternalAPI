using InternalAPI.Services;
using InternalAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Register IHttpClientFactory
builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.AddScoped<IOpenAIHttpClient, OpenAiHttpClient>();
builder.Services.AddScoped<IAudioService, AudioService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin"); // Enable CORS

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Map controllers to endpoints

app.Run();