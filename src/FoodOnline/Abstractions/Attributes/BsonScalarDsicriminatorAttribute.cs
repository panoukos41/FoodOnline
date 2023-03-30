using MongoDB.Bson.Serialization.Conventions;

namespace MongoDB.Bson.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class BsonScalarDsicriminatorAttribute : Attribute, IBsonClassMapAttribute
{
    public Type Type { get; }

    public string TypeDiscriminatorPropertyName { get; }

    public BsonScalarDsicriminatorAttribute(Type type, string typeDiscriminatorPropertyName)
    {
        Type = type;
        TypeDiscriminatorPropertyName = typeDiscriminatorPropertyName;
    }

    public void Apply(BsonClassMap classMap)
    {
        BsonSerializer.RegisterDiscriminatorConvention(Type, new ScalarDiscriminatorConvention(TypeDiscriminatorPropertyName));
    }
}

public class BsonScalarDsicriminatorAttribute<T> : BsonScalarDsicriminatorAttribute
{
    public BsonScalarDsicriminatorAttribute(string typeDiscriminatorPropertyName)
        : base(typeof(T), typeDiscriminatorPropertyName)
    {
    }
}
