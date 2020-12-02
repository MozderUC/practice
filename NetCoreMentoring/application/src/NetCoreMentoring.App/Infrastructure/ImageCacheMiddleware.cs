using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core;
using NetCoreMentoring.Core.Utilities;

namespace NetCoreMentoring.App.Infrastructure
{
    // I use caching with help of attribute instead
    // I didn't delete it just to complete the module's homework
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly CachingOptions _cachingOptions;

        public ImageCacheMiddleware(RequestDelegate next, IConfiguration configuration, CachingOptions cachingOptions)
        {
            _next = next;
            _configuration = configuration;
            _cachingOptions = cachingOptions;
        }

        public async Task Invoke(HttpContext context)
        {
            var _cacheImagePath = _configuration["CacheImagePath"];
            var isMiddlewareApplicable = _cachingOptions.CachedCodePath.TryGetValue(context.Request.Path, out var cachedKey);

            // check if we can apply caching
            if (!context.Request.Query.TryGetValue(cachedKey ?? "", out var imageId)) isMiddlewareApplicable = false;
            if (!Directory.Exists(_cacheImagePath)) isMiddlewareApplicable = false;
            if (!context.Request.Query.TryGetValue("format", out var format) ||
                format.ToString() != AcceptFormatTypes.Picture.ToString()) isMiddlewareApplicable = false;


            if (!isMiddlewareApplicable)
            {
                await _next(context);
            }

            if (isMiddlewareApplicable)
            {
                // read from cache
                var cacheFilePath = Directory.GetFiles(_cacheImagePath)
                    .Where(c => FileHelpers.GetImageId(c) == imageId)
                    .OrderBy(FileHelpers.GetTimeStampForCachedFile)
                    .FirstOrDefault();

                if (cacheFilePath != null)
                {
                    await context.Response.SendFileAsync(cacheFilePath);
                    return;
                }

                // We can't read response body in middle-ware, problem described here:
                // https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body/43404745
                var originalBody = context.Response.Body;
                try
                {
                    await using var memStream = new MemoryStream();
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);

                    // write to cache
                    var fileName = $"{DateTime.Now.ToString(Constants.CacheTimeStampFormat)}_{imageId}.jpeg";

                    await using var targetStream = File.Create(Path.Combine(_cacheImagePath, fileName));
                    memStream.Position = 0;
                    await memStream.CopyToAsync(targetStream);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
        }
    }
}