using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Sieena.Parking.UI.Classes
{
    using Resources = Sieena.Parking.Common.Resources;
    using Models = Sieena.Parking.API.Models;

    public class ParkingRoleProvider : RoleProvider
    {

        public override string ApplicationName
        {
            get { throw new Exception(Resources.UI.Membership_AppId); }
            set { }
        }

        public override string[] GetAllRoles()
        {
            return Models.Role.GetAll().OrderBy(r => r.RoleLevel).Select(r => r.RoleName).ToArray();   
        }

        public override bool RoleExists(string roleName)
        {
            return this.GetAllRoles().Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return Models.Role.GetRolesForUser(username).Select( r => r.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return Models.Role.GetUsersInRole(roleName).Select(u => u.Email).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return Models.Role.GetRolesForUser(username).Where(r => r.RoleName.Equals(roleName)).Any();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return Models.Role.GetUsersInRole(roleName)
                                .Where(u => u.Email.Contains(usernameToMatch))
                                .Select(u => u.Email).ToArray();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            List<Models.User> users = Models.User.GetAll().Where(u => usernames.Contains(u.Email)).ToList();
            List<Models.Role> roles = Models.Role.GetAll().Where(r => roleNames.Contains(r.RoleName)).ToList();

            users.ForEach(u => {
                Models.User.AddRoles(u, roles);
            });

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            List<Models.User> users = Models.User.GetAll().Where(u => usernames.Contains(u.Email)).ToList();
            List<Models.Role> roles = Models.Role.GetAll().Where(r => roleNames.Contains(r.RoleName)).ToList();

            users.ForEach(u =>
            {
                Models.User.DeleteRoles(u, roles);
            });
        }

        #region Not Implemented on purpose

        public override void CreateRole(string roleName)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        #endregion
    }
}