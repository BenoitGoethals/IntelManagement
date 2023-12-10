using System;
using FluentAssertions;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.validation;
using Xunit;

namespace IntelVault.applicationTests.validation;

public class SocialMediaValidatorTest
{


    [Fact()]
    [Trait("Category", "Tests")]
    public void SocialMediatValidatorTest()
    {
        // Arrange
        SocialMedia socialMedia = GetSocial();
        //act
        SocialMediaValidator socialMediaValidator = new SocialMediaValidator();
        //Assert
        socialMediaValidator.Validate(socialMedia).IsValid.Should().BeTrue();
    }

    private SocialMedia GetSocial()
    {
        return new SocialMedia()
        {
            AccountCreationDate = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
            Bio = "dfsf",
            DisplayName = "sfdfds",
            Platform = "dfdsf",
            Username = "ddfsfsf",

        };
    }

    [Fact()]
    [Trait("Category", "tests")]
    public void SocialMediaValidatorFalseTest()
    {

        // Arrange
        SocialMedia socialMedia = GetSocial();
        //act
        SocialMediaValidator socialMediaValidator = new SocialMediaValidator();
        socialMedia.Username = "";
        //Assert
        socialMediaValidator.Validate(socialMedia).IsValid.Should().BeFalse();
    }

}