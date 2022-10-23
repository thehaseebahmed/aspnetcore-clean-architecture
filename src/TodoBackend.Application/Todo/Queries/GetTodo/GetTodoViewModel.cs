namespace Starter.Application.Todo.Queries.GetTodo
{
    public record GetTodoViewModel
    {
        public Guid Id { get; set; }
        public bool Completed { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}