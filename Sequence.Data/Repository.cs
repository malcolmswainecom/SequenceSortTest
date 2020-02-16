using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sequence.Data
{
    public class Repository : IRepository
    {
        public ProcessedSequence FindByUnsorted(IList<double> unsorted)
        {
            using (var db = new SequenceDbContext())
            {
                string hashed = String.Join(",", unsorted);
                return db.ProcessedSequences.Where(x => x.Unsorted == hashed).FirstOrDefault();
            }
        }

        public ProcessedSequence GetLatest()
        {
            using (var db = new SequenceDbContext())
            {
                var latest = db.ProcessedSequences.Where(x => x.Id == db.ProcessedSequences.Max(x => x.Id)).FirstOrDefault();
                return latest;
            }
        }

        public ProcessedSequence Save(IList<double> unsorted, IList<double> sorted)
        {
            var newProcessesSequence = new ProcessedSequence()
            {
                Unsorted = String.Join(",", unsorted),
                Sorted = String.Join(",", sorted),
                CreatedDateTime = DateTimeOffset.Now
            };

            using (var db = new SequenceDbContext())
            {
                db.ProcessedSequences.Add(newProcessesSequence);
                db.SaveChanges();

                return newProcessesSequence;
            }
        }
    }
}
