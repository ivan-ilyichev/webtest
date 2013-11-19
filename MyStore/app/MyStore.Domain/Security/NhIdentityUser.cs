using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyStore.Domain.Security;
using SharpLite.Domain;

namespace MyStore.Domain
{
    public class NhIdentityUser : EntityWithTypedId<string>, IUser
    {
        public virtual string UserName { get; set; }

        public virtual IList<NhIdentityUserLogin> Logins { get; set; }
        public virtual IList<NhIdentityUserRole> Roles { get; set; }
        public virtual IList<NhIdentityUserClaim> Claims { get; set; }

        public virtual string PasswordHash { get; set; }

        public NhIdentityUser()
        {
            Logins = new List<NhIdentityUserLogin>();
            Roles = new List<NhIdentityUserRole>();
            Claims = new List<NhIdentityUserClaim>();
        }

        public virtual string SecurityStamp { get; set; }

        public virtual string Discriminator { get; set; }
        public virtual Guid ApplicationId { get; set; }
        public virtual string LoweredUserName { get; set; }
        public virtual bool IsAnonymous { get; set; }
        public virtual DateTime LastActivityDate { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual bool IsLockedOut { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime LastLoginDate { get; set; }
        public virtual DateTime LastPasswordChangedDate { get; set; }
        public virtual DateTime LastLockoutDate { get; set; }
        public virtual int FailedPasswordAttemptCount { get; set; }
        public virtual DateTime FailedPasswordAttemptWindowStart { get; set; }
        public virtual int FailedPasswordAnswerAttemptCount { get; set; }
        public virtual DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

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
