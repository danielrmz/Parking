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
    /// <summary>
    /// Exposes only the required functions for MailGun API
    /// </summary>
    public static class EmailerFactory
    {
        /// <summary>
        /// Gets an instance with the credentials set
        /// </summary>
        /// <returns></returns>
        private static MailgunClient GetInstance() {
            string domain = ConfigurationManager.AppSettings["MailGun.Domain"];
            string key = ConfigurationManager.AppSettings["MailGun.APIKey"];
            return new MailgunClient(domain, key);
        }

        /// <summary>
        /// Sends an email 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendMail(string to, string subject, string body)
        {
            string from = ConfigurationManager.AppSettings["MailGun.FromEmail"];
            GetInstance().SendMail(new MailMessage(from, to, subject, body));
        }
    }
}
 