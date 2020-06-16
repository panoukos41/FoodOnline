using FluentAssertions;
using FoodOnline.Domain.ValueObjects;
using NUnit.Framework;

namespace FoodOnline.Domain.UnitTests.ValueObjects
{
    public class AddressTests
    {
        [Test]
        public void ShouldHaveCorrectValues()
        {
            const string addressString = "MyStreet, My Region, MyCity, 1xxxx, GREECE";

            var address = Address.For(addressString);

            address.Street.Should().Be("MyStreet");
            address.Region.Should().Be("My Region");
            address.City.Should().Be("MyCity");
            address.ZipCode.Should().Be("1xxxx");
            address.Country.Should().Be("GREECE");
        }

        [Test]
        public void ToStringReturnsCorrectFormat()
        {
            const string addressString = "My Street,My Region, My City, 1xx xx,GREECE";
            const string correctString = "My Street, My Region, My City, 1xx xx, GREECE";

            var address = Address.For(addressString);

            var result = address.ToString();

            result.Should().Be(correctString);
        }

        [Test]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string addressString = "My Street,My Region,My City, 1xx xx, My Country";
            const string correctString = "My Street, My Region, My City, 1xx xx, My Country";

            var address = Address.For(addressString);

            string result = address;

            result.Should().Be(correctString);
        }

        [Test]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string addressString = "MyStreet,My Region, MyCity, 1xxxx, GREECE";

            var address = (Address)addressString;

            address.Street.Should().Be("MyStreet");
            address.Region.Should().Be("My Region");
            address.City.Should().Be("MyCity");
            address.ZipCode.Should().Be("1xxxx");
            address.Country.Should().Be("GREECE");
        }

        [Test]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            FluentActions.Invoking(() => (Address)"Myplace at country on zenith.")
                .Should().Throw<AddressInvalidException>();
        }
    }
}