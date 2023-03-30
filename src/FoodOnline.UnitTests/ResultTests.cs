using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodOnline.UnitTests;

// use record since they support equality!
file record Poco
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required int Age { get; set; }
}

public class ResultTests
{
    [Fact]
    public void Ok_should_serialize_to_json()
    {
        // Arrange
        Result<Poco> result = new Poco
        {
            Id = "human1",
            Name = "panos",
            Age = 99
        };

        var expected = """
            {"$result":"ok","Id":"human1","Name":"panos","Age":99}
            """;

        // Act
        var test = JsonSerializer.Serialize(result);

        // Assert
        Assert.True(test == expected);
    }

    [Fact]
    public void Ok_should_deserialzie_from_json()
    {
        // Arrange
        var result = """
            {"$result":"ok","Id":"human1","Name":"panos","Age":99}
            """;

        Result<Poco> expected = new Poco
        {
            Id = "human1",
            Name = "panos",
            Age = 99
        };

        // Act
        var test = JsonSerializer.Deserialize<Result<Poco>>(result);

        // Assert
        Assert.True(test == expected);
    }

    [Fact]
    public void Er_should_serialize_to_json()
    {
        // Arrange
        Result<Poco> result = new ArgumentException("This is wrong");

        var expected = """
            {"$result":"error","Error":"ArgumentException","Reason":"This is wrong"}
            """;
        // Act

        var test = JsonSerializer.Serialize(result);

        // Assert
        Assert.True(test == expected);
    }

    [Fact]
    public void Er_should_deserialzie_from_json()
    {
        // Arrange
        var result = """
            {"$result":"error","Error":"ArgumentException","Reason":"This is wrong"}
            """;

        Result<Poco> expected = new ArgumentException("This is wrong");

        // Act
        var test = JsonSerializer.Deserialize<Result<Poco>>(result);

        // Assert
        Assert.True(test == expected);
    }
}