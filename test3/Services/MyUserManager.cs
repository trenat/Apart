using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using test3.Data;
using Microsoft.EntityFrameworkCore;
using test3.Models;

namespace test3.Services
{
    public class MyUserManager : UserManager<User>
    {
        public MyUserManager(ApartUserStore store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, Microsoft.Extensions.Logging.ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
 

        //public override Task<User> FindByNameAsync(string userName)
        //{
        //    //int i = 1;
        //    //var u = this.Users;
        //    return Store.FindByNameAsync("xd", new System.Threading.CancellationToken());
        //    // return await base.FindByNameAsync(userName);
        //}
    }
}
