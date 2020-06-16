namespace FoodOnline.Infrastructure.Identity
{
    public class FoodOnlineUser
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual bool ConfirmedEmail { get; set; }

        public virtual string Username { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string PasswordSalt { get; set; }

        public FoodOnlineUser()
        {
        }

        public FoodOnlineUser(string id, string email, bool confirmedEmail, string username, string passwordHash, string passwordSalt)
        {
            Id = id;
            Email = email;
            ConfirmedEmail = confirmedEmail;
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}