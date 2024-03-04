namespace Core.Entities
{
    public class Series : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}
