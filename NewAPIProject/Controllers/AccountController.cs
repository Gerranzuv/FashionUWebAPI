using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NewAPIProject.Models;
using NewAPIProject.Providers;
using NewAPIProject.Results;
using System.Linq;
using NewAPIProject.Extra;
using NewAPIProject.ViewModels;

namespace NewAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        ApplicationDbContext db = new ApplicationDbContext();
        CoreController core = new CoreController();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            UserInfoViewModel result = new UserInfoViewModel
            {
                Id = user.Id,
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Country = user.Country,
                Currency = user.Currency,
                Language = user.Language,
                Sex = user.Sex,
                CreationDate = user.CreationDate,
                LastModificationDate = user.LastModificationDate,
                PhoneNumber = user.PhoneNumber,
                companyUser=user.companyUser

            };
            result.UserRoles = userManager.GetRoles(user.Id).ToList() ;
            return result;
            //return Ok(user);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            ApplicationUser temp = db.Users.Where(a => a.Email.Equals(model.Email)).FirstOrDefault();
            if (temp != null)
                return BadRequest("User is already registered");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, CreationDate = DateTime.Now,
                LastModificationDate = DateTime.Now, BirthDate = model.BirthDate, Sex = model.Sex, Country = model.Country, Language = model.Language,
                PhoneNumber = model.PhoneNumber, Currency = model.Currency, Name = model.Name
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            await UserManager.AddToRoleAsync(user.Id, "Guest");
            UserVerificationHelper.VerificationResult r = UserVerificationHelper.generateVerificationLog(user.Id, model.Email);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> VerifyEmail(VerifyEmail model)
        {
            UserVerificationHelper.VerificationResult result = UserVerificationHelper.verifyCode(model.userId, model.code);

            if (result.status.Equals("500"))
                return BadRequest(result.message);
            else
                return Ok(result);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> resendCode(VerifyEmail model)
        {
            if (model.userId == null)
                return BadRequest(" user is null");
            else
            {
                ApplicationUser user = db.Users.Where(a => a.Id.Equals(model.userId)).FirstOrDefault();
                if (user == null)
                    return BadRequest("No matching user");

                UserVerificationHelper.VerificationResult result = UserVerificationHelper.reSendVerificationLog(model.userId, user.Email);
                if (result.status.Equals("500"))
                    return BadRequest(result.message);
                else
                {
                    return Ok(result);
                }
            }

        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        [AllowAnonymous]
        [Route("checkIfEmailAvailbleToUser")]
        public IHttpActionResult checkIfEmailAvailbleToUser(string email)
        {
            ApplicationUser user = db.Users.Where(a => a.Email.Equals(email)).FirstOrDefault();
            if (user != null)
                return BadRequest("User is already registered");

            return Ok();
        }
        [Route("GetProducts")]
        public IQueryable<Product> GetProducts([FromUri] int id)
        {
            return db.Products.Include("Attachments").Include("Comments").Where(a=>a.id.Equals(id));

        }
        [Route("GetProducts")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products.Include("Attachments");

        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
        [Route("BuyProduct")]
        [HttpGet]
        public IHttpActionResult BuyProduct( int ProductId)

        {
            ApplicationUser user = core.getCurrentUser();
            Product product = db.Products.Find(ProductId);
            if (user == null)
            {
                return BadRequest("No Matching User!");
            }
            if (product == null)
            {
                return BadRequest("No Matching Product!");
            }
            //var tokens = db.UsersDeviceTokens.Where(a => a.UserId.Equals(product.Company.CompanyUserId));
            //Add new Product
            //Payment payment= AddNewPayment(product, user);

            //Add Shipping Request
            ShippingRequest request = addShippingRequest(user,product);

            //Send Notification to Company User
            sendNotification(user,request);

            
            return Ok();
        }

        private ShippingRequest addShippingRequest(ApplicationUser user, Product product)
        {
            ShippingRequest request = new ShippingRequest();
            request.productId = product.id;
            request.prodcut = product;
            request.Creator = user.Id;
            request.Modifier = user.Id;
            //request.Payment = payment;
            //request.PaymentId = payment.id;
            request.CreationDate = DateTime.Now;
            request.LastModificationDate = DateTime.Now;
            request.Status = "Active";

            db.ShippingRequests.Add(request);
            db.SaveChanges();
            return request;

        }

        private void sendNotification(ApplicationUser user, ShippingRequest request)
        {
            //Implementation here
        }

        public Payment AddNewPayment(Product product, ApplicationUser user)
        {
            Payment payment = new Payment()
            {
                CreationDate = DateTime.Now,
                Amount = product.Price,
                Company = product.Company,
                CompanyId = product.CompanyId,
                Creator = user.Id,
                Modifier = user.Id,
                LastModificationDate = DateTime.Now,
                Method = "Cash Payment",
                prodcut = product,
                productId = product.id,
                Status = "Done",
                currency = product.Currency
            };
            db.Payments.Add(payment);
            //db.SaveChanges();
            return payment;
        }

        [Route("GetCompanies")]
        public List<CompanyViewModel> GetCompanies()
        {
            return db.Companyies.Select(a => new CompanyViewModel
            {
                CompanyUserId = a.CompanyUserId,
                id = a.id,
                isActive = a.isActive,
                CompanyUserName = a.CompanyUser.Name
            }).ToList() ;

        }

        [Route("GetShippingRequests")]
        public List<ShippingRequest> GetShippingRequests()
        {
            string user = core.getCurrentUser().Id;
            return db.ShippingRequests.Include("prodcut").Where(a => a.prodcut.Company.CompanyUserId.Equals(user)
                    &&(a.Status.Equals("Active")||a.Status.Equals("Scheduled"))).ToList();
        }
    }
}
