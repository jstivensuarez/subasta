using Subasta.core.constants;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Subasta.core.helpers
{
    public class CorreoHelper : ICorreoHelper
    {
        public void enviarDesdeSubasta(string mensaje, string asunto, string destino)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            try
            {
                string origen = Correos.SUBASTAEMAIL;
                string clave = Correos.SUBASTACLAVE;

                MailMessage mailMessage = new MailMessage(origen, destino, asunto, $"<p>{mensaje}</p>");
                mailMessage.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(origen, clave);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar enviar el correo", ex);
            }
            finally {
                smtpClient.Dispose();
            }
            
        }
    }
}
