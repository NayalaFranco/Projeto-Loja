using Loja.API.Extensions.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Loja.API.Extensions
{
    // Configuração de exceção global
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,

                            // Comentado para não retornar o StackTrace ao cliente
                            // Descomente aqui e na model ErrorDetails para voltar
                            // a enviar o StackTrace caso seja necessário.
                            //Trace = contextFeature.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
