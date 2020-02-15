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
            return new List<ProcessedSequence>();
        }

        public IEnumerable<ProcessedSequence> Save(IEnumerable<double> sequence, IEnumerable<double> sortedSequence)
        {
            return new List<ProcessedSequence>();
        }
    }
}
