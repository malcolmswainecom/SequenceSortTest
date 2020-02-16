using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sequence.Data
{
    public class Repository : IRepository
    {
        public IEnumerable<ProcessedSequence> FindBySequence(IEnumerable<double> sequence)
        {
            return new List<ProcessedSequence>();
        }

        public IEnumerable<ProcessedSequence> GetLatest()
        {
            using (var db = new SequenceDbContext())
            {
                var maxId = db.Batches.Max(x => x.BatchId);
                var processedSequence = db.ProcessedSequences.Where(x => x.BatchId == maxId);
                return processedSequence;
            }
        }

        public IEnumerable<ProcessedSequence> Save(IEnumerable<double> sequence, IEnumerable<double> sortedSequence)
        {
            // this isn't great
            var concreteSequence = new List<double>(sequence);
            var concreteSortedSequence = new List<double>(sortedSequence);

            // cheaper to keep this open, than keep fetching from pool
            using (var db = new SequenceDbContext())
            {
                // generate a batch id
                // needs to be offset
                var batch = new Batch() { Created = DateTime.Now };
                db.Batches.Add(batch);
                db.SaveChanges();
                
                for (int i = 0; i < concreteSequence.Count -1; i++)
                {
                    db.ProcessedSequences.Add(
                        new ProcessedSequence() { 
                            Unsorted = concreteSequence[i], 
                            Sorted = concreteSortedSequence[i],
                            BatchId = batch.BatchId
                        });
                }

                db.SaveChanges();

                // not sure if should get this from db or just from memory
                var processedSequence = db.ProcessedSequences.Where(x => x.BatchId == batch.BatchId);
                return processedSequence;
            }
        }
    }
}
