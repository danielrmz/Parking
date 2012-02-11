using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Parking.UI.Classes
{
    using Resources = Sieena.Parking.Common.Resources;
    using Models = Sieena.Parking.API.Models;
    using Sieena.Parking.API.Models.Exceptions;

    internal static class MembershipExtensions
    {
        internal static MembershipUser ToMembership(this Models.User u)
        {
            return new MembershipUser("ParkingMembershipProvider", 
                                        u.Email, 
                                        (object)u.UserId, 
                                        u.Email,
                                        string.Empty, 
                                        string.Empty, 
                                        u.IsActive, 
                                        false, 
                                        u.CreatedAt, 
                                        u.LastAccess.HasValue ? u.LastAccess.Value : DateTime.MinValue,
                                        DateTime.Now, 
                                        DateTime.Now, 
                                        DateTime.Now);
        }
    }

    public class ParkingMembershipProvider : MembershipProvider
    {

        #region Base Methods

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            Models.User user = Models.User.SaveUser(new Models.User() { 
                 CreatedAt = DateTime.Now,
                 Email = email,
                 IsActive = true,
                 Password = password
            });

            status = MembershipCreateStatus.Success;

            return user.ToMembership();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return Models.User.DeleteUser(username);   
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Models.User> users = Models.User.FindByEmail(emailToMatch);
            totalRecords = users.Count;

            users = users.Skip(pageSize * pageIndex).Take(pageSize).ToList();

            MembershipUserCollection collection = new MembershipUserCollection();

            users.ForEach(u => collection.Add(u.ToMembership()));

            return collection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return FindUsersByEmail(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            List<Models.User> users = Models.User.GetAll();
            totalRecords = users.Count;

            MembershipUserCollection collection = new MembershipUserCollection();

            users.ForEach(u => collection.Add(u.ToMembership()));

            return collection;
        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            Models.User u = Models.User.GetByEmail(username);
            return u.ToMembership();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return Models.User.GetById((providerUserKey as int?).Value).ToMembership();        
        }

        public override string GetUserNameByEmail(string email)
        {
            return email;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return true;
        }

        public override void UpdateUser(MembershipUser user)
        {
            Models.User u = Models.User.GetByEmail(user.Email);
            u.Password = user.GetPassword();
            u.LastAccess = user.LastActivityDate;
            u.IsActive = user.IsApproved;
            u.CreatedAt = user.CreationDate;

            Models.User.SaveUser(u);
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                return Models.User.VerifyCredentials(username, password);
            }
            catch (APIException e)
            {
                return false;
            }
        }

        #endregion

        #region Configuration Properties

        public override string ApplicationName
        {
            get
            {
                return Resources.UI.Membership_AppId;
            }
            set { }
        }

        public override bool EnablePasswordReset
        {
            get {
                return false;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get {
                return false;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return int.MaxValue; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 5; }
        }

        public override int PasswordAttemptWindow
        {
            get { return int.MaxValue; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ".*"; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        #endregion

        #region Not Implemented Methods

        public override string GetPassword(string username, string answer)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        public override bool UnlockUser(string userName)
        {
            throw new Exception(Resources.UI.Membership_NotImplemented);
        }

        #endregion
    }
}