using System;
using System.Collections.Generic;
using System.Linq;
using IntelVault.ApplicationCore.Model;
using IntelVault.Sharewdtest;
using MongoDB.Bson;
using Xunit;

namespace IntelVault.InfrastructureTests
{
    public class MongoDbRepositoryTests : MongoDbTestBase<HumInt>
    {
        [Trait("Category", "Integration")]
        [Fact()]
        public void GetAllAsyncTest()
        {

            var humInt = new HumInt()
            {
                Id = ObjectId.GenerateNewId(),
                AccessLevel = "test",
                AccuracyOfDetails = "sdfds",
                AssessmentAndAnalysis = "sfdsfds",
                ClassificationHandlingInstructions = "dsfdsf",
                ContactEmail = "sdfdsfds@fdfs.be",
                ContactName = "fsdfsd",
                ContactPhoneNumber = "dsfdsf",
                ContactTitle = "John Doe",
                ContextBackground = "sdfsdf",
                IntelligenceDetails = new List<ListItem>(){new ListItem(){Id = 0,Text = "dffsdfdsf"}},
                LastContactDate = DateTime.Now,
                OperationalStatus = "asdsa",
                ReliabilityCredibility = "fdsds",
                ReliabilityRating = 100,
                SourceAffiliation = "dsad",
                SourceInformation = "sdfdsf",
                SourceName = "sdfdsf",
                SourceType = "sadsa",
                TimeLocation = "uk",
                HumIntType = HumIntType.Advisors
            };


            var task = DbRepository.InsertAsync(humInt);
            task?.ContinueWith(_ =>
            {
                var humInts = DbRepository.GetAllAsync().Result;
                if (humInts == null) return;
                var result = humInts.FirstOrDefault();
                Assert.Equal("John Doe", result?.ContactTitle);
                Assert.Equal(100, result?.ReliabilityRating);
            });
        

        }
    }
}