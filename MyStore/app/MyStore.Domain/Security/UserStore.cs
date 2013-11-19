﻿using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Linq;
using SharpLite.Domain.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyStore.Domain.Security
{
    public class UserStore<TUser> :
        IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,    
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IDisposable where TUser : User
    {
        #region constructor and properties 

        private readonly ISession _session;

        
        private readonly IRepository<TUser> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserClaim> _claimRepository;
        
        //private readonly Lazy Module

        public UserStore()
        {
            var sessionFactory = DependencyResolver.Current.GetService<ISessionFactory>();
            _session = sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Always;

            _userRepository = DependencyResolver.Current.GetService<IRepository<TUser>>();
            _roleRepository = DependencyResolver.Current.GetService<IRepository<Role>>();
            _claimRepository = DependencyResolver.Current.GetService<IRepository<UserClaim>>();
        }

        public void Dispose()
        {
            _session.Close();
        }

        #endregion

        #region Claims

        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            var item = new UserClaim
            {
                User = user,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
            user.Claims.Add(item);
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var claims = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult<IList<Claim>>(claims);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            var toRemove = user.Claims.Where(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type).ToList();
            foreach (var userClaim in toRemove)
            {
                _claimRepository.Delete(userClaim);    
            }
            
            return Task.FromResult(0);
        }

        #endregion

        #region Logins

        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var item = new Login
            {
                User = user,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            };
            user.Logins.Add(item);
            return Task.FromResult(0);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var logins = user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var loginEntry =
                user.Logins.FirstOrDefault(
                    l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);
            if (loginEntry != null)
            {
                user.Logins.Remove(loginEntry);
            }
            return Task.FromResult(0);
        }

        #endregion

        #region Roles

        public Task AddToRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException("role");
            }

            var roleEntry = _roleRepository.GetAll().FirstOrDefault(r => r.Name.ToUpper() == role.ToUpper());
            if (roleEntry == null)
            {
                throw new InvalidOperationException(string.Format("Can't find role '{0}'", role));
            }
            user.Roles.Add(roleEntry);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<IList<string>>(user.Roles.Select(r => r.Name).ToList());
        }

        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException("role");
            }

            var roleEntry = user.Roles.FirstOrDefault(r => r.Name.ToUpper() == role.ToUpper());
            if (roleEntry != null)
                user.Roles.Remove(roleEntry);
            return Task.FromResult(0);
        }

        #endregion

        #region Users

        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            try
            {
                var classes = _session.SessionFactory.GetAllClassMetadata();

                _session.SaveOrUpdate(user);
            }
            catch (Exception e)
            {
                throw;
            }

            return Task.FromResult(0);
        }

        public Task DeleteAsync(TUser user)
        {
            _userRepository.Delete(user);

            return Task.FromResult(0);
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            return Task.Factory.StartNew(() => _userRepository.GetAll().FirstOrDefault(u => u.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey)));
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return Task<TUser>.Factory.StartNew(() => _userRepository.GetAll().FirstOrDefault(u => u.Id == userId));
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return Task<TUser>.Factory.StartNew(() =>
                {
                    var user = _session.Query<TUser>().FirstOrDefault(u => u.UserName == userName);
                    return user;
                }
            );
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<bool> IsInRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult(user.Roles.Any(r => r.Name.ToUpper() == role.ToUpper()));
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => _userRepository.SaveOrUpdate(user));
        }

        #endregion

        #region Passwords

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PasswordHash);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        #endregion
    }
}
