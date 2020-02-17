using Sequence.Data;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Moq;

namespace Sequence.Services.Tests.Unit
{
    public class SequenceServiceTests
    {
        IProcessedSequenceDto processedSequenceDto; 
        Mock<IRepository> repository; 
        ISorter defaultSorter = new PartitionSorter();
        IProcessedSequenceDto testProcessedSequenceDto = new ProcessedSequenceDto()
        { 
            Id = 1, 
            CreatedDateTime = DateTimeOffset.Now, 
            Unsorted = new List<double>() { 3, 2, 1 }, 
            Sorted = new List<double>() { 1, 2, 3 } 
        };
        

        public SequenceServiceTests()
        {
            processedSequenceDto = Mock.Of<IProcessedSequenceDto>();
            repository = new Mock<IRepository>();

            repository.Setup(x => x.Save(It.IsAny<IList<double>>(), It.IsAny<IList<double>>()))
                .Returns(testProcessedSequenceDto);
        }


        [Fact]
        public void SaveNewSequence_SequenceExists_Expect_ExistingToBeReturned()
        {
            repository.Setup(x => x.FindByUnsorted(It.IsAny<IList<double>>()))
                .Returns(testProcessedSequenceDto);

            var unsorted = testProcessedSequenceDto.Unsorted;

            var sequenceService = new SequenceService(repository.Object, defaultSorter);
            var sut = sequenceService.SaveIfNotExists(unsorted);

            Assert.Equal(sut.Unsorted, unsorted);
        }

        [Fact]
        public void SaveNewSequence_SequenceDoesntExists_Expect_SequenceToBeSaved()
        {
            repository.Setup(x => x.FindByUnsorted(It.IsAny<IList<double>>())).Returns(() => null);
            testProcessedSequenceDto.Sorted = null;

            var sequenceService = new SequenceService(repository.Object, defaultSorter);
            var sut = sequenceService.SaveIfNotExists(testProcessedSequenceDto.Unsorted);

            repository.Verify(x => x.Save(It.IsAny<IList<double>>(), It.IsAny<IList<double>>()), Times.Once);
        }

        [Fact]
        public void CallSequenceSort_Expect_SequenceToSorted1()
        {
            var sequenceService = new SequenceService(repository.Object, defaultSorter);
            var sut = sequenceService.Sort(testProcessedSequenceDto.Unsorted);

            Assert.Equal(sut, new List<double>() { 1, 2, 3});
        }

        [Fact]
        public void CallSequenceSort_Expect_SequenceToSorted2()
        {
            var sequenceService = new SequenceService(repository.Object, defaultSorter);
            var sut = sequenceService.Sort(new List<double>() { 5, 4, 3, 2, 1});

            Assert.Equal(sut, new List<double>() { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void CallSequenceSort_Expect_SequenceToSorted3()
        {
            var sequenceService = new SequenceService(repository.Object, defaultSorter);
            var sut = sequenceService.Sort(new List<double>() { 1, 21, 13, 432, 19, 20 });

            Assert.Equal(sut, new List<double>() { 1, 13, 19, 20, 21, 432 });
        }
    }
}
