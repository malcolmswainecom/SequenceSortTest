using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Data
{
    public interface IRepository
    {
        IEnumerable<ProcessedSequence> FindBySequence(IEnumerable<double> sequence);
        IEnumerable<ProcessedSequence> GetLatest();
        IEnumerable<ProcessedSequence> Save(IEnumerable<double> sequence, IEnumerable<double> sortedSequence); // not sure if this is pure enough
        
    }
}
