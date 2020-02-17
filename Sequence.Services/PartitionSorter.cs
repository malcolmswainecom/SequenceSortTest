using System.Collections.Generic;

namespace Sequence.Services
{
    public class PartitionSorter : ISorter
    {
        /// <summary>
        /// Implmentation of quicksort
        /// </summary>
        /// <param name="data">The data sto be sorted</param>
        /// <param name="l">lower bounds of sort</param>
        /// <param name="r">upper bounds of sort</param>
        public void Sort(IList<double> data, int lowerBounds, int upperBounds)
        {
  
            int lowerIndex = lowerBounds;
            int upperIndex = upperBounds;

            int pivotIndex = (lowerBounds + upperBounds) / 2;
            double pivotValue = data[pivotIndex];

            while (true)
            {
                while (data[lowerIndex] < pivotValue)
                {
                    lowerIndex++;
                }

                while (data[upperIndex] > pivotValue)
                {
                    upperIndex--;
                }

                if (lowerIndex <= upperIndex)
                {
                    double temp;
                    temp = data[lowerIndex];
                    data[lowerIndex] = data[upperIndex];
                    data[upperIndex] = temp;

                    lowerIndex++;
                    upperIndex--;
                }
                if (lowerIndex > upperIndex)
                    break;
            }

            if (lowerBounds < upperIndex)
                Sort(data, lowerBounds, upperIndex);
            if (lowerIndex < upperBounds)
                Sort(data, lowerIndex, upperBounds);
        }
    }
}
