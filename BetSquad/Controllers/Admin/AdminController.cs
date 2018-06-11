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

namespace BetSquad.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly IAdminService _adminService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(IUserService userService, ICommonService commonService, IAdminService adminService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _commonService = commonService;
            _userManager = userManager;
            _signInManager = signInManager;
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            return RedirectToAction(nameof(Games));
        }
        // GET: Admin
        public async Task<ActionResult> Games()
        {
            try
            {
                return View(await _commonService.GetGames());
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> SetResult(Guid gameId)
        {
            try
            {
                var game = await _commonService.GetGame(gameId);

                return View(new AdminSetResultModel() { GameDTO = game });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { error = e.Message });
            }
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetResult(AdminSetResultModel model)
        {
            try
            {
                await _adminService.UpdateResult(model.GameDTO.Id, model.score1, model.score2);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddGame()
        {
            var teams = await _commonService.GetTeams();

            return View(new AdminAddGameModel() { Teams = teams });
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddGame(AdminAddGameModel model)
        {
            try
            {
                await _adminService.AddGame(model.GameStartTime, model.Team1Id, model.Team2Id, model.GameType, "");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home",new { error = e.Message });
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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