using System.Security.Claims;
using HandFootLib.Models;
using HandFootLib.Services;
using HandFootLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string constr = "database=HandAndFoot;server=adubzpc; Trusted_Connection=true;TrustServerCertificate=true;";


builder.Services.AddDbContext<Data>(op =>
{
    op.UseSqlServer(constr, op =>
    {

    });
});

// Add services to the container.
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IFriendsService, FriendsService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddCors(op =>
{
    op.AddPolicy("corsettings", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var key = Encoding.ASCII.GetBytes("ThisIsASampleKeyThatIsTotallyArbitraryAsLongAsItMatchesItsPairKeyInOtherFileAndCredIsSymmetricSecurityKey");

//basic authentication scheme
//builder.Services.AddAuthentication("MyAuthenticationScheme")
//  .AddScheme<AuthenticationSchemeOptions, MyAuthenticationHandler>("MyAuthenticationScheme", null);



//JWT JSON WEB TOKEN
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = false,
           ValidateIssuerSigningKey = true,
           ValidIssuer = "",        //token emitter
           ValidAudience = "",      //token consumer
           IssuerSigningKey = new SymmetricSecurityKey(key),
       };

       options.Events = new JwtBearerEvents();

       options.Events.OnTokenValidated = context =>
       {
           //This is where we can add custom validation logic after the token has been validated
           //For example, we can check the user's role, or any other custom claim
           //If the validation fails, we can call context.Fail("Access denied") to deny access to the user
           //Or if the user's token was expired or canceled, even when the token's signature is valid, we can call context.Fail("Access denied") to deny access to the user



           ClaimsPrincipal? userPrincipal = context.Principal;


           var userID = userPrincipal?.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


           //string username = "";
           //if (userPrincipal.HasClaim(c => c.Type == ClaimTypes.Name))
           //{
           //    username = userPrincipal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
           //}



           //string email = "";
           //if (userPrincipal.HasClaim(c => c.Type == "myprop"))
           //{
           //    email = userPrincipal.Claims.First(c => c.Type == "myprop").Value;
           //}

           //Console.WriteLine(userID);

           if (userID == null || userID != "ajdubz")
           {
               context.Fail("Access denied");
           }


           return Task.CompletedTask;
       };
   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("corsettings");

app.MapControllers();

app.Run();
