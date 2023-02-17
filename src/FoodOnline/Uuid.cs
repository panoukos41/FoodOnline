﻿using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodOnline;

public readonly struct Uuid : IParsable<Uuid>
{
    public static Uuid Empty { get; } = new Uuid();

    private readonly string nanoId;

    public Uuid()
    {
        nanoId = string.Empty;
    }

    private Uuid(string nanoId)
    {
        this.nanoId = nanoId;
    }

    public static Uuid NewUuid()
    {
        return new(Nanoid.Generate(size: 11));
    }

    public override string ToString()
    {
        return nanoId;
    }

    #region IParsable

    public static Uuid Parse(string s) => Parse(s, null);

    public static Uuid Parse(string s, IFormatProvider? provider)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentException("Uuid can't be null empty or whitespace.", nameof(s));
        if (s.Length != 11)
            throw new ArgumentException($"Invalid uuid length.");

        return new Uuid(s);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Uuid result) => TryParse(s, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Uuid result)
    {
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = Empty;
            return false;
        }
    }

    #endregion

    #region Equals

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Uuid other && Equals(other);

    public bool Equals(Uuid other) => nanoId == other.nanoId;

    public override int GetHashCode() => nanoId.GetHashCode();

    public static bool operator ==(Uuid left, Uuid right) => left.Equals(right);

    public static bool operator !=(Uuid left, Uuid right) => !(left == right);

    #endregion
}

public sealed class UuidJsonConverter : JsonConverter<Uuid>
{
    public override Uuid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Uuid.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Uuid value, JsonSerializerOptions options)
    {
        writer.WriteRawValue($"\"{value}\"");
    }
}

// MIT License
// 
// Copyright(c) 2017 zhu yu
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

/// <summary>
/// 
/// </summary>
file static class Nanoid
{
    private const string DefaultAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly CryptoRandom Random = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alphabet"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Task<string> GenerateAsync(string alphabet = DefaultAlphabet, int size = 21)
        => Task.FromResult(Generate(Random, alphabet, size));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alphabet"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string Generate(string alphabet = DefaultAlphabet, int size = 21)
        => Generate(Random, alphabet, size);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="random"></param>
    /// <param name="alphabet"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string Generate(Random random, string? alphabet = DefaultAlphabet, int size = 21)
    {
        if (random == null)
        {
            ArgumentNullException.ThrowIfNull(random);
        }

        if (alphabet == null)
        {
            ArgumentException.ThrowIfNullOrEmpty(alphabet);
        }

        if (alphabet.Length <= 0 || alphabet.Length >= 256)
        {
            throw new ArgumentOutOfRangeException(nameof(alphabet), "alphabet must contain between 1 and 255 symbols.");
        }

        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "size must be greater than zero.");
        }

        // See https://github.com/ai/nanoid/blob/master/format.js for
        // explanation why masking is use (`random % alphabet` is a common
        // mistake security-wise).
        var mask = (2 << 31 - Clz32((alphabet.Length - 1) | 1)) - 1;
        var step = (int)Math.Ceiling(1.6 * mask * size / alphabet.Length);

        Span<char> idBuilder = stackalloc char[size];
        Span<byte> bytes = stackalloc byte[step];
        int cnt = 0;
        while (true)
        {
            random.NextBytes(bytes);

            for (var i = 0; i < step; i++)
            {

                var alphabetIndex = bytes[i] & mask;

                if (alphabetIndex >= alphabet.Length) continue;
                idBuilder[cnt] = alphabet[alphabetIndex];
                if (++cnt == size)
                {
                    return new string(idBuilder);
                }
            }
        }
    }

    /// <summary>
    /// Counts leading zeros of <paramref name="x"/>.
    /// </summary>
    /// <param name="x">Input number.</param>
    /// <returns>Number of leading zeros.</returns>
    /// <remarks>
    /// Courtesy of spender/Sunsetquest see https://stackoverflow.com/a/10439333/623392.
    /// </remarks>
    internal static int Clz32(int x)
    {
        const int numIntBits = sizeof(int) * 8; //compile time constant
        //do the smearing
        x |= x >> 1;
        x |= x >> 2;
        x |= x >> 4;
        x |= x >> 8;
        x |= x >> 16;
        //count the ones
        x -= x >> 1 & 0x55555555;
        x = (x >> 2 & 0x33333333) + (x & 0x33333333);
        x = (x >> 4) + x & 0x0f0f0f0f;
        x += x >> 8;
        x += x >> 16;
        return numIntBits - (x & 0x0000003f); //subtract # of 1s from 32
    }
}

/// <inheritdoc />
/// <summary>
/// </summary>
file class CryptoRandom : Random
{
    private static readonly RandomNumberGenerator _r;
    private readonly byte[] _uint32Buffer = new byte[4];

    static CryptoRandom()
    {
        _r = RandomNumberGenerator.Create();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void NextBytes(byte[] buffer)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        _r.GetBytes(buffer);
    }

    /// <inheritdoc/>
    public override void NextBytes(Span<byte> buffer)
    {
        RandomNumberGenerator.Fill(buffer);
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public override double NextDouble()
    {
        Span<byte> uint32Buffer = stackalloc byte[4];
        RandomNumberGenerator.Fill(uint32Buffer);
        return BitConverter.ToUInt32(uint32Buffer) / (1.0 + uint.MaxValue);
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    public override int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue) throw new ArgumentOutOfRangeException(nameof(minValue));
        if (minValue == maxValue) return minValue;
        var range = (long)maxValue - minValue;
        return (int)((long)Math.Floor(NextDouble() * range) + minValue);
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public override int Next()
    {
        return Next(0, int.MaxValue);
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    public override int Next(int maxValue)
    {
        if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));
        return Next(0, maxValue);
    }
}
