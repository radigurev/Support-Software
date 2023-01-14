using AutoMapper;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using System.Reflection;

namespace DBTest3.Config
{
    public class Config
    {
        private static bool initialized;

        public static IMapper Mapper { get; set; }

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            var types = assemblies.SelectMany(n => n.GetExportedTypes()).ToList();

            var config = new MapperConfigurationExpression();
            config.CreateProfile("ReflectionProfile", configuration =>
            {

                configuration.CreateMap<User, UserVM>()
                .ForMember(dest => dest.Company, options => options.ExplicitExpansion())
                .ReverseMap();

                configuration.CreateMap<TicketStatus, TicketStatusVM>().ReverseMap();

                configuration.CreateMap<Tickets, TicketsVM>()
                .ForMember(dest => dest.Status, options => options.ExplicitExpansion())
                .ForMember(dest => dest.Worker, options => options.ExplicitExpansion())
                .ForMember(dest => dest.Client, options => options.ExplicitExpansion())
                .ReverseMap();

                configuration.CreateMap<Role, RoleVM>().ReverseMap();

                configuration.CreateMap<Projects, ProjectsVM>()
                .ForMember(dest => dest.Company, options => options.ExplicitExpansion())
                .ReverseMap();

                configuration.CreateMap<Companies, CompanyVM>()
                .ForMember(dest => dest.Projects, options => options.ExplicitExpansion())
                .ReverseMap();


                foreach (var mapping in GetToMappings(types))
                {
                    configuration.CreateMap(mapping.Source, mapping.Destination);
                }

                //IMapFrom<>
                foreach (var mapping in GetFromMappings(types))
                {
                    config.CreateMap(mapping.Source, mapping.Destination);
                }

                //ICustomMappings
                foreach (var mapping in GetCustomMappings(types))
                {
                    mapping.CreateMappings(configuration);
                }
            });
            Mapper = new Mapper(new MapperConfiguration(config));
        }

        private static IEnumerable<TypesMap> GetToMappings(IEnumerable<Type> types)
        {
            var toMappings = from t in types
                             from i in t.GetTypeInfo().GetInterfaces()
                             where i.GetTypeInfo().IsGenericType &&
                                   i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                                   !t.GetTypeInfo().IsAbstract &&
                                   !t.GetTypeInfo().IsInterface
                             select new TypesMap
                             {
                                 Source = t,
                                 Destination = i.GetTypeInfo().GetGenericArguments()[0],
                             };

            return toMappings;
        }

        private static IEnumerable<TypesMap> GetFromMappings(IEnumerable<Type> types)
        {
            var fromMappings = from t in types
                               from i in t.GetTypeInfo().GetInterfaces()
                               where i.GetTypeInfo().IsGenericType &&
                                     i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                     !t.GetTypeInfo().IsAbstract &&
                                     !t.GetTypeInfo().IsInterface
                               select new TypesMap
                               {
                                   Source = i.GetTypeInfo().GetGenericArguments()[0],
                                   Destination = t,
                               };

            return fromMappings;
        }

        private static IEnumerable<ICustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var customMaps = from t in types
                             from i in t.GetTypeInfo().GetInterfaces()
                             where typeof(ICustomMappings).GetTypeInfo().IsAssignableFrom(t) &&
                                   !t.GetTypeInfo().IsAbstract &&
                                   !t.GetTypeInfo().IsInterface
                             select (ICustomMappings)Activator.CreateInstance(t);

            return customMaps;
        }
    }
}
