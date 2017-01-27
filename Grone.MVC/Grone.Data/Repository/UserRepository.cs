using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(UserEntityModel entity)
        {
            using (var context = new GroneEntities())
            {
                entity.Salt = CreateSalt(10);
                entity.Password = GenerateSHA256Hash(entity.Password, entity.Salt);

                context.Users.Add(entity);

                context.SaveChanges();
            }
        }

        public void Delete(CommentEntityModel entity)
        {
            ICommentRepository repo = new CommentRepository();

            using (var context = new GroneEntities())
            {
                repo.Delete(entity.Id);

                context.SaveChanges();
            }
        }

        public void Delete(PostEntityModel entity)
        {
            IPostRepository repo = new PostRepository();

            using (var context = new GroneEntities())
            {
                repo.Delete(entity.Id);

                context.SaveChanges();
            }
        }

        public List<UserEntityModel> GetAll()
        {
            using (var context = new GroneEntities())
            {
                return context.Users.ToList();
            }
        }

        public UserEntityModel GetUserById(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public UserEntityModel CheckCredentials(string email, string password)
        {

            using (var context = new GroneEntities())
            {
                foreach (var user in context.Users)
                {
                    if (user.eMail == email)
                        if(GenerateSHA256Hash(password, user.Salt) == user.Password) { return user; }   
                }
                return null;
            }
        }

        public UserEntityModel GetUserByMail(string eMail)
        {
            using (var context = new GroneEntities())
            {
                return context.Users.FirstOrDefault(u => u.eMail == eMail);
            }
        }

        public void Update(UserEntityModel entity)
        {
            entity.Salt = CreateSalt(10);
            entity.Password = GenerateSHA256Hash(entity.Password, entity.Salt);

            using (var context = new GroneEntities())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.Id == entity.Id);

                userToUpdate.eMail = entity.eMail;
                userToUpdate.Fullname = entity.Fullname;
                userToUpdate.Password = entity.Password;

                context.SaveChanges();
            }
        }

        public bool EmailAlreadyExists(string eMail)
        {
            List<UserEntityModel> usersFromDb = GetAll();

            foreach (var user in usersFromDb)
            {
                if (user.eMail == eMail)
                {
                    return true;
                }
            }
            return false;
        }

        private string CreateSalt(int size) //Metod för att skapa salt, tar en inparameter som bestämmer längden på saltet.
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); //Skapar upp en ny "Random generator" från security namespase.
            var buff = new byte[size]; //Skapar upp en array med längden från inparametern.
            rng.GetBytes(buff); //Random genererar bytes i arrayen
            return Convert.ToBase64String(buff); //Konverterar och returnerar det nu färdiga saltet.
        }

        private string GenerateSHA256Hash(string input, string salt) //Metod för att hasha lösenordet tillsammans med ett salt
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt); //Kodar om input tillsammans med saltet och lägger i en byte array
            var sha256hashstring = new System.Security.Cryptography.SHA256Managed(); //Skapar upp en SHA256 krypterare från namespacet security
            byte[] hash = sha256hashstring.ComputeHash(bytes); //Skickar in vår byte variabel till vår SHA256 krypterare och tilldear hashen till en variabel
            return Convert.ToBase64String(hash); //Konverterar hashen till en string och returnerar.
        }
    }
}
