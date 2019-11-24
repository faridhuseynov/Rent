using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ServiceLayers
{
    public static class MapperExtensions
    {
        private static void IgnoreUnmappedProperties(TypeMap map, IMappingExpression expression)
        {
            foreach (var propName in map.GetUnmappedPropertyNames())
            {
                if (map.SourceType.GetProperty(propName)!=null)
                {
                    expression.ForSourceMember(propName, opt => opt.DoNotValidate());
                }
                if (map.DestinationType.GetProperty(propName) != null)
                {
                    expression.ForMember(propName, opt => opt.Ignore());
                }
            }
        }
        public static void IgnoreUnmapped(this IProfileExpression profile)
        {
            profile.ForAllMaps(IgnoreUnmappedProperties);
        }
    }
}
