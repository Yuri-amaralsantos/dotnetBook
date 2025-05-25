namespace JwtAuthApi.DTOs
{
    public class BookWithReviewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }

        public List<ReadBookDto> Reviews { get; set; } = new();
    }

    public class ReadBookDto
    {
        public int Id { get; set; }
        public int BookId { get; set; } // ✅ Adicionado
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty; 
        public string BookTitle { get; set; } = string.Empty;

    }
}
