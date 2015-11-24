using System;
using System.Reflection;
using AutoMapper;
using Rc.Components.Extensions;

namespace Rc.Core.Mapper
{
    internal static class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        public static void CreateMap<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.GetTypeInfo().IsDefined(typeof (TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetTypeInfo().GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        AutoMapper.Mapper.CreateMap(type, targetType);
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        AutoMapper.Mapper.CreateMap(targetType, type);                                
                    }
                }
            }
        }
    }
}