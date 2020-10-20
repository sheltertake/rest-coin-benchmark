using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace coinapi
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/coin", async context =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    //var request = await JsonSerializer.DeserializeAsync<CoinRequest>(context.Request.Body, cancellationToken: context.RequestAborted);
                    var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    var request = JsonConvert.DeserializeObject<CoinRequest>(json);

                    //var result = JsonSerializer.Serialize(new CoinRequest { 
                    var result = JsonConvert.SerializeObject(new CoinRequest
                    {
                        win = request.win == new Random().NextDouble() >= 0.5
                    });
                    await context.Response.WriteAsync(result);
                });
            });
        }
    }

    public class CoinRequest
    {
        public bool win { get; set; }
    }
}
