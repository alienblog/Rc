using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Rc.Core.Mapper
{
    public class MapperBootstrapper
    {
        public static void ConfigureMapper(Type[] types)
        {
            var mapTypes = types.Where(t=>IsDefined(t,typeof(AutoMapAttribute))
                                    ||IsDefined(t,typeof(AutoMapFromAttribute))
                                    ||IsDefined(t,typeof(AutoMapToAttribute)));
                                    
            foreach (var type in mapTypes)
            {
                MapType(type);
            }
        }
        
        private static bool IsDefined(Type type,Type attributeType)
        {
            return type.GetTypeInfo().IsDefined(attributeType);
        }
        
        private static void MapType(Type type)
        {
            AutoMapperHelper.CreateMap(type);
        }
    }
}