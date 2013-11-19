using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyStore.Domain.Security;
using SharpLite.Domain;

namespace MyStore.Domain
{
    public class User : EntityWithTypedId<string>, IUser
    {
        public virtual string UserName { get; set; }

        public virtual IList<Login> Logins { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual IList<UserClaim> Claims { get; set; }

        public virtual string PasswordHash { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            
            Logins = new List<Login>();
            Roles = new List<Role>();
            Claims = new List<UserClaim>();
        }

        public virtual string SecurityStamp { get; set; }
        /*
      ,[UserName]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[Discriminator]
      ,[ApplicationId]
      ,[LegacyPasswordHash]
      ,[LoweredUserName]
      ,[MobileAlias]
      ,[IsAnonymous]
      ,[LastActivityDate]
      ,[MobilePIN]
      ,[Email]
      ,[LoweredEmail]
      ,[PasswordQuestion]
      ,[PasswordAnswer]
      ,[IsApproved]
      ,[IsLockedOut]
      ,[CreateDate]
      ,[LastLoginDate]
      ,[LastPasswordChangedDate]
      ,[LastLockoutDate]
      ,[FailedPasswordAttemptCount]
      ,[FailedPasswordAttemptWindowStart]
      ,[FailedPasswordAnswerAttemptCount]
      ,[FailedPasswordAnswerAttemptWindowStart]
      ,[Comment]
         */
    }
}
