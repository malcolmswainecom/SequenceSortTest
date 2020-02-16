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
        public void SaveNewSequence_Expect_NewSequenceToBeReturnedFromCreate()
        {
            var sequence = new List<double>{ 1, 2, 3 };

            var sequenceService = new SequenceService(repository);
            var sut = sequenceService.SaveIfNotExists(sequence)?.ToList();

            Assert.NotNull(sut);
            Assert.True(sut.Select(x => x.Unsorted).All(sequence.Contains));
        }

        [Fact]
        public void TwoSequencesSameNumbers_Expect_SequenceExists_ToBeTrue()
        {
            var sequence = new List<double> { 3, 2, 1 };
            var orderedSequence = new List<double> { 1, 2, 3 };

            var sequenceService = new SequenceService(repository);
            var sut = sequenceService.SaveIfNotExists(sequence)?.ToList();

            Assert.True(sut.Select(x => x.Sorted).SequenceEqual(orderedSequence));
        }
    }
}
