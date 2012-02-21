using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;

/**
 * PubNub 3.0 Real-time Push Cloud API
 *
 * @author Stephen Blum
 * @package pubnub
 */
public static class PubnubFactory {

    public static Pubnub GetInstance()
    {
        return new Pubnub(
            ConfigurationManager.AppSettings["PUBNUB.PublishKey"],  // PUBLISH_KEY
            ConfigurationManager.AppSettings["PUBNUB.SubscribeKey"],  // SUBSCRIBE_KEY
            ConfigurationManager.AppSettings["PUBNUB.SecretKey"],      // SECRET_KEY
            false    // SSL_ON?
        );
    }

    public struct Channels {
        public static string CheckinHistory = "parking:checkins:history";
    }
}
