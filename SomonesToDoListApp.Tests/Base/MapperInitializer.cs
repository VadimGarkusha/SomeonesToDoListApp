using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SomeonesToDoListApp.AutoMapper;

namespace AndresToDoListApp.Tests.Base
{
    [TestClass]
    public class MapperInitializer
    {
        [AssemblyInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<ToDoMappingProfile>();
            });
        }
    }
}
