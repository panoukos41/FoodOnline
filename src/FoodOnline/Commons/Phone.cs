using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Commons;

public sealed record Phone : IValid
{

    public static Phone Empty { get; } = new();

    public static IValidator Validator => throw new NotImplementedException();
}
