using System.Net;
using System.Net.Mail;

namespace SportsResults;

public class EmailService {
    public static void SendEmail(string emailBody) {
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;
        string emailFromAddress = "brettkohler93@gmail.com";
        string password = "pmvs zrdh gxdy arvh";
        string emailToAddress = "brettkohler93@gmail.com";
        string subject = "NBA Scores";

        using(MailMessage mail = new MailMessage()) {  
                        mail.From = new MailAddress(emailFromAddress);  
                        mail.To.Add(emailToAddress);  
                        mail.Subject = subject;  
                        mail.Body = emailBody;  
                        mail.IsBodyHtml = true;  

                        using(SmtpClient smtp = new SmtpClient(smtpAddress, portNumber)) {  
                            smtp.Credentials = new NetworkCredential(emailFromAddress, password);  
                            smtp.EnableSsl = enableSSL;  
                            smtp.Send(mail);  
                        }  
                    }
            }
}