using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Repository.Interfaces;
using ImdbApplication.Domain;
namespace ImdbApplication.Repository
{
    public class ActorRepository:IActorRepository
    {
        List<Actor> _actors = new List<Actor>();
        public void AddActor(Actor actor)
        {
            _actors.Add(actor);
        }

        public List<Actor> GetAllActors() 
        {
            return _actors;
        }
    }

}
