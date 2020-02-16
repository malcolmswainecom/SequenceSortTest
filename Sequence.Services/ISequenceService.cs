using Sequence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public interface ISequenceService
    {
        IProcessedSequenceDto GetLatest();
        IProcessedSequenceDto SaveIfNotExists(IList<double> unsorted);
        IList<double> Sort(IList<double> unsorted);
    }
}
