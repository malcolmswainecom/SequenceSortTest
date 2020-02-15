using Sequence.Data;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Sequence.Services.Tests.Unit
{
    public class SequenceServiceTests
    {
        // needs to be injected and mocked
        IRepository repository = new Repository();

        [Fact]
        public void SaveNewSequence_Expect_SequencToBeReturnedFromCreate()
        {
            var sequence = new List<double>{ 1, 2, 3 };

            
            var sequenceService = new SequenceService(repository);
            sequenceService.SaveIfNotExists(sequence);

            var sut = sequenceService.SaveIfNotExists(sequence)?.ToList();

            Assert.NotNull(sut);
           // Assert.Equal(sut.Select(x => x.Unsorted).
        }

        [Fact]
        public void TwoSequencesSameNumbers_Expect_SequenceExists_ToBeTrue()
        {
            int[] sequence = { 1, 2, 3 };

            var sequenceService = new SequenceService(repository);
            //sequenceService.SaveIfNotExists(sequence);

            //var sut = sequenceService.FindIndex(sequence);

           //Assert.Equal(sut, 0);
        }
    }
}
