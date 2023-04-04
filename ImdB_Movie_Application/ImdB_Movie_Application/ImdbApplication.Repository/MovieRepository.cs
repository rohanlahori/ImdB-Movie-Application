using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Repository.Interfaces;
using ImdbApplication.Domain;
namespace ImdbApplication.Repository
{
    public class MovieRepository : IMovieRepository
    {
        List<Movie> _movies = new List<Movie>()
        {
        };
        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }
        public List<Movie> GetAllMovies() 
        {
            return _movies.ToList();
        }
        public void DeleteMovie(int movieId)
        {
            Movie movie= _movies.Find(m=>m.Id==movieId); 
            _movies.Remove(movie);
        }
    }
}

//new Movie()
//{
//    MovieId = 100,
//    MovieName = "Shershah",
//    YearOfRelease = 2020,
//    Plot = "Army",
//    Actors = new List<int> { 1 },
//    Producer = 1
//}