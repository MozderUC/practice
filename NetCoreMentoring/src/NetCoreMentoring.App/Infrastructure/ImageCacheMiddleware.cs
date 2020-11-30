using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Utilities;

namespace NetCoreMentoring.App.Infrastructure
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        private bool _isMiddlewareApplicable;
        private string _cacheImagePath;


        public ImageCacheMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            _cacheImagePath = _configuration["CacheImagePath"];
            _isMiddlewareApplicable = true;

            // check if we can apply caching
            if (!context.Request.Query.TryGetValue("format", out var format)) _isMiddlewareApplicable = false;
            if (format.ToString() != AcceptFormatTypes.Picture.ToString()) _isMiddlewareApplicable = false;
            if (!context.Request.Query.TryGetValue("categoryId", out var categoryId)) _isMiddlewareApplicable = false;
            if (!Directory.Exists(_cacheImagePath)) _isMiddlewareApplicable = false;

            if (!_isMiddlewareApplicable)
            {
                await _next(context);
            }

            if (_isMiddlewareApplicable)
            {
                // read from cache
                var cachedFiles = Directory.GetFiles(_cacheImagePath);
                var filePath = cachedFiles.FirstOrDefault(c => FileHelpers.GetImageId(c) == categoryId);

                if (filePath != null)
                {
                    await context.Response.SendFileAsync(filePath);

                    // update file name, so we can to scan and clean-up unused cache files by date stamp in file name
                    var newFileName = $"{DateTime.Now:MM-dd-yyyy}_{categoryId}.jpeg";
                    File.Move(filePath, Path.Combine(_cacheImagePath, newFileName));

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

                    // Delete previously cached image for this category
                    var allCachedImages = Directory.GetFiles(_configuration["CacheImagePath"]);
                    var categoryCachedImage = allCachedImages.FirstOrDefault(f => FileHelpers.GetImageId(f) == categoryId);
                    if (!string.IsNullOrEmpty(categoryCachedImage))
                    {
                        File.Delete(categoryCachedImage);
                    }

                    // write to cache
                    var fileName = $"{DateTime.Now:MM-dd-yyyy}_{categoryId}.jpeg";
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