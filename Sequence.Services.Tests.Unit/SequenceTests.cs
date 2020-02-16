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
            var unsorted = new List<double> { 1, 2, 3 };

            var sequenceService = new SequenceService(repository);
            var sut = sequenceService.SaveIfNotExists(unsorted);

            Assert.NotNull(sut);
            Assert.Equal(sut.Unsorted, unsorted);
        }

        [Fact]
        public void TwoSequencesSameNumbers_Expect_SequenceExists_ToBeTrue()
        {
            var unsorted = "3,2,1";
            var sorted = "1,2,3";

            var sequenceService = new SequenceService(repository);
            var sut = sequenceService.SaveIfNotExists(unsorted);

            Assert.Equal(sut.Sorted, sorted);
        }
    }
}
