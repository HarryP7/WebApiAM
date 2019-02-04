using System;
using System.Collections.Generic;
using WebApiAM.Models;
using WebApiAM.Helpers;
using WebApiAM.Repository;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiAM.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        public UserService(AppDbContext context){
            this.context = context;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = Get(u => u.Email == email);

            // check if username exists and password is correct
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Необходим пароль");

            if (Get(u => u.Email == user.Email) != null)
                throw new AppException($"Пользователь с электронной почтой {user.Email} уже существует.");
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Add(user);
            context.SaveChanges();

            return user;
        }
        public User Get(int id)
        {
            try{ return context.Users.Where(u => u.Id == id).Single(); }
            catch{ return null; }
        }     
        public User Get(Func<User, bool> predicate)
        {
            try{ return context.Users.Where(predicate).Single(); }
            catch{ return null; }
        }

        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            context.Remove(new User() { Id = id });
        }

        public IEnumerable<User> Fetch(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate).Include(u => u.Role);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}