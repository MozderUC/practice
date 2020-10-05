using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ORMs.Utils
{
    public static class ScriptLoader
    { 
        public static string GetEmbeddedResourceByPath(string path, [CallerMemberName] string resourceName = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream manifestResourceStream = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + path + "." + resourceName + ".sql"))
            {
                if (manifestResourceStream == null)
                    throw new ArgumentException("Resource not found", resourceName);
                using (StreamReader streamReader = new StreamReader(manifestResourceStream))
                    return streamReader.ReadToEnd();
            }
        }
    }
}
