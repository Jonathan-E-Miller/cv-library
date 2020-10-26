using CvWebApi.Database;
using CvWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CvWebApi.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthenticationController : ControllerBase
  {
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
      _dbContext = context;
      _userManager = userManager;
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
      if (_dbContext.Users.Where(u => u.Email == newUser.Email).ToList().Count == 0)
      {
        IdentityResult result = _userManager.CreateAsync(identityUser).Result;
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
  }
}
