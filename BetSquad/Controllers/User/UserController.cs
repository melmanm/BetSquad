using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetSquad.Core.Domain;
using BetSquad.Infrastructure.Services;
using BetSquad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetSquad.Controllers.User
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(IUserService userService, ICommonService commonService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _commonService = commonService;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        // GET: User
        public async Task<ActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var model = new UserIndexModel();
                model.Bets = await _userService.GetBetsDTO(user.Id);
                model.Games = await _userService.GetAvlblGamesDTO(user.Id);
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public async Task<ActionResult> GamesToBet()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var games = await _userService.GetAvlblGamesDTO(user.Id);

                return View(games);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public async Task<ActionResult> MyBets()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var bets = await _userService.GetBetsDTO(user.Id);

                return View(bets);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public async Task<ActionResult> MyWaitingBets()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var bets = await _userService.GetWaitingBetsDTO(user.Id);

                return View(bets);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        public async Task<ActionResult> MyFinishedBets()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var bets = await _userService.GetFinishedBetDTO(user.Id);

                return View(bets);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public async Task<ActionResult> CreateBet(Guid gameId)
        {
            try
            {
                var game = await _commonService.GetGame(gameId);

                return View(new UserCreateBetModel() { GameDTO = game });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBet(UserCreateBetModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _userService.CreateBet(user.Id, model.GameDTO.Id, model.score1, model.score2);

                return RedirectToAction(nameof(MyBets));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> EditBet(Guid betId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var bet = await _userService.GetBetDTO(user.Id, betId);

                return View(new UserEditBetModel() { GameDTO = bet.Game, BetId = bet.Id });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBet(UserEditBetModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _userService.EditBet(user.Id, model.BetId, model.score1, model.score2);

                return RedirectToAction(nameof(MyBets));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // GET: User/Delete/5
      
    }
}