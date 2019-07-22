using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Common
{
    public class NamedSingleton<T>
    {
        public NamedSingleton()
        {
            Instances = new ConcurrentDictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }

        public IDictionary<string, T> Instances { get; set; }

        public T Resolve(string serviceName, Func<string, T> func)
        {
            if (Instances.ContainsKey(serviceName))
            {
                return Instances[serviceName];
            }

            Instances[serviceName] = func(serviceName);
            return Instances[serviceName];
        }

        public static NamedSingleton<T> Instance = new NamedSingleton<T>();
    }
}