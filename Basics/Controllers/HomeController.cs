using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Basics.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
      return View();

    }

    public IActionResult Authenticate()
    {
      var grandmaClaims = new List<Claim>() {
        new Claim(ClaimTypes.Name, "Bob"),
        new Claim(ClaimTypes.Email, "Bob@fmail.com"),
        new Claim("Grandma.Says", "Very nice boi")
      };
      var licenseClaims = new List<Claim>() {
        new Claim(ClaimTypes.Name, "Bob K Foo"),
        new Claim("DrivingLicencense", "A")
      };

      var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
      var licIdentity = new ClaimsIdentity(licenseClaims, "Governemnt");

      var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licIdentity });
      
      HttpContext.SignInAsync(userPrincipal);

      return RedirectToAction("Index");
    }

  }
}
