using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using ImdbApplication;
using Newtonsoft.Json;
using NUnit.Framework;
using ImdbApplication.Domain;
using ImdB_Application.Services;
using ImdB_Application.Services.Interfaces;
using System.Collections.Generic;

namespace ImdbApplication.Tests.StepDefinitions
{
    [Binding]
    public class Imdb_ApplicationStepDefinitions
    {
        static IActorServices _actorService=new ActorService();
        static IProducerService _producerService=new ProducerService();
        private IMovieService _movieService = new MovieService(_actorService, _producerService);
        private string movieName, plot;
        private Int32 releaseYear=-1;
        private List<int> actors;
        private Int32 producer=-1;
        string exceptionMessage;
        Int32 deleteMovieId;
        private List<MovieResponse> allMovieList = new List<MovieResponse>();
        
        //Add Movie Steps
        [Given(@"user provides the inputs for the movie")]
        public void GivenUserProvidesTheInputsForTheMovie(Table table)
        {
           
            movieName = table.Rows[0]["MovieName"];
            releaseYear = Convert.ToInt32(table.Rows[0]["YearOfRelease"]);
            plot = table.Rows[0]["Plot"];
            actors = table.Rows[0]["Actors"].Split(',').Select(Int32.Parse).ToList();
            producer = Convert.ToInt32(table.Rows[0]["Producer"]);
        }

        [When(@"Add Movie to the database")]
        public void WhenAddMovieToTheDatabase()
        {
            try
            {
                _movieService.AddMovie(movieName, releaseYear, plot, actors, producer);
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }

        }

        
        //List Movie Step
        [When(@"List all the movies stored in the database")]
        public void WhenListAllTheMoviesStoredInTheDatabase()
        {
            allMovieList = _movieService.GetAllMovies();
        }
        

        //Delete Movie Steps
        [Given(@"the user provides id of the movie to be deleted as (.*)")]
        public void GivenTheUserProvidesIdOfTheMovieToBeDeletedAs(Int32 id)
        {
            deleteMovieId = id;
        }

        [When(@"Delete the movie from the database")]
        public void WhenDeleteTheMovieFromTheDatabase()
        {
            try
            {
                _movieService.DeleteMovie(deleteMovieId);
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }
        }

        
        //common step for comparing the expected and actual values
        [Then(@"Movie List would look like '([^']*)'")]
        public void ThenMovieListWouldLookLike(string expectedMovie)
        {
            var actualMovie = JsonConvert.SerializeObject(_movieService.GetAllMovies());
            Assert.AreEqual(actualMovie, expectedMovie);
        }

        
        //Invalid Cases handling
        [Then(@"error with message ""([^""]*)"" would be shown")]
        public void ThenErrorWithMessageWouldBeShown(string expectedMessage)
        {
            Assert.AreEqual(exceptionMessage, expectedMessage);
        }

        [Given(@"user provides the inputs for the movie '([^']*)','([^']*)','([^']*)','([^']*)','([^']*)'")]
        public void GivenUserProvidesTheInputsForTheMovie(string name, string year, string movieplot, string aid, string pid)
        {
            movieName = name;
            if (year != "")
            {
                releaseYear = Convert.ToInt32(year);
            }
            plot = movieplot;
            if (aid != "")
            {
                actors = aid.Split(',').Select(int.Parse).ToList();
            }

            if (pid != "")
            {
                producer = Convert.ToInt32(pid);
            }

        }

        [Then(@"error with the message '([^']*)'would be shown")]
        public void ThenErrorWithTheMessageWouldBeShown(string message)
        {
            Assert.AreEqual(message,exceptionMessage);

        }



        [BeforeScenario("listMovie","deleteMovie","addMovie")]
        public void AddMovie()
        {
            List<int> actors = new List<int> { 1, 2 };
            _actorService.AddActor("Ranbir Kapoor", "10/20/1990");
            _actorService.AddActor("Ranbir Singh", "11/21/1994");
            _producerService.AddProducer("Ajay Kapoor","10/21/1980");
            _movieService.AddMovie("Pathan", 2020, "Fun", actors, 1);
            _movieService.AddMovie("Shershah", 2021, "Army", actors, 1);
        }

    }
}