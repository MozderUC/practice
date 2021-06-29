using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetCoreMentoring.Core.Utilities.ResultFlow;

namespace NetCoreMentoring.Core.Utilities
{
    public static class FileHelpers
    {
        private const long SizeLimit = 2097152;

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
            string fileName,
            byte[] file)
        {

            if (file.Length == 0)
            {
                return Result.Failure<byte[]>(new Error("File is empty."));
            }

            if (file.Length > SizeLimit)
            {
                return Result.Failure<byte[]>(new Error("File size more than limit."));
            }

            if (!IsValidFileExtensionAndSignature(
                fileName, file))
            {
                return Result.Failure<byte[]>(new Error("File extension is not supported"));
            }

            return Result.Success(file);
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
            IReadOnlyCollection<byte> file)
        {
            if (string.IsNullOrEmpty(fileName) || file == null || file.Count == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !FileSignature.Keys.Contains(ext))
            {
                return false;
            }

            var signatures = FileSignature[ext];

            return signatures.Any(signature =>
                file.Take(signature.Length).SequenceEqual(signature));
        }
    }
}
