using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Windows;

namespace CVSite.Controllers
{
    public class ApiMessaController : ApiController
    {
        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();
        private RepMessage repMassage = new RepMessage();







        [Route("api/SendAPI/{id}/{content}/{sender}")]
        [HttpGet]
        public IHttpActionResult SendMessage(int id, string content, string sender)
        {

            using (var db = new Datacontext())
            {
                var reciver = repUser.HämtaUserGenomIdUser(id);
                var msg = new Message
                {
                    Content = content
                };

                if (reciver == null)
                {
                    return BadRequest();
                }
                else
                {
                    repMassage.SparaMeddelande(msg);

                    var usermsg = new UserMessage
                    {
                        RecievingUser = id,
                        MessageID = msg.MessageID,
                        Read = false,
                        Sender = sender,

                    };
                    repMassage.MeddlandeUserSpara(usermsg);
                    return Ok();
                }
            }
        }



        [Route("api/GetApi/")]
        [HttpGet]
        public int GetUnreadMessages()
        {




            string loggedInUserMail = User.Identity.Name.ToString();
            using (var db = new Datacontext())
            {
                User user = repUser.HämtaUserGenomMail(loggedInUserMail);
                int id = user.UserID;
                int unreadcount = repMassage.HämtaAntalOlästMeddlande(id);
                return unreadcount;
            }

        }

    }
}
