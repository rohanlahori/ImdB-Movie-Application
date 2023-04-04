using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Domain;
namespace ImdbApplication.Repository.Interfaces
{
    public interface IMovieRepository
    {
        public void AddMovie(Movie movie);
        public List<Movie> GetAllMovies();

        public void DeleteMovie(int MovieId);
    }
}
