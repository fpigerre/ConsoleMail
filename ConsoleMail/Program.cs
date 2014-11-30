using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ConsoleApplication1
{
    class Program
    {
        private static string emailAddress = Environment.GetEnvironmentVariable("sendemailfrom");
        private static string emailPassword = Environment.GetEnvironmentVariable("sendemailpassword");

        static void Main(string[] args)
        {
            // Get email information from console input
            string recipient = getRecipient();
            Console.WriteLine("Subject:");
            string subject = Console.ReadLine();
            Console.WriteLine("Body:");
            string body = Console.ReadLine();

            mail(recipient, subject, body);
        }

        static string getRecipient()
        {
            Console.WriteLine("Recipient:");
            string recipient = Console.ReadLine();

            /* Ensure that the recipient string contains an '@' symbol
               Othweise, continue looping through the method */
            if (!recipient.Contains("@"))
            {
                getRecipient();
            }
            return recipient;
        }

        static void mail(string recipient, string subject, string body)
        {
            try
            {
                // Create SMPT client with SSL and input credentials
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailAddress, emailPassword);

                // Send email
                client.Send(recipient, emailAddress, subject, body);

                Console.WriteLine("Success! Your message has been sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
