namespace MovieClub.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string AddedBy { get; set; }
        public int Votes { get; set; }
        public bool IsWatched { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
