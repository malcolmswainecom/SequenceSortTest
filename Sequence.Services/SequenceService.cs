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
        ISorter sorter;

        public SequenceService(IRepository _repository, ISorter _sorter)
        {
            repository = _repository;
            sorter = _sorter;
        }
        
        /// <summary>
        /// Get the last inserted sequence
        /// (if multi user, might not be the one you last inserted!)
        /// </summary>
        /// <returns></returns>
        public IProcessedSequenceDto GetLatest()
        {
            return repository.GetLatest();
        }

        /// <summary>
        /// Save a sequence and its sorted counterpart to the database if not exists
        /// </summary>
        /// <param name="unsorted"></param>
        /// <returns>If exists the existing object, otherwise the Dto of the newly created entity</returns>
        public IProcessedSequenceDto SaveIfNotExists(IList<double> unsorted)
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

        /// <summary>
        /// Abstract away the concerete sort implementation
        /// </summary>
        /// <param name="unsorted"></param>
        /// <returns></returns>
        public IList<double> Sort(IList<double> unsorted)
        {
            // shallow copy is okay here
            var sorted = new List<double>(unsorted);

            // For purpose of the exercise write our own sorting
            sorter.Sort(sorted, 0, sorted.Count - 1);

            // return the sorted list
            return sorted;
        }
    }
}
