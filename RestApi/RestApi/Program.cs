using FluentValidation;
using MediatR;
using RestApi;
using RestApi.Application;
using RestApi.Application.Validation;
using RestApi.Infrastructure;
using RestApi.Infrastructure.Initializer;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5200);
    //options.ListenAnyIP(5201, listenOptions => listenOptions.UseHttps());
});

ConfigureServices(builder);

var app = builder.Build();

ConfigurePipeline(app);

InitializeDB(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    }));

    builder.Services.AddMediatR(typeof(Program).Assembly);
    builder.Services.AddMediatR(typeof(AutoMapperProfile).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);

    builder.Services.AddInfrastructure(builder.Configuration);
}

void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseAuthorization();

    app.MapControllers();
}

void InitializeDB(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
        initializer.Initialize().GetAwaiter().GetResult();
    }
}
