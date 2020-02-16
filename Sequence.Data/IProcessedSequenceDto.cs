using System;
using System.Collections.Generic;
using System.Text;

namespace Sequence.Data
{
    public interface IProcessedSequenceDto
    {
        int Id { get; set; }
        IList<double> Unsorted { get; set; }
        IList<double> Sorted { get; set; }
        DateTimeOffset CreatedDateTime { get; set; }

        IProcessedSequenceDto FromEntity(ProcessedSequence entity);
    }
}
