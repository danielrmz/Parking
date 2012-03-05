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
    public static class TropoFactory
    {
        private static Tropo GetInstance() {
            return new Tropo();
        }

        public static string ParseSession(StreamReader reader, Dictionary<string, List<string>> messages)
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
                        tropo.Message(new Say(msg), new List<string>() { to }, false, Channel.Text, from, "parkingapp@hotmail.com", Network.Msn, false, 30);
                    }
                }
            }
            catch
            {
            }  
            return tropo.RenderJSON();
        }

        public static void SendMessage(string to, string body)
        {
            //inst.CreateSession("");
            string from = "parkingapp@hotmail.com";
             
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("to", to);
            //parameters.Add("fromNumber", from);
            parameters.Add("channel", Channel.Text);
            parameters.Add("say", body);
           // parameters.Add("customerName", "customer Name");
            parameters.Add("network", Network.Msn);
            Tropo inst = GetInstance();

            XmlDocument doc = new XmlDocument();

            string token = "0e098127c8bbff4f94763796f933f4ff252a36ef3cb41b0f011bc911f6ee26e1155bf7946b1fd3d1addc316f";

            doc.Load(inst.CreateSession(token, parameters));

            string success = doc.SelectSingleNode("session/success").InnerText.ToUpper();
            string tokenStr   = doc.SelectSingleNode("session/token").InnerText;

            return;
        }
    }
}
 