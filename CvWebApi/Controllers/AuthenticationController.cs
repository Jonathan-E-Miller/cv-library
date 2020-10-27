using CvWebApi.Database;
using CvWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CvWebApi.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthenticationController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult RegisterUser(ApiUser newUser)
    {
      CvApiResponse response = new CvApiResponse();

      // create application user...
      IdentityUser identityUser = new IdentityUser()
      {
        Email = newUser.Email,
        EmailConfirmed = false,
        UserName = newUser.Username
      };

      // ensure unique
      if (_userManager.Users.Where(u => u.Email == newUser.Email).ToList().Count == 0)
      {
        IdentityResult result = _userManager.CreateAsync(identityUser, newUser.Password).Result;
        response.Success = result.Succeeded;
        if (!result.Succeeded)
        {
          response.Message = result.Errors.ToList()[0].Description;
        }
      }
      else
      {
        response.Message = "Email exists";
      }

      return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> LogIn(ApiUser user)
    {
      CvApiResponse response = new CvApiResponse();
      IdentityUser identityUser = await _userManager.FindByEmailAsync(user.Email);

      if (identityUser != null)
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(identityUser, user.Password, user.RememberMe, false);

        if (result.Succeeded)
        {
          response.Success = true;
        }
        else
        {
          response.Success = false;
          response.Message = "Invalid Password";
        }
      }
      return Ok(response);
    }
  }
}
