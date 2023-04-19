using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace FoodOnline.Users.BsonSerializesrs;

public sealed class RoleBsonSerializer : SerializerBase<Role>
{
    private static bool registered;

    public static bool TryRegister()
    {
        if (registered) return false;

        BsonSerializer.RegisterDiscriminatorConvention(typeof(Role), new ScalarDiscriminatorConvention("$role"));

        return registered = true;
    }

    public RoleBsonSerializer()
    {
        TryRegister();
    }

    public override Role Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return BsonSerializer.Deserialize<Role>(context.Reader);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Role value)
    {
        BsonSerializer.Serialize(context.Writer, args.NominalType, value);
    }
}
