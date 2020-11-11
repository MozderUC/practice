using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreMentoring.Core;
using NetCoreMentoring.Core.Utilities;

namespace NetCoreMentoring.App.Infrastructure
{
    public class ImageCacheAttribute : Attribute, IResourceFilter
    {
        // read cache
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Query.TryGetValue("categoryId", out var categoryId)) return;

            var cachedFiles = Directory.GetFiles(Globals.CacheImagePath);
            var filePath = cachedFiles.FirstOrDefault(c => FileHelpers.GetImageId(c) == categoryId);

            if (filePath != null)
            {
                context.Result = new FileContentResult(File.ReadAllBytes(filePath), "image/jpeg");

                // update file name, so we can to scan and clean-up unused cache files by date stamp in file name
                var newFileName = $"{DateTime.Now:MM-dd-yyyy}_{categoryId}.jpeg";
                File.Move(filePath, Path.Combine(Globals.CacheImagePath, newFileName));
            }
        }

        // update cache
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!context.HttpContext.Request.Query.TryGetValue("categoryId", out var categoryId)) return;
            if (!(context.Result is FileContentResult)) return;

            var result = (FileContentResult)context.Result;
            var fileName = $"{DateTime.Now:MM-dd-yyyy}_{categoryId}.jpeg";
            File.WriteAllBytes(
                Path.Combine(Path.Combine(Globals.CacheImagePath, fileName)), result.FileContents);
        }
    }
}
