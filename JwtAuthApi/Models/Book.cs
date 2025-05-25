namespace JwtAuthApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }

        public string? ImageUrl { get; set; }

        // Relacionamento reverso
        public List<ReadBook> Reviews { get; set; } = new();
    }
}
