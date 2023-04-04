using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdB_Application.Services.Interfaces;
using ImdbApplication.Domain;
using ImdbApplication.Repository;
using ImdbApplication.Repository.Interfaces;

namespace ImdB_Application.Services
{
    public class ActorService:IActorServices
    {
        private readonly IActorRepository _actorRepository=new ActorRepository();
        public void AddActor(string name, string dateOfBirth)
        {
            List<Actor> allActors = _actorRepository.GetAllActors();
            if (name==null)
            {
                throw new Exception("Name can't be empty enter valid name");
            }
            if(dateOfBirth==null) 
            {
                throw new Exception("DateOfBirth can't be empty enter valid birth date");
            }

            var parameterDate = DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (parameterDate > DateTime.Today)
            {
                throw new ArgumentNullException("Entered date is greater than current date");
            }

            //Used for assigning actor Id to every added actor
            Int32 Id;
            if (allActors.Count == 0)
            {
                Id = 1;
            }
            else
            {
                Id = allActors.OrderByDescending(m => m.Id).Select(m => m.Id).ToList()[0] + 1;
            }

            //Assigning the values to actor object
            Actor newActor = new Actor();
            newActor.Id= Id;
            newActor.Name= name;
            newActor.DateOfBirth=Convert.ToDateTime(dateOfBirth);
            _actorRepository.AddActor(newActor);
        }
        public List<Actor> GetAllActors()
        {
            List<Actor>allActors=_actorRepository.GetAllActors();
            if(allActors.Count==0)
            {
                throw new Exception("No actor in the list please first add an actor");
            }
            return allActors;
        }
    }
}
