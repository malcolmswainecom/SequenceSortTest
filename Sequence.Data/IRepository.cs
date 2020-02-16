using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Data
{
    public interface IRepository
    {
        ProcessedSequence FindByUnsorted(IList<double> unsorted);
        ProcessedSequence GetLatest();
        ProcessedSequence Save(IList<double> unsorted, IList<double> sorted);
    }
}
