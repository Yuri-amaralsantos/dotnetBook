namespace JwtAuthApi.Models
{
    public class ReadBook
    {
        public int Id { get; set; }

        // Relacionamentos
        public int UserId { get; set; } // <-- VOLTA pra int
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        // Dados adicionais
        public int Rating { get; set; } // Nota de 1 a 5
        public string Comment { get; set; } = string.Empty;
        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
