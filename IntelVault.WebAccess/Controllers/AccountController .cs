﻿using IntelVault.WebAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelVault.WebAccess.Controllers;

public class AccountController(
    IDataProtectionProvider dataProtectionProvider,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : Controller
{
    private readonly IDataProtector _dataProtector = dataProtectionProvider.CreateProtector("SignIn");

    [HttpGet("account/signinactual")]
    public async Task<IActionResult> SignInActual(string t)
    {
        var data = _dataProtector.Unprotect(t);

        var parts = data.Split('|');

        var identityUser = await userManager.FindByIdAsync(parts[0]);

        var isTokenValid = identityUser != null && await userManager.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "SignIn", parts[1]);

        if (isTokenValid)
        {
            await signInManager.SignInAsync(identityUser, true);
            if (parts.Length == 3 && Url.IsLocalUrl(parts[2]))
            {
                return Redirect(parts[2]);
            }
            return Redirect("/");
        }
        else
        {
            return Unauthorized("STOP!");
        }
    }

    [Authorize]
    [HttpGet("account/signout")]
    public async Task<IActionResult> SignOut()
    {
        await signInManager.SignOutAsync();

        return Redirect("/account/signedout");
    }
}