using Dunet;
using System.Text.Json.Serialization;

namespace FoodOnline.Auths;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$auth")]
[JsonDerivedType(typeof(Default), typeDiscriminator: nameof(Default))]
[JsonDerivedType(typeof(GitHub), typeDiscriminator: nameof(GitHub))]
[JsonDerivedType(typeof(Facebook), typeDiscriminator: nameof(Facebook))]

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
