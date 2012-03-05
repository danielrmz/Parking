using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;
using Typesafe.Mailgun;
using System.Net.Mail;

namespace Sieena.Parking.Common.Utils
{
    public static class EmailerFactory
    {
        private static MailgunClient GetInstance() {
            string domain = ConfigurationManager.AppSettings["MailGun.Domain"];
            string key = ConfigurationManager.AppSettings["MailGun.APIKey"];
            return new MailgunClient(domain, key);
        }

        public static void SendMail(string to, string subject, string body)
        {
            string from = ConfigurationManager.AppSettings["MailGun.FromEmail"];
            GetInstance().SendMail(new MailMessage(from, to, subject, body));
        }
    }
}
 