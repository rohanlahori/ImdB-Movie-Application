using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Domain;
using ImdbApplication.Repository.Interfaces;

namespace ImdbApplication.Repository
{
    public class ProducerRepository: IProducerRepository
    {
        List<Producer> _producers = new List<Producer>();
        public void AddProducer(Producer producer)
        {
            _producers.Add(producer);
        }

        public List<Producer> GetAllProducers()
        {
            return _producers;
        }
    }
}
