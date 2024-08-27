using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Referral API", Version = "v1" });

            var xmlFile = "ReferralAPI.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
builder.Services.AddAuthorization();

var signingKey = Environment.GetEnvironmentVariable("SIGNING_KEY") ?? ""; //TODO: Add signing key
if(string.IsNullOrWhiteSpace(signingKey)){
    throw new Exception("You forgot to add the signing key, please add env variable SIGNING_KEY or manually insert it in Program.cs");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "pmanacorda@dev",
            ValidateAudience = true,
            ValidAudience = "LiveFront",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)), 
            RequireSignedTokens = true
        };
    });

builder.Services.AddSingleton<IReferralService, ReferralService>();
builder.Services.AddSingleton<IReferralRepository, ReferralRespository>();
builder.Services.AddSingleton<IDeepLinkService, DeepLinkService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Referral API"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();