using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace FoodOnline.Common;

[JsonConverter(typeof(SmartEnumNameConverter<Role, string>))]
public sealed class Role : SmartEnum<Role, string>
{
    public static readonly Role Admin = new();

    public static readonly Role User = new();

    public static readonly Role Manager = new();

    public static readonly Role Employee = new();

    private Role([CallerMemberName] string name = default!) : base(name, name)
    {
    }
}
