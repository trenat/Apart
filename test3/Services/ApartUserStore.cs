using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace test3.Services
{
    public class ApartUserStore : IUserStore<User>,
                                  IUserPasswordStore<User>
    {
        private eadiApartDbContext _dbContext;
        private UserManager<User> tuser;

        public ApartUserStore()
        {
            _dbContext = new eadiApartDbContext();
        }
        
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            //            _dbContext.User.Add(new User(){ Password = user.Password, Email=user.Email,  );
            // user.UserId = null;

            _dbContext.User.Add(user);
            _dbContext.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        //    throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            
            return _dbContext.User.FirstOrDefaultAsync<User>(b => b.UserID.ToString() == userId);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _dbContext.User.FirstOrDefaultAsync(b=> b.Email == normalizedUserName); 
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.Password);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.UserID.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.Name.ToString());
        }
        

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
           // _dbContext.Set<User>().Update(user);
            _dbContext.SaveChanges();
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            
            return Task.FromResult<User>(user);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
