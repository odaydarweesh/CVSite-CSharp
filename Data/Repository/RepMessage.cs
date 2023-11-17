using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
   public class RepMessage
    {
        public Datacontext db { get; set ; }

        public RepMessage()
        {
            db = new Datacontext();
        }

      
        
        public int HämtaAntalOlästMeddlande(int id)


        {
            return db.UserMessages.Where(x => x.Read == false && x.RecievingUser == id).Count();

        }
        
        public List<UserMessage> HämtaMeddelandeUserGenomIdUser(int id)

        {
            return db.UserMessages.Where(x => x.RecievingUser == id).ToList();

        }

        public Message HämtaMeddelandeGenomMeddelandeId(int id)
        {

            return db.Messages.Where(x => x.MessageID == id).FirstOrDefault();
        }

     
        public void SparaMeddelande(Message msg)
        {
            db.Messages.Add(msg);

            db.SaveChanges();
        }

        public void MeddlandeUserSpara(UserMessage userMessage)
        {


            db.UserMessages.Add(userMessage);


            db.SaveChanges();
        }

        public void MarkeraLäst(int id)
        {
            UserMessage meddleande = db.UserMessages.Where(x => x.MessageID == id).FirstOrDefault();

            meddleande.Read = true;

            db.SaveChanges();
        }

        public void MarkAllAsRead(int userID)
        {
            List<UserMessage> userMessages = HämtaMeddelandeUserGenomIdUser(userID);

            foreach (var item in userMessages)

            {

                item.Read = true;

            }
            db.SaveChanges();
        }
    }
}
