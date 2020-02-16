using System;
using System.Collections.Generic;
using System.Text;

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
              
            }

            return new List<ProcessedSequence>();
        }

        public IEnumerable<ProcessedSequence> Save(IEnumerable<double> sequence, IEnumerable<double> sortedSequence)
        {
            var concreteSequence = new List<double>(sequence);
            var concreteSortedSequence = new List<double>(sortedSequence);

            using (var db = new SequenceDbContext())
            {
                for (int i = 0; i < concreteSequence.Count -1; i++)
                {
                    db.ProcessedSequences.Add(new ProcessedSequence() { Unsorted = concreteSequence[i], Sorted = concreteSortedSequence[i] });
                }

                db.SaveChanges();
            }
            
            return new List<ProcessedSequence>();
        }
    }
}
