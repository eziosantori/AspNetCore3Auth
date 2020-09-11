﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
  public class HomeController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager
      )
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }
    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
      return View();

    }
    public IActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
      //login func go here
      var user = await _userManager.FindByNameAsync(username);
      if (user != null)
      {
        //sigin
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (signInResult.Succeeded)
          return RedirectToAction("Index");
      }

      return RedirectToAction("Index");

    }

    public IActionResult Register()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {
      var user = new IdentityUser(username);
      var result = await _userManager.CreateAsync(user, password);

      if (result.Succeeded)
      {
        // signIn User here
        //sigin
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (signInResult.Succeeded)
          return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    public async Task<IActionResult> LogOut()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}
