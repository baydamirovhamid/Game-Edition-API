using game.edition.api.Extensions;
using game.edition.api.Infrastructure;
using game.edition.api.Infrastructure.Repository;
using game.edition.api.Models;
using game.edition.api.Services.Implementation;
using game.edition.api.Services.Interface;
using Microsoft.AspNetCore.Identity;
using game.payment.service.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));
builder.Services.ConfigureCors();
builder.Services.ConfigureJWTService();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDataProtectionTokenProvider();

builder.Services.AddTransient<ISqlService, SqlService>();
builder.Services.AddTransient<ICmdService, CmdService>();
builder.Services.AddTransient<IJwtHandler, JwtHandler>();
builder.Services.AddTransient<ILookupService, LookupService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IValidationCommon, ValidationCommon>();
//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddDbContext<GameEditionDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<USER, USER_ROLE>(options =>
{

})
.AddRoles<USER_ROLE>()
.AddEntityFrameworkStores<GameEditionDbContext>()
.AddDefaultTokenProviders();

//Logs
//builder.Host.ConfigureLogging((hostingContext, logging) =>
//{
//    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//    logging.AddDebug();
//    logging.AddNLog();
//});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.MapControllers();
app.Run();
