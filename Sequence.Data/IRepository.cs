using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Data
{
    public interface IRepository
    {
        IProcessedSequenceDto FindByUnsorted(IList<double> unsorted);
        IProcessedSequenceDto GetLatest();
        IProcessedSequenceDto Save(IList<double> unsorted, IList<double> sorted);
    }
}
