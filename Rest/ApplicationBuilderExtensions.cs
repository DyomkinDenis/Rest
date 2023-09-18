using Domain.Entities;
using Infrastructure;

namespace MyCleanArchitecture
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;
            var context = serviceProvider.GetRequiredService
                <ApplicationDbContext>();

            var lists = new List<ToDoList>
            {
                new ToDoList
                {
                    Name = "FirstList",
                    Description = "First part of tasks",
                    IsDeleted = false,
                    Items = new List<ToDoItem>()
                    {
                        new ToDoItem { Name = "Купить продукты", Description  = "Составить список покупок и купить необходимые продукты"},
                        new ToDoItem { Name = "Уборка дома", Description  = "Провести уборку в комнатах, вымыть полы и вынести мусор"},
                        new ToDoItem { Name = "Приготовить обед", Description  = "Подготовить вкусный обед из доступных продуктов"},
                    }
                },
                new ToDoList
                {
                    Name = "SecondList",
                    Description = "Second part of tasks",
                    IsDeleted = false,
                    Items = new List<ToDoItem>()
                    {
                        new ToDoItem { Name = "Погладить белье", Description = "Погладить накопившееся белье и разложить его"},
                        new ToDoItem { Name = "Полить цветы", Description = "Полить все растения в горшках и проверить их состояние"},
                        new ToDoItem { Name = "Заказать пиццу", Description = "Выбрать пиццу по меню и заказать доставку на ужин"}
                    }
                },
                new ToDoList
                {
                    Name = "ThirdList",
                    Description = "Third part of tasks",
                    IsDeleted = false,
                    Items = new List<ToDoItem>()
                    {
                        new ToDoItem { Name = "Подготовить одежду", Description = "Выбрать одежду на завтра и подготовить её заранее."},
                        new ToDoItem { Name = "Забрать посылку", Description = "Забрать посылку из почтового отделения или пункта выдачи"},
                        new ToDoItem { Name = "Заказать такси", Description = "Заказать такси для поездки"}
                    }
                }
            };
            context.TodoLists.AddRange(lists);
            context.SaveChanges();

            return app;
        }
    }
}
