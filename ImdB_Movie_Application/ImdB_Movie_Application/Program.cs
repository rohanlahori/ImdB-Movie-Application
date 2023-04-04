using ImdB_Application.Services;
using ImdbApplication.Domain;
using System.Globalization;
using System;
using System.Reflection;
using ImdbApplication.Repository;
using ImdB_Application.Services.Interfaces;

namespace ImdB_Movie_Application
{
    public class Program
    {
        const Int32 Exit= 6;
        static void Main(string[] args)
        {
            IActorServices _actorService = new ActorService();
            IProducerService _producerService = new ProducerService();
            var movieService = new MovieService(_actorService,_producerService);
            Int32 option = 0;
            while (option != Exit)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Select the option from the given below");
                    Console.WriteLine("1: List Movie");
                    Console.WriteLine("2: Add Movie");
                    Console.WriteLine("3: Add Actor");
                    Console.WriteLine("4: Add Producer");
                    Console.WriteLine("5: Delete Movie");
                    Console.WriteLine("6: Exit");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        //TODO Change the response ans and remove exception here make function 
                        case 1:
                            ListMovie(movieService);
                            break;
                        case 2:
                            AddMovie(_actorService, _producerService, movieService);
                            break;
                        case 3:
                            AddActor(_actorService);
                            break;
                        case 4:
                            AddProducer(_producerService);
                            break;
                        case 5:
                            DeleteMovie(movieService);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void DeleteMovie(MovieService movieService)
        {
            string str;
            try
            {
                List<MovieResponse> movieList = movieService.GetAllMovies();
                if (movieList.Count == 0)
                {
                    Console.WriteLine("No movie in the list to delete");
                }
                else
                {
                    Console.WriteLine("Movie Id      Movie Name");
                    foreach (MovieResponse movie in movieList)
                    {
                        Console.WriteLine(movie.Id + "      " + movie.Name);
                    }


                    Console.WriteLine("Enter the id of the movie you want to delete");
                    Int32 movieId;
                    string idInput = Console.ReadLine();
                    while (string.IsNullOrEmpty(idInput) || !idInput.All(char.IsDigit))
                    {
                        Console.WriteLine("Enter id in proper format");
                        str = Console.ReadLine();
                    }

                    movieId = Convert.ToInt32(idInput);

                    //Deleting the Movie
                    movieService.DeleteMovie(movieId);
                }

                Console.WriteLine("Movie Deleted Succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddProducer(IProducerService _producerService)
        {
            try
            {
                string producerName;
                string producerDateOfBirth;
                Console.WriteLine("Enter the name of the producer");
                producerName = Console.ReadLine();
                while (string.IsNullOrEmpty(producerName))
                {
                    Console.WriteLine("Enter valid name of actor");
                    producerName = Console.ReadLine();
                }

                //Date of birth of producer
                Console.WriteLine("Enter the date of birth of the producer");
                producerDateOfBirth = Console.ReadLine();
                bool producerBirthDateValidated = false;
                while (producerBirthDateValidated == false)
                {
                    try
                    {
                        DateTime tmpObject;
                        bool dateValidity = DateTime.TryParseExact
                        (
                            producerDateOfBirth,
                            "MM/dd/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out tmpObject
                        );
                        if (dateValidity)
                        {
                            producerBirthDateValidated = true;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Enter valid date of birth of actor Format(MM/dd/yyyy)");
                        producerDateOfBirth = Console.ReadLine();
                    }
                }


                _producerService.AddProducer(producerName, producerDateOfBirth);
                Console.WriteLine("Producer Added Succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddActor(IActorServices _actorService)
        {
            try
            {
                Console.WriteLine("Enter the name of the actor");
                string actorName = Console.ReadLine();
                while (string.IsNullOrEmpty(actorName))
                {
                    Console.WriteLine("Enter valid name of actor");
                    actorName = Console.ReadLine();
                }
                Console.WriteLine("Enter the date of birth of the actor Format (MM/dd/yyyy)");
                string actorDateOfBirth = Console.ReadLine();
                bool actorbirthDateValidated = false;
                while (actorbirthDateValidated == false)
                {
                    try
                    {
                        DateTime tmpObject;
                        bool dateValidity = DateTime.TryParseExact
                        (
                            actorDateOfBirth,
                            "MM/dd/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out tmpObject
                        );
                        if (dateValidity)
                        {
                            actorbirthDateValidated = true;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Enter valid date of birth of actor Format(MM/dd/yyyy)");
                        actorDateOfBirth = Console.ReadLine();
                    }
                }
                _actorService.AddActor(actorName, actorDateOfBirth);
                Console.WriteLine("Actor Added Succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddMovie(IActorServices _actorService, IProducerService _producerService, MovieService movieService)
        {
            Console.WriteLine("Enter the name of the movie");
            string movieName = Console.ReadLine();
            while (string.IsNullOrEmpty(movieName))
            {
                Console.WriteLine("Name can't be empty enter valid name");
                movieName = Console.ReadLine();
            }

            Console.WriteLine("Enter the release year of the movie");


            string str = Console.ReadLine();
            Int32 yearOfRelease;
            while (string.IsNullOrEmpty(str) || !str.All(char.IsDigit))
            {
                Console.WriteLine("Release year is invalid, enter valid release year");
                str = Console.ReadLine();
            }

            yearOfRelease = Convert.ToInt32(str);
            Console.WriteLine("Enter the plot of the movie");
            string plot = Console.ReadLine();
            while (string.IsNullOrEmpty(plot))
            {
                Console.WriteLine("Plot of the movie can't be empty enter valid plot");
                plot = Console.ReadLine();
            }

            List<Actor> allActors = new List<Actor>();
            List<int> actors = new List<int>();
            try
            {
                allActors = _actorService.GetAllActors();
                Console.WriteLine("List of all the available actors:  ");
                Console.WriteLine("ActorID     Actor Name");
                foreach (Actor actor in allActors)
                {
                    Console.WriteLine(actor.Id + "     " + actor.Name);
                }

                Console.WriteLine("Select the actor from the list ( , seprated values)");
                string actorInput = Console.ReadLine();
                bool actorsValidated = false;
                while (!actorsValidated)
                {
                    try
                    {
                        actors = actorInput.Split(',').Select(Int32.Parse).ToList();
                        actorsValidated = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Input is not in correct format Enter again");
                        actorInput = Console.ReadLine();
                    }
                }

                List<Producer> allProducers = new List<Producer>();
                try
                {
                    allProducers = _producerService.GetAllProducers();
                    Console.WriteLine("ProducerID     Producer Name");
                    foreach (Producer pr in allProducers)
                    {
                        Console.WriteLine(pr.Id + "     " + pr.Name);
                    }

                    string producerInput = Console.ReadLine();
                    Int32 producer;
                    bool producerValidated = false;
                    while (!producerValidated)
                    {
                        try
                        {
                            producer = Convert.ToInt32(producerInput);
                            producerValidated = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Input is not in correct format Enter again");
                            producerInput = Console.ReadLine();
                        }
                    }

                    producer = Convert.ToInt32(producerInput);
                    movieService.AddMovie(movieName, yearOfRelease, plot, actors, producer);
                    Console.WriteLine("Movie Added Succesfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ListMovie(MovieService movieService)
        {
            try
            {
                List<MovieResponse> allMovies = movieService.GetAllMovies();
                if (allMovies.Count == 0)
                {
                    Console.WriteLine("No Movie in the list");
                }
                else
                {
                    foreach (MovieResponse movie in allMovies)
                    {
                        Console.WriteLine("Movie Id  " + movie.Id);
                        Console.WriteLine("Name  " + movie.Name);
                        Console.WriteLine("Year of Release  " + movie.YearOfRelease);
                        Console.WriteLine("Plot of Movie  " + movie.Plot);
                        Console.Write("Actors -  ");
                        foreach (Actor a in movie.Actors)
                        {
                            Console.Write(a.Name + "  ");
                        }

                        Console.WriteLine();
                        Console.Write("Producers - ");
                        Console.Write(movie.Producer.Name);
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}