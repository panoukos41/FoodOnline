using Dunet;
using System.Text.Json.Serialization;

namespace FoodOnline.Users;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$role")]
[JsonDerivedType(typeof(Admin), typeDiscriminator: nameof(Admin))]
[JsonDerivedType(typeof(User), typeDiscriminator: nameof(User))]
[JsonDerivedType(typeof(Manager), typeDiscriminator: nameof(Manager))]
[JsonDerivedType(typeof(Employee), typeDiscriminator: nameof(Employee))]

[Union]
public partial record Role
{
    public partial record Admin;

    public partial record User;

    public partial record Manager;

    public partial record Employee;

    public sealed override string ToString()
    {
        return GetType().Name;
    }
}
