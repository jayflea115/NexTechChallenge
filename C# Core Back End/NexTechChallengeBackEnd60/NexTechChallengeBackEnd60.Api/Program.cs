var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<NexTechChallengeBackEnd.Api.Repositories.IStoryRepository, NexTechChallengeBackEnd.Api.Repositories.StoryRepository>();
builder.Services.AddControllers();
builder.Services.AddHttpClient<NexTechChallengeBackEnd.Api.Repositories.CommunicationClient>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c => 
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
