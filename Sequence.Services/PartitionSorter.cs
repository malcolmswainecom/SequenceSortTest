using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Services
{
    public class PartitionSorter : ISorter
    {
        public void Sort(IList<double> data, int l, int r)
        {
            int i, j;
            double x;

            i = l;
            j = r;

            x = data[(l + r) / 2]; /* find pivot item */
            while (true)
            {
                while (data[i] < x)
                    i++;
                while (x < data[j])
                    j--;
                if (i <= j)
                {
                    exchange(data, i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }
            if (l < j)
                Sort(data, l, j);
            if (i < r)
                Sort(data, i, r);
        }

        public void exchange(IList<double> data, int m, int n)
        {
            double temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
