using Application.Common.Mapping;
using AutoMapper;

namespace Application.ToDo.Commands.Queries.GetToDoItemDetail
{
    public class ToDoItemVM : IMapWith<Domain.Entities.ToDoItem>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.ToDoItem, ToDoItemVM>()
                .ForMember(toDoItemVM => toDoItemVM.Name,
                    opt=>opt.MapFrom(toDo=>toDo.Name))
                .ForMember(toDoItemVM => toDoItemVM.Id,
                    opt => opt.MapFrom(toDo => toDo.Id));
        }
    }
}
