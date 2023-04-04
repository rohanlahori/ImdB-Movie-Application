using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApplication.Domain;

namespace ImdB_Application.Services.Interfaces
{
    public interface IProducerService
    {
        public void AddProducer(string name, string dateOfBirth);
        public List<Producer> GetAllProducers();
    }
}
