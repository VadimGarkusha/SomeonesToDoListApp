using AutoMapper;

namespace SomeonesToDoListApp.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            // Initializing the AutoMapper configuration for the to do mappings
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<ToDoMappingProfile>();
            });
        }
    }
}