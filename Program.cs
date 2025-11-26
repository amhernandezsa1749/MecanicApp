using MecanicApp.Api.Services;
using MecanicApp.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// =============================================
// 1. Configurar settings de MongoDB
// =============================================
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

// =============================================
// 2. Registrar servicios (UsuarioService)
// =============================================
builder.Services.AddScoped<UsuarioService>();

// =============================================
// 3. Controladores
// =============================================
builder.Services.AddControllers();

// =============================================
// 4. CORS (para que tu HTML/React pueda llamar al API)
// =============================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontPermitir", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// =============================================
// 5. Swagger (OpenAPI)
// =============================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =============================================
// 6. Middleware
// =============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontPermitir");

app.UseAuthorization();

app.MapControllers();

app.Run();
