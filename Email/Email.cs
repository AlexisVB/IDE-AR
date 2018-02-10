using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace IDE_AR.Email
{
    public class email
    {
        //Las credenciales del correo de la aplicación IDE-AR
        //correo: ide.ar.asistencia@gmail.com
        //contrseña: e31d9348
        private string from;
        private string to;
        private string subject;
        private string message;
        private string correo;
        private string password;
        public email()
        {

        }
        public email(string t,string s,string m)
        {
            correo = "id.ar.asistencia@gmail.com";
            password = "e31d9348";
            from = correo;
            to = t;
            subject = s;
            message = m;
        }
        public bool sendEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(from, password);
                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(to);
                msgObj.From = new MailAddress(from);
                msgObj.Subject = subject;
                msgObj.Body = message;
                client.Send(msgObj);
                return true;
            }
            catch
            {
                return false;
            }           
        }
    }
}
