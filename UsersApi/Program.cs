using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersApi.Authorization;
using UsersApi.Data;
using UsersApi.Model;
using UsersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. VARIÁVEIS DE AMBIENTE / APPSETTINGS
// ==========================================
var securityKey = builder.Configuration["SymmetricSecurityKey"];
var connectionString = builder.Configuration.GetConnectionString("UserConnection");

// ==========================================
// 2. BANCO DE DADOS E IDENTITY
// ==========================================
builder.Services.AddDbContext<UserDbContext>(opts =>
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

// ==========================================
// 3. SEGURANÇA (AUTENTICAÇÃO E AUTORIZAÇÃO)
// ==========================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy => policy.AddRequirements(new IdadeMinima(18)));
});
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// ==========================================
// 4. INJEÇÃO DE DEPENDÊNCIA (SEUS SERVIÇOS)
// ==========================================
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

// ==========================================
// 5. CONFIGURAÇÕES DA API E FERRAMENTAS
// ==========================================
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// PIPELINE DE REQUISIÇÃO (MIDDLEWARES)
// ==========================================
var app = builder.Build();

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