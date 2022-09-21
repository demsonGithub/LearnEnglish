using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                                .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration)
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureInitService();

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        //�޸��������Ƶ����л���ʽ������ĸСд�����շ���ʽ ,�Զ���long����string��ֹ��ʧ��׼��
        options.SerializerSettings.ContractResolver = new CustomContractResolver();
        //�����������ϣ���շ���ʽ���Ǿ�ʹ��Ĭ�ϣ�Ȼ���ڷ���ʵ���ϱ�ע��eg��[Newtonsoft.Json.JsonProperty("code")]
        //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        //����ѭ������
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //����ʱ���ʽ
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        //���Կ�ֵ����
        //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
    // ���Mvc������
    builder.Services.Configure<MvcOptions>(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbSetup(Configuration.GetValue<string>("ConnectionStrings:sqlserver"));

    //builder.Services.AddScoped<SystemDomainService>();

    builder.Services.AddCorsSetup();

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    SeedData.Initialize(scope.ServiceProvider);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "System Service");
            c.RoutePrefix = "api";
        });
    }
    app.UseCors("LearnEnglish");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}