using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;
using TropoCSharp;
using TropoCSharp.Tropo;
using TropoCSharp.Structs;
using System.Xml;

namespace Sieena.Parking.Common.Utils
{
    /// <summary>
    /// Wrapper to abstract Tropo's service logic. 
    /// </summary>
    public static class TropoFactory
    {
        private static Tropo GetInstance() {
            return new Tropo();
        }

        /// <summary>
        /// Must be used from an endpoint URL. 
        /// This will parse the active session and send the specified messages. 
        /// As of now, it only supports sending through the MSN Network IM
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static string ExecuteSession(StreamReader reader, Dictionary<string, List<string>> messages)
        {
            string sessionJSON = reader.ReadToEnd();
            Tropo tropo = new Tropo();
            try
            {
                Session tropoSession = new Session(sessionJSON);

                foreach (var kvp in messages)
                {
                    string to = kvp.Key;
                    string from = "parkingapp@hotmail.com";

                    foreach (string msg in kvp.Value)
                    {
                        tropo.Message(new Say(msg), new List<string>() { to }, false, Channel.Text, from, from, Network.Msn, false, 30);
                    }
                }
                
            }
            catch
            {
            }  
            return tropo.RenderJSON();
        }

        /// <summary>
        /// Creates a session for the Tropo Service
        /// </summary> 
        public static void CreateSession()
        {
            Tropo inst = GetInstance();

            XmlDocument doc = new XmlDocument();

            string token = ConfigurationManager.AppSettings["Tropo.APIKey"];

            doc.Load(inst.CreateSession(token));

            string success  = doc.SelectSingleNode("session/success").InnerText.ToUpper();
            string tokenStr = doc.SelectSingleNode("session/token").InnerText;

            return;
        }
    }
}
 