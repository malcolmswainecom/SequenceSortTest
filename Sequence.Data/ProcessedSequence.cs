using System;

namespace Sequence.Data
{
    public class ProcessedSequence
    {
        public int Id { get; set; }
        public string Unsorted { get; set; }
        public string Sorted { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
