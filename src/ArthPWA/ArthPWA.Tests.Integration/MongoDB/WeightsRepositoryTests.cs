using ArthPWA.MongoDB;
using ArthPWA.Models;
using NUnit.Framework;

namespace ArthPWA.Tests.Integration.MongoDB
{
    [TestFixture]
    class WeightsRepositoryTests : TestWithDatabaseBase
    {
        private WeightsRepository _weightsRepository;

        [SetUp]
        public void SetUp()
        {
            _weightsRepository = new WeightsRepository(Database);
        }

        public sealed class CreateResourceMethod : WeightsRepositoryTests
        {
            private WeightEntry _weightEntry;

            [SetUp]
            public void PrepareTest()
            {
                //_createUtcTime = RandomData.GetDateTime().ToUniversalTime();
                //   SystemTime.UtcNow = () => _createUtcTime;

                _weightEntry = new WeightEntry
                {
                   
                };
            }


         //   [Test, Explicit]
            public void Should_insert_weight_entry_with_same_created_and_last_modified_timestamps()
            {
                //act
                var newId = _weightsRepository.CreateResource(_weightEntry);

                //assert
                var fetched = _weightsRepository.GetResource(newId);
               // fetchedAccount.Created.On.Should().Be.EqualTo(_createUtcTime);
            //    fetchedAccount.LastModified.On.Should().Be.EqualTo(_createUtcTime);
            }
        }
    }
}
