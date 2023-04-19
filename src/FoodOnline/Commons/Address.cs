using FluentValidation;

namespace FoodOnline.Commons;

public sealed record Address : IValid
{
    public required string Street { get; init; }

    public required string Region { get; init; }

    public required string City { get; init; }

    public required string Country { get; init; }

    public required string ZipCode { get; init; }

    public required Location Location { get; init; }

    public static Address Empty { get; } = new()
    {
        Street = string.Empty,
        Region = string.Empty,
        City = string.Empty,
        Country = string.Empty,
        ZipCode = string.Empty,
        Location = Location.Empty,
    };

    public static IValidator Validator { get; } = new AddresValidator();
}

public class AddresValidator : AbstractValidator<Address>
{
    public AddresValidator()
    {

    }
}

//public class Address : ValueObjectBase
//{
//    public string Street { get; private set; }

//    public string Region { get; private set; }

//    public string City { get; private set; }

//    public string Country { get; private set; }

//    public string ZipCode { get; private set; }

//    private Address()
//    {
//    }

//    public Address(string street, string region, string city, string country, string zipCode)
//    {
//        Street = street;
//        Region = region;
//        City = city;
//        Country = country;
//        ZipCode = zipCode;
//    }

//    public static Address For(string address)
//    {
//        try
//        {
//            var split = address.Split(',');
//            return new Address
//            {
//                Street = split[0].Trim(),
//                Region = split[1].Trim(),
//                City = split[2].Trim(),
//                ZipCode = split[3].Trim(),
//                Country = split[4].Trim()
//            };
//        }
//        catch (Exception ex)
//        {
//            throw new AddressInvalidException(address, ex);
//        }
//    }

//    public static implicit operator string(Address address) => address.ToString();

//    public static explicit operator Address(string address) => For(address);

//    public override string ToString()
//    {
//        return $"{Street}, {Region}, {City}, {ZipCode}, {Country}";
//    }

//    protected override IEnumerable<object> GetAtomicValues()
//    {
//        yield return Street;
//        yield return Region;
//        yield return City;
//        yield return Country;
//        yield return ZipCode;
//    }
//}
