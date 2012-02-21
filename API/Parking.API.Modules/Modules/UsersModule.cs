/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using Sieena.Parking.API.Models;

namespace Sieena.Parking.API.Modules
{
    using Classes;
    using APISession = Sieena.Parking.API.Models.Session;
    using Sieena.Parking.API.Models.Views;

    public class UsersModule : AbstractBaseModule
    {
        public UsersModule()
            : base("users")
        {
        }

        [Api("/{id}", ApiMethod.GET, true)]
        public UserInfo GetUser(User u, APISession session, DynamicDictionary parameters)
        {
            return UserInfo.GetByUserId(parameters["id"]);
        }

        [Api("/", ApiMethod.POST, true)]
        public UserInfo AddUser(User u, APISession session, DynamicDictionary parameters)
        {

            UserInfo s = parameters.Fill<UserInfo>();

            return UserInfo.Save(s);
        }

        [Api("/{id}", ApiMethod.PUT, true)]
        public UserInfo UpdateUser(User u, APISession session, DynamicDictionary parameters)
        {
            UserInfo s = parameters.Fill<UserInfo>();
            
            return UserInfo.Save(s); 
        }

        [Api("/{id}", ApiMethod.DELETE, true, AccessLevel.Admin)]
        public User DeleteUser(User u, APISession session, DynamicDictionary parameters)
        {
            return null;
        }

        [Api("/GetAll", ApiMethod.GET, true)]
        public List<UserInfo> GetUsersInformation(User u, APISession session, DynamicDictionary parms) {
            List<User> users = User.GetAll();
            List<UserInfo> uis = new List<UserInfo>();

            users.ForEach(ux => {
                uis.Add(User.GetBasicUserInformation(ux));
            });

            return uis;
        }
    }
}