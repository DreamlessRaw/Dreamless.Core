using AutoMapper;
using Fissoft.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamless.Core
{ 
    public class MapperUtils
    {
        public static IMapper Mapper => Configuration.CreateMapper();
        public static MapperConfiguration Configuration { get; set; }
         
        public static TDestination Map<TDestination>(object source)
        {
            return Configuration.CreateMapper().Map<TDestination>(source);
        }

        public static void Config(MapperConfiguration config)
        {
            Configuration = config;
        }
    }
}
