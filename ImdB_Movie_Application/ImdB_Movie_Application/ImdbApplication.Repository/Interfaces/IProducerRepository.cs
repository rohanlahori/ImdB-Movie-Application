using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Domain;

namespace ImdbApplication.Repository.Interfaces
{
    public interface IProducerRepository
    {
        public void AddProducer(Producer producer);
        public List<Producer> GetAllProducers();
    }
}
