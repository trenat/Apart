using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using test3.Data;

namespace test3.Services
{
    public class MySignInManager : SignInManager<User>
    {
        public MySignInManager(MyUserManager userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            ;
        }
        
    }
}
