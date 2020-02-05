using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Testnt.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Testnt.Main.Application", StringComparison.InvariantCultureIgnoreCase)).ToList();
            assemblies.ForEach(a => ApplyMappingsFromAssembly(a));
            //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
