using Sequence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public interface ISequenceService
    {
        ProcessedSequence GetLatest();
        ProcessedSequence SaveIfNotExists(IList<double> unsorted);
    }
}
