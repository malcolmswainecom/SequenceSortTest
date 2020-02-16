using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequence.Data
{
    public class ProcessedSequenceDto : IProcessedSequenceDto
    {
        public int Id { get; set; }
        public IList<double> Unsorted { get; set; }
        public IList<double> Sorted { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Convert our entity into a Dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IProcessedSequenceDto FromEntity(ProcessedSequence entity)
        {
            return new ProcessedSequenceDto()
            {
                Id = entity.Id,
                Unsorted = entity.Unsorted.Split(",").Select(Double.Parse).ToList(),
                Sorted = entity.Sorted.Split(",").Select(Double.Parse).ToList(),
                CreatedDateTime = entity.CreatedDateTime
            };
        }
    }
}
