﻿using FluentAssertions;
using IntelVault.ApplicationCore.Services;
using Xunit;

namespace IntelVault.applicationTests
{
    
    public class ServiceCountryTests
    {
      [Fact]
        public void ServiceCountryTest()
        {
           var country = new ServiceCountry();
           country.Countries.Should().NotBeEmpty();
        }
    }
}