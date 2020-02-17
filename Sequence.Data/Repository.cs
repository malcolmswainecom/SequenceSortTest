using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sequence.Data
{
    public class Repository : IRepository
    {
        IProcessedSequenceDto processedSequenceDto;

        public Repository(IProcessedSequenceDto _processedSequenceDto)
        {
            processedSequenceDto = _processedSequenceDto;
        }
        
        /// <summary>
        /// Check if this exact sequence has already been saved to the database
        /// </summary>
        /// <param name="unsorted"></param>
        /// <returns>The Dto representation if it;s found, otherwise null</returns>
        public IProcessedSequenceDto FindByUnsorted(IList<double> unsorted)
        {
            using (var db = new SequenceDbContext())
            {
                string hashed = String.Join(",", unsorted);
                var found = db.ProcessedSequences.Where(x => x.Unsorted == hashed).FirstOrDefault();
                return found != null ? processedSequenceDto.FromEntity(found) : null;
            }
        }

        /// <summary>
        /// Get the latest saved sequence data
        /// </summary>
        /// <returns></returns>
        public IProcessedSequenceDto GetLatest()
        {
            using (var db = new SequenceDbContext())
            {
                var latest = db.ProcessedSequences.Where(x => x.Id == db.ProcessedSequences.Max(x => x.Id)).FirstOrDefault();
                return processedSequenceDto.FromEntity(latest);
            }
        }

        /// <summary>
        /// Save the sequence (sorted and unsorted) as an entity back to the database
        /// </summary>
        /// <param name="unsorted"></param>
        /// <param name="sorted"></param>
        /// <returns>The Dto represenation of the saved entity</returns>
        public IProcessedSequenceDto Save(IList<double> unsorted, IList<double> sorted)
        {
            // sanity check
            if (unsorted.Count == 0 || unsorted.Count != sorted.Count)
                throw new Exception("Unexpected parameter values");
            
            // Save in an easy to find format
            // in the real world would save vertically as saving numbers in a string is never good practice. 
            // Would probably also create a hash of the sequence for quicker lookup
            var newProcessesSequence = new ProcessedSequence()
            {
                Unsorted = String.Join(",", unsorted),
                Sorted = String.Join(",", sorted),
                CreatedDateTime = DateTimeOffset.Now
            };

            // Add the entity to the database
            using (var db = new SequenceDbContext())
            {
                db.ProcessedSequences.Add(newProcessesSequence);
                db.SaveChanges();

                // create a Dto from the entity and return it back up the stack
                return processedSequenceDto.FromEntity(newProcessesSequence);
            }
        }
    }
}
