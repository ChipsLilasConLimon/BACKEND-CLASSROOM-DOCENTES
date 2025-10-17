using AsistenciasyAlumnos.Jwt;
using AsistenciasyAlumnos.Repositorios;
using AsistenciasyAlumnos.Servicios;
using Microsoft.IdentityModel.Tokens;
using TextAPI_D311.Middlewares;
using TextAPI_D311.Repositorios;
using TextAPI_D311.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IAsistenciaServicio, AsistenciaServicio>();
builder.Services.AddTransient<IAsistenciaRepositorio, AsistenciaRepositorio>();

builder.Services.AddTransient<ITareaServicio, TareaServicio>();
builder.Services.AddTransient<ITareaRepositorio, TareaRepositorio>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthRepositorio, AuthRepositorio>();

builder.Services.AddTransient<IDatosUsuariosServicio, DatosUsuariosServicio>();
builder.Services.AddTransient<IDatosUsuariosRepositorio, DatosUsuarioRepositorio>();

builder.Services.AddTransient<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();

builder.Services.AddTransient<IGruposRepositorio, GruposRepositorio>();
builder.Services.AddTransient<IGruposService, GruposService>();

builder.Services.AddTransient<IJwtService, JwtService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<MiddlewareExcepcion>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();  
app.UseAuthorization(); 

app.MapControllers();

app.Run();
