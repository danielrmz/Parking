using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;

/// <summary>
/// Pubnub class Factory
/// </summary>
public static class PubnubFactory {

    /// <summary>
    /// Singleton that returns a new instance of PubNub with the keys already set up.
    /// </summary>
    /// <returns></returns>
    public static Pubnub GetInstance()
    {
        return new Pubnub(
            ConfigurationManager.AppSettings["PUBNUB.PublishKey"],  // PUBLISH_KEY
            ConfigurationManager.AppSettings["PUBNUB.SubscribeKey"],  // SUBSCRIBE_KEY
            ConfigurationManager.AppSettings["PUBNUB.SecretKey"],      // SECRET_KEY
            false    // SSL_ON?
        );
    }

    /// <summary>
    /// Channels available for MyPlace
    /// </summary>
    public struct Channels {
        public static string CheckinHistory = "parking:checkins:history";
        public static string CheckinCurrent = "parking:checkins:current";
        public static string Users          = "parking:users";
        public static string GeneralNotification = "parking:notifications";
    }
}
