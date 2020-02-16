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
        
        public IProcessedSequenceDto FindByUnsorted(IList<double> unsorted)
        {
            using (var db = new SequenceDbContext())
            {
                string hashed = String.Join(",", unsorted);
                var found = db.ProcessedSequences.Where(x => x.Unsorted == hashed).FirstOrDefault();
                return found != null ? processedSequenceDto.FromEntity(found) : null;
            }
        }

        public IProcessedSequenceDto GetLatest()
        {
            using (var db = new SequenceDbContext())
            {
                var latest = db.ProcessedSequences.Where(x => x.Id == db.ProcessedSequences.Max(x => x.Id)).FirstOrDefault();
                return processedSequenceDto.FromEntity(latest);
            }
        }

        public IProcessedSequenceDto Save(IList<double> unsorted, IList<double> sorted)
        {
            // sanity check
            if (unsorted.Count == 0 || unsorted.Count != sorted.Count)
                throw new Exception("Unexpected parameter values");
            
            // Save in an easy to find format
            // in the real world would save vertically as saving numbers in a string 
            // is never good practice. 
            // Would probably also create a hash of the sequence as a lookup
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

                // create a Dto from the same entity and return it to the user
                return processedSequenceDto.FromEntity(newProcessesSequence);
            }
        }
    }
}
