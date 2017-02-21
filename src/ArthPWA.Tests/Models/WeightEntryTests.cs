using NUnit.Framework;
using ArthPWA.Models;
using SharpTestsEx;
using ArthPWA.Common;
using ArthPWA.Tests.Helpers;
using System;

namespace ArthPWA.Tests.Models
{
    [TestFixture]
    public class WeightEntryTests
    {
        private DateTime _utcTime;
        private WeightEntry _sut;

        [SetUp]
        public void PrepareSut()
        {
            _utcTime = RandomData.GetDateTime();
            SystemTime.UtcNow = () => _utcTime;
            _sut = new WeightEntry();
        }

        [TearDown]
        public void TearDown()
        {
            SystemTime.ResetToDefault();
        }

        public sealed class Ctor : WeightEntryTests
        {
            [Test]
            public void Should_initialize_CreatedOn_to_current_Utc_time()
            {
                //assert
                _sut.Created.On.Should().Be.EqualTo(_utcTime);
            }

            [Test]
            public void Should_initialize_LastModifiedOn_to_current_Utc_time()
            {
                //assert
                _sut.LastModified.On.Should().Be.EqualTo(_utcTime);
            }
        }
    }
}
