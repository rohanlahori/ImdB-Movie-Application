using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Domain;
using ImdbApplication.Repository;
namespace ImdB_Application.Services.Interfaces
{
    public interface IMovieService
    {
        public void AddMovie(string movieName, Int32 yearOfRelease, string plot, List<int> actors, int producer);
        public List<MovieResponse> GetAllMovies();
        public void DeleteMovie(Int32 movieId);
    }
}
