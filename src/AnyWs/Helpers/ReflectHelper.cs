using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace AnyWs.Helpers
{
    public class ReflectHelper
    {
        public ReflectHelper()
        {
            LoadedAssemblies = new ConcurrentDictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);
        }

        public IDictionary<string, Assembly> LoadedAssemblies { get; set; }

        public object CreateInstance(string dllPath, string typeName)
        {
            if (!LoadedAssemblies.ContainsKey(dllPath))
            {
                LoadedAssemblies.Add(dllPath, Assembly.LoadFile(dllPath));
            }

            LoadedAssemblies.TryGetValue(dllPath, out var assembly);
            if (assembly == null)
            {
                return null;
            }
            var theType = assembly.GetType(typeName);
            return Activator.CreateInstance(theType);
        }
        
        public static ReflectHelper Instance = new ReflectHelper();
    }
}
