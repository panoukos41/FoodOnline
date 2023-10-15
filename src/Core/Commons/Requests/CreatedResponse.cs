using System.Diagnostics.CodeAnalysis;

namespace Core.Commons.Requests;

public sealed record CreatedResponse
{
    public required Uuid Id { get; set; }

    public CreatedResponse()
    {
    }

    [SetsRequiredMembers]
    public CreatedResponse(Uuid id)
    {
        Id = id;
    }
}
