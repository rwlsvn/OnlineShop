﻿using AutoMapper;
using System.Reflection;

namespace OnlineShop.Library.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingFromAssembly(assembly);
        }

        private void ApplyMappingFromAssembly(Assembly assemly)
        {
            var types = assemly.GetExportedTypes()
                .Where(_ => _.GetInterfaces().Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>))).ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
