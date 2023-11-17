using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepUser
    {

        public Datacontext _context { get; set; }

        public RepUser()
        {
            _context = new Datacontext();


        }


        public User HämtaUserGenomIdUser(int id)
        {
            return _context.Users.Where(x => x.UserID == id).FirstOrDefault();
        }

        public User HämtaUserGenomMail(string mail)
        {
            return _context.Users.Where(x => x.Email == mail).FirstOrDefault();
        }

        public int HämtaIdUserGenomMail(string mail)
        {
            var userMail = _context.Users.Where(x => x.Email == mail).FirstOrDefault();

            return userMail.UserID;
        }
        public List<User> HämtaAllaUser()

        {

            return _context.Users.ToList();
        }

        public List<User> AllUserSomIntePrivata() 
        {

            List<User> användare = _context.Users.ToList();


            List<User> list = new List<User>();


            foreach (var item in användare)
            {

                if (item.PrivateUser == false)
                {
                    list.Add(item);
                }
            }


            return list;
        }

     

        public void AdderaUser(string mail, string adress, string förnamn, string efternamn)
        {
            _context.Users.Add(new Data.Models.User
            {
                Email = mail,

                Adress = adress,

                Firstname = förnamn,

                Lastname = efternamn
            });

            _context.SaveChanges();
        }
        
      
        public List<User> UsersGenomString(string elements)
        {
            List<string> List = new List<string>();

            if (elements != null && elements.Contains(" "))
            {
                List = elements.Split().ToList();

                string firstName = List.ElementAt(0);

                string lastName = List.ElementAt(1);

                return _context.Users.Where(x => x.Firstname.Contains(firstName) || x.Lastname.Contains(lastName)).ToList();
            }


            return _context.Users.Where(x => x.Firstname.Contains(elements) || x.Lastname.Contains(elements) || elements == null).ToList();
        }

    }

    
}
