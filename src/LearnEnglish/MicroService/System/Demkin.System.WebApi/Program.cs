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
        //修改属性名称的序列化方式，首字母小写，即驼峰样式 ,自定义long返回string防止丢失精准度
        options.SerializerSettings.ContractResolver = new CustomContractResolver();
        //如果属性名不希望驼峰样式，那就使用默认，然后在返回实体上标注，eg：[Newtonsoft.Json.JsonProperty("code")]
        //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        //忽略循环引用
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //设置时间格式
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        //忽略空值处理
        //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
    // 添加Mvc过滤器
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