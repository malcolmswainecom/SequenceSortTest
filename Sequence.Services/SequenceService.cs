using Sequence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public class SequenceService : ISequenceService
    {
        // needs to be injected
        //SequenceResult[] CachedSequences = { };
        
        IRepository repository;


        public SequenceService(IRepository _repository)
        {
            repository = _repository;
        }
        
        public IEnumerable<ProcessedSequence> GetLatest()
        {
            return repository.GetLatest();
        }

        public IEnumerable<ProcessedSequence> SaveIfNotExists(IEnumerable<double> sequence)
        {
            var existingSequence = repository.FindBySequence(sequence);

            // if it doesn't exist
            if (existingSequence == null)
            {
                var sortedSequence = Sort(sequence);
                return repository.Save(sequence, sortedSequence);
            }

            return existingSequence;
        }


        //public int FindIndex(int[] sequence) 
        //{
        //    // maybe use parallel task lib here
        //    int matchingIndex = -1;
        //    bool found = false;


        //    for (int i = 0; i < CachedSequences.Length; i++)
        //    {
        //        for (int j = 0; j < CachedSequences[i].Raw.Length; j++)
        //        {
        //            for (int k = 0; k < sequence.Length; k++)
        //            {
        //                if (sequence[k] == CachedSequences[i].Raw[j])
        //                {
        //                    found = true;
        //                    continue;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    return matchingIndex;
        //}

        public IEnumerable<double> Sort(IEnumerable<double> sequence)
        {
            // TODO write own sorting algo
            sequence.ToList().Sort();

            return sequence;
        }
    }
}
