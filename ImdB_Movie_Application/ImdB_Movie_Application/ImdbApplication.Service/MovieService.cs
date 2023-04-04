using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdB_Application.Services.Interfaces;
using ImdbApplication.Domain;
using ImdbApplication.Repository;
using ImdbApplication.Repository.Interfaces;

namespace ImdB_Application.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository=new MovieRepository();
        private readonly IActorServices _actorService;
        private readonly IProducerService _producerService;

        public MovieService(IActorServices actorService,IProducerService producerService)
        {
            _actorService = actorService;
            _producerService = producerService;
        }

        public void AddMovie(string name, Int32 yearOfRelease, string plot, List<int> actors,Int32 producer)
        {
            List<Actor> allActors = new List<Actor>();
            allActors = _actorService.GetAllActors();
            List<Producer> allProducers = new List<Producer>();
            allProducers = _producerService.GetAllProducers();
            
            if(name=="")
            {
                throw new Exception("Invalid name for the movie");
            }
            if(plot=="")
            {
                throw new Exception("Invalid plot for the movie");
            }
            if(actors==null || actors.Count==0)
            {
                throw new Exception("Add , seprated values for the actor");
            }
            if (producer <0)
            {
                throw new Exception("Invalid producer for the movie");
            }
            if (producer<0)
            {
                throw new Exception("Id of producer should be greater than zero");
            }
            if(yearOfRelease > DateTime.Now.Year || yearOfRelease < 1000)
            {
                throw new Exception("Invalid year of release");
            }

            foreach (Int32 id in actors)
            {
                Actor currentActor = allActors.Find(a => a.Id == id);
                if (!allActors.Contains(currentActor))
                {
                    throw new Exception("Entered actor is not present in the list");
                }
            }

           
            if (!allProducers.Contains(allProducers.Find(p => p.Id == producer)))
            {
                throw new Exception("Entered producer is not present in the list");
            }



            Movie newMovie = new Movie();
            //For assigning ID to every incoming movie
            List<Movie> allMovies = _movieRepository.GetAllMovies();
            Int32 movieId;
            if (allMovies.Count == 0)
            {
                movieId = 1;
            }
            else
            {
                movieId = allMovies.OrderByDescending(m => m.Id).Select(m => m.Id).ToList()[0]+1;
            }

            //After all the validation assigning the whole data to the newMovie object
            newMovie.Id = movieId;
            newMovie.Name= name;
            newMovie.YearOfRelease= yearOfRelease;
            newMovie.Plot= plot;
            newMovie.Producer= producer;
            newMovie.Actors= actors;
            _movieRepository.AddMovie(newMovie);
        }
       

        public List<MovieResponse>GetAllMovies()
        {
            List<Movie>allMovieList=_movieRepository.GetAllMovies();
            if (allMovieList.Count == 0) 
            {
                throw new Exception("No movie is present in the list");
            }
            List<Actor> allActorsList = _actorService.GetAllActors();
            List<Producer> allProducerList = _producerService.GetAllProducers();

            List<MovieResponse> allMovies=new List<MovieResponse>();
            foreach (Movie movie in allMovieList)
            {
                MovieResponse curMovie = new MovieResponse();
                curMovie.Name = movie.Name;
                curMovie.Id = movie.Id;
                curMovie.Plot = movie.Plot;
                curMovie.YearOfRelease = movie.YearOfRelease;
                List<Actor> allActors = new List<Actor>();

                foreach (int id in movie.Actors)
                {
                    allActors.Add(allActorsList.Find(a => a.Id == id));
                    Console.WriteLine(allActorsList.Find(a => a.Id == id));
                }

                curMovie.Actors = allActors;
                curMovie.Producer = allProducerList.Find(p => p.Id == movie.Producer);
                allMovies.Add(curMovie);

            }

            return allMovies;
        }
        public void DeleteMovie(Int32 movieId)
        {
            List<Movie> allMovies = _movieRepository.GetAllMovies();
            Movie movie=allMovies.Find(m=>m.Id==movieId);
            
            if(movie!=null) 
            {
                _movieRepository.DeleteMovie(movieId);
            }
            else
            {
                throw new Exception("Invalid Movie ID");
            }
        }
    }
}
