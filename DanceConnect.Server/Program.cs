using DanceConnect.Server.Authorization;
using DanceConnect.Server.Chats;
using DanceConnect.Server.DataContext;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IChatService, ChatService>();
// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<DanceConnectContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddConfigureContext(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options=> { 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Dance-Connect",
                        ValidAudience = "Dance-Connect",
                        IssuerSigningKey = JwtSecurityKey.Create("This is my secret key of Dance Connect application")
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;

                            // Check if the token is in the Authorization header
                            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                            //if (!string.IsNullOrEmpty(token))
                            //{
                            //    context.Token = token;
                            //}

                            //return Task.CompletedTask;
                        }
                    };
                });

 
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromDays(30);
//    options.Cookie.HttpOnly = true;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapHub<ChatHub>("/ChatHub");


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
