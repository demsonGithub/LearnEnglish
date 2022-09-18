using Demkin.Core.Jwt;
using Demkin.System.Infrastructure;
using Demkin.System.WebApi.Extensions;
using Demkin.Utils.ContractResolver;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(options =>
{
    options.SecretKey = Configuration["JwtOptions:SecretKey"];
    options.Issuer = Configuration["JwtOptions:Issuer"];
    options.Audience = Configuration["JwtOptions:Audience"];
    options.ExpireSeconds = Convert.ToInt32(Configuration["JwtOptions:ExpireSeconds"]);
});

builder.Services.AddMediatRService();

builder.Services.AddDataBaseDomainContext(Configuration.GetValue<string>("ConnectionStrings:sqlserver1"));

builder.Services.AddRepositoriesDI();

builder.Services.AddCorsSetup();

var app = builder.Build();

var scope = app.Services.CreateScope();
SeedData.Initialize(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("LearnEnglish");

app.UseAuthorization();

app.MapControllers();

app.Run();