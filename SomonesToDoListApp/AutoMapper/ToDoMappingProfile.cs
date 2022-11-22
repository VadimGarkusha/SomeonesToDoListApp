using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.ViewModels;

namespace SomeonesToDoListApp.AutoMapper
{
    public class ToDoMappingProfile : Profile
    {

        public ToDoMappingProfile()
        {
            CreateMap<ToDo, ToDoViewModel>();
        }
    }
}