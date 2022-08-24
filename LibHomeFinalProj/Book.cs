namespace LibHomeFinalProj
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Authors { get; set; }

        public string PublishedDate { get; set; } = string.Empty;

        public int PageCount { get; set; }

        public string Categorie { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
       
    }
}
