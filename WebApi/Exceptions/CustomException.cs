using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi.Exceptions
{
    public static class CustomException
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool internalServerErrorWithException)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var exception = contextFeature.Error.InnerException ?? contextFeature.Error;

                    var operationResult = new Exception("Internal Error: " + exception);

                    string json = JsonConvert.SerializeObject(operationResult,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                            Formatting = Formatting.Indented,
                            NullValueHandling = NullValueHandling.Ignore
                        });

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
