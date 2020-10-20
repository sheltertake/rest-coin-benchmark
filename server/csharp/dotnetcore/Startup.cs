using System;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

namespace coinapi
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Path.StartsWithSegments("/coin", StringComparison.Ordinal))
                {
                    var request = await JsonSerializer.DeserializeAsync<CoinRequest>(context.Request.Body, cancellationToken: context.RequestAborted);
                    var check = request.win ? 1 : 0;
                    var rnd = new Random().Next(0, 2);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    //context.Response.ContentLength = _bufferSize;

                    await JsonSerializer.SerializeAsync(context.Response.Body, new CoinRequest { win = check == rnd });
                }
            });
        }
    }

    public class CoinRequest
    {
        public bool win { get; set; }
    }
}
