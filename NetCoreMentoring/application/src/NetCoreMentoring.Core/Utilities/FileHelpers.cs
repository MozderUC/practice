using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using NetCoreMentoring.Core.Utilities.ResultFlow;

namespace NetCoreMentoring.Core.Utilities
{
    public static class FileHelpers
    {
        private static readonly long SizeLimit = 2097152;
        private static readonly string[] PermittedExtensions = {".png", ".jpeg", ".jpg"};
        private static readonly Dictionary<string, List<byte[]>> FileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            }
        };

        public static Result<byte[]> ProcessFormFile(
            IFormFile formFile)
        {
            var fileName = WebUtility.HtmlEncode(
                formFile.FileName);

            if (formFile.Length == 0)
            {
                return Result.Failure<byte[]>(new Error("File is empty."));
            }

            if (formFile.Length > SizeLimit)
            {
                return Result.Failure<byte[]>(new Error("File size more than limit."));
            }

            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);

            if (memoryStream.Length == 0)
            {
                return Result.Failure<byte[]>(new Error("File is empty."));
            }

            if (!IsValidFileExtensionAndSignature(
                formFile.FileName, memoryStream, PermittedExtensions))
            {
                return Result.Failure<byte[]>(new Error("File extension is not supported"));
            }

            return Result.Success(memoryStream.ToArray());
        }

        public static string GetImageId(string imagePath)
        {
            return Path.GetFileName(imagePath).Split('_')[1].Split('.')[0];
        }

        public static DateTime GetTimeStampForCachedFile(string imagePath)
        {
            return DateTime.ParseExact(
                Path.GetFileName(imagePath).Split('_')[0],
                Constants.CacheTimeStampFormat,
                null);
        }

        private static bool IsValidFileExtensionAndSignature(
            string fileName,
            Stream data,
            string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }

            data.Position = 0;

            using var reader = new BinaryReader(data);

            var signatures = FileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            return signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
    }
}
