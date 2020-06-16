using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Infrastructure.Identity.Exceptions;
using FoodOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOnline.Infrastructure.Identity
{
    public class FoodOnlineUserManager : IIdentityService
    {
        protected readonly FoodOnlineDbContext context;

        public FoodOnlineUserManager(FoodOnlineDbContext context)
        {
            this.context = context;
        }

        /// <summary>Authenticate if the user credentials provided are correct.</summary>
        /// <param name="email">The user email.</param>
        /// <param name="password">The user password.</param>
        /// <returns>
        /// null if the user is not found or the password is wrong otherwise returns the user.
        /// </returns>
        public virtual FoodOnlineUser Authenticate(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = context.Users.AsNoTracking().SingleOrDefault(x => x.Email == email);

            // return null if user not found
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            user.PasswordHash = string.Empty;
            user.PasswordSalt = string.Empty;

            context.Entry(user).State = EntityState.Detached;

            return user;
        }

        /// <summary>Get all users, this is a linq select, users are not tracked..</summary>
        /// <returns>All the users.</returns>
        public virtual IEnumerable<FoodOnlineUser> GetAll()
        {
            return context.Users.AsNoTracking()
                .Select(x => new FoodOnlineUser
                {
                    Id = x.Id,
                    ConfirmedEmail = x.ConfirmedEmail,
                    Email = x.Email,
                    Username = x.Username,
                    PasswordHash = string.Empty,
                    PasswordSalt = string.Empty
                });
        }

        /// <summary>Get a user using the id property. The user will not be tracked.</summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>The user if found or null.</returns>
        public virtual Task<FoodOnlineUser> GetById(string id)
        {
            return context.Users.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new FoodOnlineUser
                {
                    Id = x.Id,
                    ConfirmedEmail = x.ConfirmedEmail,
                    Email = x.Email,
                    Username = x.Username,
                    PasswordHash = string.Empty,
                    PasswordSalt = string.Empty
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the username or null if the user is not found.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<string> GetUsernameAsync(string userId)
        {
            return context.Users.AsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => x.Username)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Will use the provided user and password to fill the hash and salt. The id will be filled
        /// using the <see cref="GenerateId"/> Will return true if the method succeeded.
        /// </summary>
        /// <param name="user">
        /// A user to be saved with all necessary parameters filled except the id. this will be
        /// filled from <see cref="GenerateId"/>
        /// </param>
        /// <param name="password">The password to hash.</param>
        /// <returns>True if the user was stored false otherwise.</returns>
        /// <exception cref="NoPasswordException">No password was provided.</exception>
        /// <exception cref="EmailTakenException">Email already exists.</exception>
        public virtual string Create(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new NoPasswordException();
            if (context.Users.Any(x => x.Email == email)) throw new EmailTakenException(email);

            PasswordHelper.CreatePasswordHash(password,
                out string passwordHash,
                out string passwordSalt);

            var user = new FoodOnlineUser
            {
                Username = username,
                Email = email,
                Id = IdGenerator.Generate(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            context.Users.Add(user);
            context.SaveChanges();
            context.Entry(user).State = EntityState.Detached;

            return user.Id;
        }

        /// <summary>
        /// Update a user's email, username and password inside the database if the value is provided.
        /// </summary>
        /// <param name="userParam">
        /// A user object that should populate the values that need to be update and leave the rest empty.
        /// </param>
        /// <param name="password">The user password..</param>
        /// <param name="newPassword">The new password to change if provided.</param>
        /// <exception cref="FoodOnlineUserNotFoundException">When the user is not found.</exception>
        /// <exception cref="NoPasswordException">When no password is provided.</exception>
        /// <exception cref="WrongPasswordException">When the password provided is wrong.</exception>
        /// <exception cref="EmailTakenException">When the email already exists.</exception>
        public virtual void Update(FoodOnlineUser userParam, string password, string newPassword)
        {
            var user = GetUser(userParam.Id);
            PasswordNotEmpty(password);
            VerifyPassword(user, password!);

            // update email if provided
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new email already exists.
                if (context.Users.AsNoTracking().Any(x => x.Email == userParam.Email))
                    throw new EmailTakenException(userParam.Email);

                user.Email = userParam.Email;
            }

            // update username if provided
            if (!string.IsNullOrWhiteSpace(userParam.Username))
            {
                user.Username = userParam.Username;
            }

            // update password if provided
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                PasswordHelper.CreatePasswordHash(newPassword,
                    out string passwordHash,
                    out string passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            context.SaveChanges();
            context.Entry(user).State = EntityState.Detached;
        }

        /// <summary>Delete a user only if he can be validated. Otherwise an exception is thrown.</summary>
        /// <param name="id"></param>
        /// <exception cref="FoodOnlineUserNotFoundException"/>
        /// <exception cref="NoPasswordException"/>
        /// <exception cref="WrongPasswordException"/>
        public virtual void Delete(string id)
        {
            var user = new User { Id = id };

            context.Entry(user).State = EntityState.Deleted;
            context.SaveChanges();
        }

        #region Helpers

        /// <summary>
        /// It will return the user for the id this entity is tracked, if the user is not found it
        /// will throw an exception.
        /// </summary>
        /// <exception cref="FoodOnlineUserNotFoundException"/>
        protected FoodOnlineUser GetUser(string id)
        {
            return context.Users.Find(id) ?? throw new FoodOnlineUserNotFoundException();
        }

        /// <summary>If the passowrd is null, empty, or whitespace it will throw an exception.</summary>
        /// <exception cref="NoPasswordException"/>
        protected static void PasswordNotEmpty([AllowNull] string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new NoPasswordException();
        }

        /// <summary>Will verify the password using <see cref="Password.VerifyPasswordHash(string, string, string)"/></summary>
        /// <param name="user">The user whose password will be verified.</param>
        /// <param name="password">The password to verify.</param>
        /// <exception cref="WrongPasswordException">If the password is wrong.</exception>
        protected static void VerifyPassword(FoodOnlineUser user, string password)
        {
            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                throw new WrongPasswordException();
        }

        #endregion
    }
}