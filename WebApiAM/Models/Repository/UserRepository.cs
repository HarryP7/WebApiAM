using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiAM.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiAM.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context){
            this.context = context;
        }
        public void Create(User user){
            context.Add(user);
        }
        public void Delete(User user){
            context.Remove(context.Users.Where(u => u.Id == user.Id).Single());
        }

        public IEnumerable<User> Fetch(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate).Include(u => u.Role);
        }

        //метод возвращает null, если пользователь не существует
        public User Get(int id)
        {
            try {
                return context.Users.Where(u => u.Id == id).Single();
            } catch {
                return null;
            }
        }

        //метод возвращает null, если пользователь не существует
        public User Get(Expression<Func<User, bool>> predicate)
        {
            try {
                return context.Users.Where(predicate).Single();
            } catch {
                return null;
            }
        }

        public void Update(User user)
        {
            context.Update(user);
        }
    }
}