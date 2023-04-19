using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace FoodOnline.Authentications.BsonSerializesrs;

public class AuthTypeSerializer : SerializerBase<AuthType>
{
    private static bool registered;

    public static bool TryRegister()
    {
        if (registered) return false;

        BsonSerializer.RegisterDiscriminatorConvention(typeof(AuthType), new ScalarDiscriminatorConvention("$auth"));

        return registered = true;
    }

    public AuthTypeSerializer()
    {
        TryRegister();
    }

    public override AuthType Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return BsonSerializer.Deserialize<AuthType>(context.Reader);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, AuthType value)
    {
        BsonSerializer.Serialize(context.Writer, args.NominalType, value);
    }
}
