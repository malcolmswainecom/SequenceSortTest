using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Services
{
    public interface ISorter
    {
        void Sort(IList<double> data, int l, int r);
    }
}
