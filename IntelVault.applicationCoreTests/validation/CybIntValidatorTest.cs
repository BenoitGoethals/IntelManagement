using System;
using System.Collections.Generic;
using FluentAssertions;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.validation;
using Xunit;

namespace IntelVault.applicationCoreTests.validation;

public class CybIntValidatorTest
{
    [Fact()]
    [Trait("Category", "Tests")]
    public void HumIntValidatorTest()
    {
        // Arrange
        CybInt cybInt = GetCyInt();
        //act
        CybIntValidator validator = new CybIntValidator();
        //Assert
        validator.Validate(cybInt).IsValid.Should().BeTrue();
    }

    private CybInt GetCyInt()
    {
        return new CybInt()
        {
            ImpactAssessment = "dsfdsfdsf",
            Attribution = "sdfdsfdsf",
            IncidentDescription = "Sdfsdfdsf",
            IncidentType = TypeOfCybInt.DataBreaches,
            MalwareAnalysis = "sdfdsfdsf",
            Timeline = new List<ListItemDate>() { new ListItemDate() { Id = 1, Dtg = DateTime.Now } },
        };
    }

    [Fact()]
    [Trait("Category", "tests")]
    public void HumIntValidatorFalseTest()
    {
        // Arrange
        CybInt cybInt = GetCyInt();
        //act
        CybIntValidator validator = new CybIntValidator();
        cybInt.IncidentDescription = "";
        //Assert
        validator.Validate(cybInt).IsValid.Should().BeFalse();
    }



}