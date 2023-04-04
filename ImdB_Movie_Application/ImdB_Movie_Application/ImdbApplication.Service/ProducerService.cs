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
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository=new ProducerRepository();
        public void AddProducer(string name, string dateOfBirth)
        {
            //For assigning id to all the producers 
            List<Producer> allProducers = _producerRepository.GetAllProducers();
            if (name == null)
            {
                throw new Exception("Name can't be empty enter valid name");
            }
            if (dateOfBirth == null)
            {
                throw new Exception("DateOfBirth can't be empty enter valid birth date");
            }
            var parameterDate = DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (parameterDate > DateTime.Today)
            {
                throw new ArgumentNullException("Entered date is greater than current date");
            }

            Int32 producerId;
            if (allProducers.Count == 0)
            {
                producerId = 1;
            }
            else
            {
                producerId = allProducers.OrderByDescending(m => m.Id).Select(m => m.Id).ToList()[0] + 1;
            }

            //Assigning values to producer object
            Producer newProducer = new Producer();
            newProducer.Id =producerId;
            newProducer.Name = name;
            newProducer.DateOfBirth =Convert.ToDateTime(dateOfBirth);
            _producerRepository.AddProducer(newProducer);
        }
        public List<Producer> GetAllProducers()
        {
            List<Producer> allProducer = _producerRepository.GetAllProducers();
            if (allProducer.Count == 0)
            {
                throw new Exception("No Producer in the list please first producer an actor");
            }
            return allProducer;
        }
    }

}
