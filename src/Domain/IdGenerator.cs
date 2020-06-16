using System;

namespace FoodOnline.Domain
{
    public static class IdGenerator
    {
        public static string Generate() => Guid.NewGuid().ToString("N");
    }
}