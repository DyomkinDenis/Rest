namespace Domain.Entities
{
    public class ToDoList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public IList<ToDoItem> Items { get; set; } = new List<ToDoItem>();
    }
}
