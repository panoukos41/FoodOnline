namespace FoodOnline.Domain.Common
{
    /// <summary>An entity that will have login credentials.</summary>
    public abstract class LoginEntity
    {
        /// <summary>
        /// The Username used to login.
        /// </summary>
        public string Username { get; set; } = default!;

        /// <summary>
        /// The has of the password.
        /// </summary>
        public string PasswordHash { get; set; } = default!;

        /// <summary>
        /// The salt used to hash the password.
        /// </summary>
        public string PasswordSalt { get; set; } = default!;
    }
}