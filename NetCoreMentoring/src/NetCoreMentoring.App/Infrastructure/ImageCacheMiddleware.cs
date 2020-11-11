using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetCoreMentoring.Core;
using NetCoreMentoring.Core.Utilities;

namespace NetCoreMentoring.App.Infrastructure
{
    // I use caching with help of attribute instead
    // Don't delete this just for completing homework module
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate next;
        private static readonly Dictionary<string, string> SupportedPictureContentTypes = new Dictionary<string, string>()
        {
            {"image/jpeg", "jpeg"},
            {"image/png", "png"}
        };
        private readonly IConfiguration _configuration;

        public ImageCacheMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            // We can't read response body in middleware, problem described here:
            // https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body/43404745

            var originalBody = context.Response.Body;
            try
            {
                await using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await next(context);

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);

                if (SupportedPictureContentTypes.Keys.Contains(context.Response.ContentType ?? ""))
                {
                    if (Directory.GetFiles(Globals.CacheImagePath).Length >=
                        int.Parse(Globals.CacheImagePath)) return;

                    if (!context.Request.Query.TryGetValue("categoryId", out var categoryId)) return;

                    // Delete previously cached image for this category
                    var allCachedImages = Directory.GetFiles(_configuration["CacheImagePath"]);
                    var categoryCachedImage = allCachedImages.FirstOrDefault(f => FileHelpers.GetImageId(f) == categoryId);
                    if (!string.IsNullOrEmpty(categoryCachedImage))
                    {
                        File.Delete(categoryCachedImage);
                    }

                    // Add image in cache
                    var fileName = $"{DateTime.Now:MM-dd-yyyy}_{categoryId}.{SupportedPictureContentTypes[context.Response.ContentType]}";
                    await using var targetStream = File.Create(
                    Path.Combine(Path.Combine(Globals.CacheImagePath, fileName)));
                    
                    memStream.Position = 0;
                    await memStream.CopyToAsync(targetStream);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}