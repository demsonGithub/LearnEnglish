using Microsoft.AspNetCore.Builder;

namespace Demkin.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder InitUseDefaultMiddleware(this IApplicationBuilder app)
        {
            // 静态资源
            app.UseStaticFiles();

            // 启用跨域
            app.UseCors();

            // 授权
            app.UseAuthorization();

            return app;
        }
    }
}