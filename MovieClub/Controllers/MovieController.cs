using Microsoft.AspNetCore.Mvc;
using MovieClub.Data;
using MovieClub.Models;
namespace MovieClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("movies")]
        public ActionResult<IEnumerable<Movie>> GetMovies([FromQuery] string? genre, [FromQuery] string? addedBy)
        {
            var movies = _context.Movies.AsQueryable();

            if (genre is not null)
                movies = movies.Where(m => m.Genre == genre);

            if (addedBy is not null)
                movies = movies.Where(m => m.AddedBy == addedBy);

            return Ok(movies.OrderByDescending(m => m.Votes));
        }

        [HttpGet("movies/{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie is null)
                return NotFound();
            return Ok(movie);
        }

        [HttpGet("movies/unwatched")]
        public ActionResult<IEnumerable<Movie>> GetUnwatchedMovies()
        {
            var movies = _context.Movies.Where(m => !m.IsWatched).OrderByDescending(m => m.Votes);
            return Ok(movies);
        }

        [HttpGet("api/movies/winner")]
        public ActionResult<Movie> GetWinningMovie()
        {
            var winningMovie = _context.Movies.OrderByDescending(m => m.Votes).FirstOrDefault();
            if (winningMovie is null)
                return NotFound();
            return Ok(winningMovie);
        }


        [HttpPost("movies")]
        public ActionResult<Movie> AddMovie([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
        }

        [HttpPost("movies/{id}/vote")]
        public ActionResult VoteForMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie is null)
                return NotFound();
            movie.Votes++;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("movies/{id}")]
        public ActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            var movie = _context.Movies.Find(id);
            if (movie is null)
                return NotFound();
            movie.Title = updatedMovie.Title;
            movie.Genre = updatedMovie.Genre;
            movie.AddedBy = updatedMovie.AddedBy;
            movie.IsWatched = updatedMovie.IsWatched;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("movies/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie is null)
                return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
