using MongoDB.Bson;
using System.Collections.Generic;
using System;
using FluentAssertions;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.validation;
using Xunit;

namespace IntelVault.applicationTests.validation
{

    public class HumIntValidatorTests
    {
        [Fact()]
        [Trait("Category", "Tests")]
        public void HumIntValidatorTest()
        {
            // Arrange
            HumInt humInt = GetHumit();
            //act
            HumIntValidator humIntValidator = new HumIntValidator();
            //Assert
            humIntValidator.Validate(humInt).IsValid.Should().BeTrue();
        }

        [Fact()]
        [Trait("Category", "tests")]
        public void HumIntValidatorFalseTest()
        {
            // Arrange
            HumInt humInt = GetHumit();
            humInt.ContactEmail = "be";
            humInt.ContactName = "";
            //act
            HumIntValidator humIntValidator = new HumIntValidator();
            //Assert
            humIntValidator.Validate(humInt).IsValid.Should().BeFalse();
        }


        private HumInt GetHumit()
        {
            return new HumInt()
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
                IntelligenceDetails = new List<ListItem>(),
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
        }
    }
}