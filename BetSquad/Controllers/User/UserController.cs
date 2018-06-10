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
            var user = await _userManager.GetUserAsync(User);
            var model = new UserIndexModel();
            model.Bets = await _userService.GetBetsDTO(user.Id);
            model.Games= await _userService.GetAvlblGamesDTO(user.Id);
            return View(model);
        }

        public async Task<ActionResult> GamesToBet()
        {
            var user = await _userManager.GetUserAsync(User);
            var games = await _userService.GetAvlblGamesDTO(user.Id);

            return View(games);
        }

        public async Task<ActionResult> MyBets()
        {
            var user = await _userManager.GetUserAsync(User);
            var bets = await _userService.GetBetsDTO(user.Id);

            return View(bets);
        }

        public async Task<ActionResult> MyWaitingBets()
        {
            var user = await _userManager.GetUserAsync(User);
            var bets = await _userService.GetWaitingBetsDTO(user.Id);

            return View(bets);
        }

        public async Task<ActionResult> MyFinishedBets()
        {
            var user = await _userManager.GetUserAsync(User);
            var bets = await _userService.GetFinishedBetDTO(user.Id);

            return View(bets);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public async Task<ActionResult> CreateBet(Guid gameId)
        {
            var game = await _commonService.GetGame(gameId);
            
            return View(new UserCreateBetModel() {GameDTO = game});
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
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> EditBet(Guid betId)
        {
            var user = await _userManager.GetUserAsync(User);
            var bet = await _userService.GetBetDTO(user.Id,betId);

            return View(new UserEditBetModel() { GameDTO = bet.Game, BetId = bet.Id });
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
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}