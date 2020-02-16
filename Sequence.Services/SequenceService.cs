using Sequence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public class SequenceService : ISequenceService
    {
        IRepository repository;

        public SequenceService(IRepository _repository)
        {
            repository = _repository;
        }
        
        public ProcessedSequence GetLatest()
        {
            return repository.GetLatest();
        }

        public ProcessedSequence SaveIfNotExists(IList<double> unsorted)
        {
            var existing = repository.FindByUnsorted(unsorted);
            if (existing != null)
            {
                return existing;
            }
            else 
            {
                var sorted = Sort(unsorted);
                return repository.Save(unsorted, sorted);
            }
        }

        public List<double> Sort(IList<double> unsorted)
        {
            var sorted = new List<double>();
            
            // For purpose of the exercise write our own sorting
            (unsorted as List<double>).Sort();

            return sorted;
        }
    }
}
