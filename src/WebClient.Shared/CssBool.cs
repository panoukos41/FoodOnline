using System;

namespace FoodOnline.WebClient
{
    /// <summary>
    /// A class to display different css based on a boolean value.
    /// </summary>
    public class CssBool
    {
        private bool _value;

        /// <summary>
        /// Css to display based on the current value.
        /// </summary>
        public string Css => Value ? CssTrue : CssFalse;

        /// <summary>
        /// Css to display when value equals true.
        /// </summary>
        public string CssTrue { get; }

        /// <summary>
        /// Css to display when value equals false.
        /// </summary>
        public string CssFalse { get; }

        /// <summary>
        /// The value to watch for. This value can be set
        /// only when the predicate is not set.
        /// </summary>
        public bool Value
        {
            get => Predicate == null ? _value : Predicate.Invoke();
            set => _value = Predicate is null
                ? value
                : throw new InvalidOperationException("Can't set Value because a predicate is provided");
        }

        /// <summary>
        /// A predicate to calculate the Value.
        /// </summary>
        public Func<bool> Predicate { get; }

        /// <summary>
        /// Initialize a new <see cref="CssBool"/> with default value of false.
        /// </summary>
        /// <param name="cssTrue">Css to display when value equals true.</param>
        /// <param name="cssFalse">Css to display when value equals false.</param>
        public CssBool(string cssTrue, string cssFalse = null)
        {
            CssTrue = cssTrue;
            CssFalse = cssFalse;
        }

        /// <summary>
        /// Initialize a new <see cref="CssBool"/>.
        /// </summary>
        /// <param name="value">Default value.</param>
        /// <param name="cssTrue">Css to display when value equals true.</param>
        /// <param name="cssFalse">Css to display when value equals false.</param>
        public CssBool(bool value, string cssTrue, string cssFalse = null)
        {
            Value = value;
            CssTrue = cssTrue;
            CssFalse = cssFalse;
        }

        /// <summary>
        /// Initialize a new <see cref="CssBool"/> with a predicate that will calculate the value.
        /// </summary>
        /// <param name="predicate">The prediacte that will be called to calculate the new value.</param>
        /// <param name="cssTrue">Css to display when value equals true.</param>
        /// <param name="cssFalse">Css to display when value equals false.</param>
        public CssBool(Func<bool> predicate, string cssTrue, string cssFalse = null)
        {
            Predicate = predicate;
            CssTrue = cssTrue;
            CssFalse = cssFalse;
        }

        /// <summary>
        /// Can't be called with predicate.
        /// </summary>
        public void Revert()
        {
            Value = !Value;
        }

        /// <summary>
        /// Returns the <see cref="Css"/> property.
        /// </summary>
        public override string ToString() => Css;
    }
}