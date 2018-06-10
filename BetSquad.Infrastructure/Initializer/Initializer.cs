using BetSquad.Core.Domain;
using BetSquad.Core.Repository;
using BetSquad.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BetSquad.Infrastructure.Initializer
{
    public class Initializer : IInitializer
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<ApplicationRole> _roleRepository;
        private readonly IRepository<ApplicationUserRole> _userRoleRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public Initializer(IRepository<Game> gameRepository, IRepository<Team> teamRepository, 
            IRepository<ApplicationUser> userRepository, IRepository<ApplicationRole> roleRepository, 
            IRepository<ApplicationUserRole> userRoleRepository, RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;

            
        }

        public void Initialize()
        {
            createTeamsAndGames();
            // _gameRepository.Insert(new Game(DateTime.Parse("2018-09-11 22:30"), poland, russia, GameType.Group, "Testowy"));
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {


            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                // first we create Admin rool    
                var role = new ApplicationRole();
                role.Name = "Admin";
                var createResult = _roleManager.CreateAsync(role).Result;

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "maciej_admin@betsquad.com";
                user.Email = "maciej_admin@betsquad.com";
                user.NickName = "Maciej_S";
                string userPWD = "Admin1!";

                var user2 = new ApplicationUser();
                user2.UserName = "michal_admin@betsquad.com";
                user2.Email = "michal_silski@betsquad.com";
                user2.NickName = "Michal_S";
                string user2PWD = "Admin1!";


                var createUserResult = _userManager.CreateAsync(user, userPWD).Result;
                var createUserResult2 = _userManager.CreateAsync(user2, user2PWD).Result;

                var addResullt2 = _userManager.AddToRoleAsync(user, "Admin").Result;
                var addResullt = _userManager.AddToRoleAsync(user2,"Admin").Result;
            }
            

        }
        private void createTeamsAndGames()
        {
            if (_gameRepository.GetAll().Result.Count == 0)
            {
                var Uruguay = new Team(Group.A, "Uruguay");
                var Egypt = new Team(Group.A, "Egypt");
                var Russia = new Team(Group.A, "Russia");
                var Saudi_Arabia = new Team(Group.A, "Saudi_Arabia");
                var Portugal = new Team(Group.B, "Portugal");
                var Spain = new Team(Group.B, "Spain");
                var Iran = new Team(Group.B, "Iran");
                var Morocco = new Team(Group.B, "Morocco");
                var France = new Team(Group.C, "France");
                var Peru = new Team(Group.C, "Peru");
                var Denmark = new Team(Group.C, "Denmark");
                var Australia = new Team(Group.C, "Australia");
                var Argentina = new Team(Group.D, "Argentina");
                var Croatia = new Team(Group.D, "Croatia");
                var Iceland = new Team(Group.D, "Iceland");
                var Nigeria = new Team(Group.D, "Nigeria");
                var Brazil = new Team(Group.E, "Brazil");
                var Switzerland = new Team(Group.E, "Switzerland");
                var Costa_Rica = new Team(Group.E, "Costa_Rica");
                var Serbia = new Team(Group.E, "Serbia");
                var Germany = new Team(Group.F, "Germany");
                var Mexico = new Team(Group.F, "Mexico");
                var Sweden = new Team(Group.F, "Sweden");
                var Korea_Republic = new Team(Group.F, "Korea_Republic");
                var Belgium = new Team(Group.G, "Belgium");
                var England = new Team(Group.G, "England");
                var Tunisia = new Team(Group.G, "Tunisia");
                var Panama = new Team(Group.G, "Panama");
                var Poland = new Team(Group.H, "Poland");
                var Colombia = new Team(Group.H, "Colombia");
                var Senegal = new Team(Group.H, "Senegal");
                var Japan = new Team(Group.H, "Japan");

                _teamRepository.Insert(Uruguay);
                _teamRepository.Insert(Egypt);
                _teamRepository.Insert(Russia);
                _teamRepository.Insert(Saudi_Arabia);
                _teamRepository.Insert(Portugal);
                _teamRepository.Insert(Spain);
                _teamRepository.Insert(Iran);
                _teamRepository.Insert(Morocco);
                _teamRepository.Insert(France);
                _teamRepository.Insert(Peru);
                _teamRepository.Insert(Denmark);
                _teamRepository.Insert(Australia);
                _teamRepository.Insert(Argentina);
                _teamRepository.Insert(Croatia);
                _teamRepository.Insert(Iceland);
                _teamRepository.Insert(Nigeria);
                _teamRepository.Insert(Brazil);
                _teamRepository.Insert(Switzerland);
                _teamRepository.Insert(Costa_Rica);
                _teamRepository.Insert(Serbia);
                _teamRepository.Insert(Germany);
                _teamRepository.Insert(Mexico);
                _teamRepository.Insert(Sweden);
                _teamRepository.Insert(Korea_Republic);
                _teamRepository.Insert(Belgium);
                _teamRepository.Insert(England);
                _teamRepository.Insert(Tunisia);
                _teamRepository.Insert(Panama);
                _teamRepository.Insert(Poland);
                _teamRepository.Insert(Colombia);
                _teamRepository.Insert(Senegal);
                _teamRepository.Insert(Japan);


                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-14 15:00"), Russia, Saudi_Arabia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-15 12:00"), Egypt, Uruguay, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-15 18:00"), Portugal, Spain, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-15 15:00"), Morocco, Iran, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-16 10:00"), France, Australia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-16 16:00"), Peru, Denmark, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-16 13:00"), Argentina, Iceland, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-16 19:00"), Croatia, Nigeria, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-17 18:00"), Brazil, Switzerland, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-17 12:00"), Costa_Rica, Serbia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-17 15:00"), Germany, Mexico, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-18 12:00"), Sweden, Korea_Republic, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-18 15:00"), Belgium, Panama, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-18 18:00"), Tunisia, England, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-19 15:00"), Poland, Senegal, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-19 12:00"), Colombia, Japan, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-19 18:00"), Russia, Egypt, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-20 15:00"), Uruguay, Saudi_Arabia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-20 12:00"), Portugal, Morocco, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-20 18:00"), Iran, Spain, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-21 15:00"), France, Peru, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-21 12:00"), Denmark, Australia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-21 18:00"), Argentina, Croatia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-22 15:00"), Nigeria, Iceland, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-22 12:00"), Brazil, Costa_Rica, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-22 18:00"), Serbia, Switzerland, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-23 18:00"), Germany, Sweden, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-23 15:00"), Korea_Republic, Mexico, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-23 12:00"), Belgium, Tunisia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-24 12:00"), England, Panama, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-24 18:00"), Poland, Colombia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-24 15:00"), Japan, Senegal, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-25 14:00"), Uruguay, Russia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-25 14:00"), Saudi_Arabia, Egypt, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-25 18:00"), Iran, Portugal, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-25 18:00"), Spain, Morocco, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-26 14:00"), Denmark, France, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-26 14:00"), Australia, Peru, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-26 18:00"), Nigeria, Argentina, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-26 18:00"), Iceland, Croatia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-27 18:00"), Serbia, Brazil, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-27 18:00"), Switzerland, Costa_Rica, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-27 14:00"), Korea_Republic, Germany, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-27 14:00"), Mexico, Sweden, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-28 18:00"), England, Belgium, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-28 18:00"), Panama, Tunisia, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-28 14:00"), Japan, Poland, GameType.Group, ""));
                    _gameRepository.Insert(new Game(DateTime.Parse("2018-06-28 14:00"), Senegal, Colombia, GameType.Group, ""));

                     _gameRepository.SaveChanges();
                     _teamRepository.SaveChanges();
            }
                
            
        }
    
    }
}
