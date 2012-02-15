﻿/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Web;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using System.Text.RegularExpressions;



namespace Sieena.Parking.API.Modules.Classes
{
    using Sieena.Parking.API.Models;
    using Sieena.Parking.API.Models.Exceptions;
    
    using APISession = Sieena.Parking.API.Models.Session;
    using Resources  = Sieena.Parking.Common.Resources.UI;
    using Sieena.Parking.Common.Utils;
    using System.Configuration;

    /// <summary>
    /// Autoregisters the API Methods based on the attributes
    /// </summary>
    public abstract class AbstractBaseModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="modulePath"></param>
        public AbstractBaseModule(string modulePath)
            : base(modulePath)
        {
            Get["/"] = parameters =>
            {
                Type t = this.GetType();

                List<string> methods = t.GetMethods()
                                        .Where(mi => {
                                            return mi.GetCustomAttributes(typeof(ApiAttribute), true).Any();
                                        })
                                        .Select(mi => {
                                            ApiAttribute attr = mi.GetCustomAttributes(typeof(ApiAttribute), true).First() as ApiAttribute;
                                            
                                            return string.Format("{0}\t{1}\t- {2} {3}", attr.GetMethod(), attr.GetRoute(), mi.Name, attr.IsSecure() ? "(SECURE)" : "");
                                        })
                                        .ToList();

                // Get available public methods to display.
                return Envelope(methods);
            };

            Get["/ping"] = parameters =>
            {
                return Envelope("pong");
            };

            this.RegisterAPIMethods();
        }

        /// <summary>
        /// Registers the API Methods
        /// </summary>
        private void RegisterAPIMethods()
        {
            Type t = this.GetType();
            t.GetMethods()
             .Where(mi =>
             {
                return mi.GetCustomAttributes(typeof(ApiAttribute), true).Any();
             })
             .ToList()
             .ForEach( mi => {
                ApiAttribute tag = mi.GetCustomAttributes(typeof(ApiAttribute), true).First() as ApiAttribute;
                
                Func<dynamic, Response> method = (parameters) => {
                    try
                    {
                        // Authentication
                        if (tag.IsSecure())
                        {
                            var header_token= Context.Request.Headers["x-parking-token"].FirstOrDefault() ?? string.Empty;
                            var header_sign = Context.Request.Headers["x-parking-signature"].FirstOrDefault() ?? string.Empty;

                            // Validate signature and token.
                            string signature = header_sign == string.Empty ? parameters["pk_signature"] : header_sign;
                            string token     = header_token == string.Empty ? parameters["pk_token"] : header_token;
                            string tokenRaw  = string.Empty;

                            try {
                                tokenRaw = Crypto.DecryptStringAES(token, ConfigurationManager.AppSettings["Crypto.Secret"]);
                            } catch(Exception e) {
                                throw new APIException(Resources.API_ErrorInvalidToken);
                            }

                            Guid tokenGuid  = new Guid(tokenRaw);

                            #region Validate session token.

                            APISession sess = APISession.Get(tokenGuid);
                            if (sess == null)
                            {
                                throw new AccessException(Resources.API_ErrorSessionRequired);
                            }

                            User user = User.GetById(sess.UserId.Value);

                            if (tag.GetRoleLevel() > 0)
                            {
                                Role role = Role.GetRolesForUser(user.Email).OrderByDescending( r => r.RoleLevel ).FirstOrDefault();
                                if (role == null)
                                {
                                    throw new APIException(Resources.API_ErrorNoRolesAssigned);
                                }

                                if (role.RoleLevel < tag.GetRoleLevel())
                                {
                                    throw new AccessException(Resources.API_ErrorSessionPrivilegesRequired);
                                }
                            }

                            if (mi.GetParameters().Length != 2)
                            {
                                throw new APIException(Resources.API_ErrorMethodDefinition);
                            }

                            #endregion

                            return Envelope(mi.Invoke(this, new object[] { user, parameters }));

                            //Authorization: "AWS" + " " + AWSAccessKeyID + ":" +   Base64(HMAC-SHA1(UTF-8(Date), UTF-8(AWSSecretAccessKey)))
                        }

                        return Envelope(mi.Invoke(this, new object[] { parameters }));
                    } catch(ModelValidationException me) {
                        Response r = Response.AsJson(new
                        {
                            Time = ConvertToUnixTime(DateTime.Now),
                            Response = me.Errors.ToArray(),
                            Type = "ValidationResult",
                            Error = true
                        });
                        return r;
                    }
                    catch (Exception e) {
                        return Envelope(e);
                    }
                };

                string route = tag.GetRoute();
                switch (tag.GetMethod())
                {
                    case ApiMethod.GET:
                        Get[route] = method;
                        break;
                    case ApiMethod.POST:
                        Post[route] = method;
                        break;
                    case ApiMethod.PUT:
                        Put[route] = method;
                        break;
                    case ApiMethod.DELETE:
                        Delete[route] = method;
                        break;
                    case ApiMethod.GETPOST:
                        Get[route] = method;
                        Post[route] = method;
                        break;
                }
             });
                                        
        }


        /// <summary>
        /// Wraps the data that will be returned into a standard message with additional fixed data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Response Envelope(dynamic data)
        {
            #region Get the type of the Entity
            string type = string.Empty;
            try
            {
                Type t = data.GetType();
                type = t.Name;

                if (t.IsGenericType)
                {
                
                    type = t.GetGenericArguments()[0].Name;
                }
                
            }
            catch (Exception e)
            {
                data = e;
            }

            bool isError = false;
            if (data is Exception)
            {
                isError = true;
                type = data.InnerException != null ? data.InnerException.GetType().Name : type;
                data = data.InnerException != null ? data.InnerException.Message : data.Message;
            }
            #endregion

            #region Calculate Signature
            // Based on Amazon S3 Auth suggestion
            // http://docs.amazonwebservices.com/AmazonCloudFront/latest/DeveloperGuide/RESTAuthentication.html?r=3922



            #endregion

            return Response.AsJson(new {
                Time = ConvertToUnixTime(DateTime.Now),
                Response = data,
                Type = type,
                Error = isError
            });
        }

        /// <summary>
        /// Converts a datetime to unixtime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected double ConvertToUnixTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}