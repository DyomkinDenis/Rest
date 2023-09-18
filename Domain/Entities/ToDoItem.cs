namespace Domain.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
