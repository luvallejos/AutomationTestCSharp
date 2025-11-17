using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace AutomationTestCSharp.Utilities
{
    public static class ResourcesUtilities
    {
        #region Methods
        public static string GetEmbeddedResourceFromCommon(string namespacePath, string resourceName)
        {
            string testData;
            string path = namespacePath == "Resources" ? namespacePath : $"Resources.{namespacePath}";

            testData = GetEmbeddedResource($"{path}.{resourceName}", Assembly.GetExecutingAssembly());

            if (testData == null)
                throw new FileNotFoundException($"The embedded resource <{resourceName}> with namespacePath <{path}>, does not exist in the assembly.");
            return testData;
        }

        public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(assembly, resourceName);

            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    return null;

                using (var reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
        }

        public static T DeserializeObject<T>(string inputJsonData)
        {
            return JsonConvert.DeserializeObject<T>(inputJsonData);
        }
        #endregion
    }
}
