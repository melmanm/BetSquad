﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetSquad.Core.Domain;
using BetSquad.Infrastructure.Services;
using BetSquad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetSquad.Controllers.Common
{
    [Authorize]
    public class CommonController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CommonController(IUserService userService, ICommonService commonService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _commonService = commonService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _commonService.GetUsersDTO();
                return View(users);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public async Task<ActionResult> FinishedBets(Guid userId)
        {
            try
            {
                var user = await _commonService.GetUserDTO(userId);
                var bets = await _commonService.GetFinishedBetDTO(userId);
                var model = new CommonUserFinishedBetsModel() { FinishedBets = bets, Score = user.Score, Email = user.Email, NickName = user.NickName };
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }
    }
}