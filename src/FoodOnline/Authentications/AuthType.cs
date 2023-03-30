using Dunet;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace FoodOnline.Authentications;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$auth")]
[JsonDerivedType(typeof(Default), typeDiscriminator: nameof(Default))]
[JsonDerivedType(typeof(GitHub), typeDiscriminator: nameof(GitHub))]
[JsonDerivedType(typeof(Facebook), typeDiscriminator: nameof(Facebook))]

[BsonScalarDsicriminator<AuthType>("$auth")]

[Union]
public partial record AuthType
{
    public partial record Default;

    public partial record GitHub;

    public partial record Facebook;

    public sealed override string ToString()
    {
        return GetType().Name;
    }
}
