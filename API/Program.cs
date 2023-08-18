using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BLL.DTOS.Search;
using DAL.Interfaces;
using DAL.Services;
using BLL.DTOS.Addresses;
using BLL.DTOS.Contacts;
using BLL.DTOS.Client;
using BLL.DTOS.Emails;
using BLL.DTOS.Order;
using BLL.GlobalException;
using NLog.Web;
using NLog;
using FluentValidation;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddTransient<IValidator<ClientCreateDTO>, ClientValidator>();
    var config = builder.Configuration;

    var configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .Build();


    var connectionString = configuration.GetConnectionString("EmployeeDbConnection");


    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.CreateMap<Client, ClientDTO>();
        cfg.CreateMap<Client, SearchResultDTO>();
        cfg.CreateMap<ClientAddresses, ClientAddressesDTO>();
        cfg.CreateMap<ClientContact, ClientContactDTO>();
        cfg.CreateMap<ClientEmail, ClientEmailDTO>();
        cfg.CreateMap<Order, OrderDTO>();
        cfg.CreateMap<OrderCreateDTO, OrderDTO>();
        cfg.CreateMap<ClientAddressCreateDTO, ClientAddresses>();
        cfg.CreateMap<ClientContactCreateDTO, ClientContact>();
        cfg.CreateMap<ClientEmailCreateDTO, ClientEmail>();
        cfg.CreateMap<Client, ClientDDTO>();
    });

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining(typeof(ClientValidator));
    builder.Services.AddTransient<IValidator<ClientCreateDTO>, ClientValidator>();
    builder.Services.AddTransient<IValidator<ClientAddressCreateDTO>, ClientAddressValidator>();
    builder.Services.AddTransient<IValidator<ClientEmailCreateDTO>, ClientEmailValidator>();
    builder.Services.AddTransient<IValidator<ClientContactCreateDTO>, ClientContactValidator>();
    builder.Services.AddTransient<IValidator<OrderCreateDTO>, OrderValidator>();

    builder.Services.AddScoped<ILogger<ClientAddressController>, Logger<ClientAddressController>>();
    AppDomain.CurrentDomain.GetAssemblies();



    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IOrderService, OrderService>();  
    builder.Services.AddScoped<IClientAddressService, ClientAddressService>();
    builder.Services.AddScoped<IClientContactService, ClientContactService>();
    builder.Services.AddScoped<IClientEmailService, ClientEmailService>();


    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EmployeeDbConnection")));


    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
    });
    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");

            c.OAuthClientId("swagger");
            c.OAuthAppName("Swagger UI");
        });
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw ex;
}

finally {
    LogManager.Shutdown();
}