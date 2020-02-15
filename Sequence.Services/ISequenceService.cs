using Sequence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public interface ISequenceService
    {
        IEnumerable<ProcessedSequence> GetLatest();
        IEnumerable<ProcessedSequence> SaveIfNotExists(IEnumerable<double> sequence);
        
    }
}
