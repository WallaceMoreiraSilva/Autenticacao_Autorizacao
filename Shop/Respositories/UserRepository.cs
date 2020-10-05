using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Respositories
{
    public class UserRepository
    {
        public UserModel Get (string username, string password)
        {
            var users = new List<UserModel>();

            users.Add(new UserModel
            {
                Id = 1,
                UserName = "batman",
                Password = "batman",
                Role = "manager"
            });

            users.Add(new UserModel
            {
                Id = 2,
                UserName = "robin",
                Password = "robin",
                Role = "employee"
            });

            var usuario = users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();

            return usuario;
        }
    }
}
