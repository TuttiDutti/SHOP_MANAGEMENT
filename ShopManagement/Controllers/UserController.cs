using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Services;
using ShopManagement.ViewModels;
using System.Data;
using System.Security.Claims;

namespace ShopManagement.Controllers
{

    public class UserController : Controller
    {
        public const string SessionKeyUserId = "_UserId";
        public const string SessionKeyRole = "_Role";
        public const string SessionKeyTime = "_LoginTime";

        private UserService _userService;
        private RoleService _roleService;

        public UserController(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index(int filter, string searchString, string sortOrder)
        {
            var users = _userService.GetAll();
            var roles = _roleService.GetAll();
            var userRoles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = userRoles;
            ViewBag.Search = searchString;
            ViewBag.UserRole = filter;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString) || s.LastName.Contains(searchString)).ToList();
            }

            if (filter > 0)
            {
                users = users.Where(r => r.RoleId == filter).ToList();
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.LastName).ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.LastName).ToList();
                    break;
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text= r.Name
            }).ToList();
            
            ViewBag.Roles = roles;

            return View("Create");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = PasswordHasher.HashPassword(user.Password);
                user.Password = hashedPassword;

                var newUser = new User()
                {
                    Name= user.Name,
                    Email= user.Email,
                    LastName= user.LastName,
                    Login = user.Login,
                    Password= hashedPassword,
                    LogTime= DateTime.MinValue,
                    isBlocked= false,
                    RoleId= user.RoleId,
                    Number= user.Number,
                    VacationDays= user.VacationDays,
                     
                };
                
                _userService.Add(newUser);
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            var roles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel usr)
        {
            if(ModelState.IsValid){
                var user = _userService.GetNotBlocked().FirstOrDefault(u => u.Login == usr.Login);
                var roles = _roleService.GetAll();
                var roleDictionary = roles.ToDictionary(role => role.Id.ToString(), role => role.Name);
                if (user == null)
                {
                    ViewBag.Valid = "Login jest niepoprawny";
                    return View("Login");
                }
                bool isPassValid = PasswordHasher.VerifyPassword(usr.Password, user.Password);

                if (isPassValid)
                {
                    var time = DateTime.Now;
                    user.LogTime = time;
                    _userService.Update(user);
                    var roleName = roleDictionary.ContainsKey(user.RoleId.ToString()) ? roleDictionary[user.RoleId.ToString()] : "Unknown Role";
                    HttpContext.Session.SetInt32(SessionKeyUserId, user.Id);
                    HttpContext.Session.SetString(SessionKeyRole, roleName);
                    HttpContext.Session.SetString(SessionKeyTime, time.ToString());
                    var name = user.Name + " " + user.LastName;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Actor, name),
                        new Claim(ClaimTypes.Role, roleName),
                        new Claim(ClaimTypes.SerialNumber, user.Login),
                        new Claim(ClaimTypes.DateOfBirth, user.LogTime.ToString()),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));


                    return RedirectToAction("Index", "Home");

                }
                ViewBag.Valid = "Hasło jest niepoprawne";
                return View();
            }

            
            return View("Login");

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            var roles = _roleService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;

            var model = new User
            {
                Id = id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Number = user.Number,
                RoleId = user.RoleId,
                VacationDays = user.VacationDays,
                isBlocked= user.isBlocked
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(User usr)
        {

            var user = _userService.GetById(usr.Id);
            if (user == null)
                return NotFound();
            user.Name = usr.Name;
            user.LastName = usr.LastName;
            user.Email = usr.Email;
            user.Login = usr.Login;
            user.Number = usr.Number;
            user.RoleId = usr.RoleId;
            user.VacationDays = usr.VacationDays;
            user.isBlocked = usr.isBlocked;

            _userService.Update(user);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var roles = _roleService.GetAll();
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();


            return View(user);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            _userService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassViewModel changePass)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetById(
                       int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));

                bool isPassValid = PasswordHasher.VerifyPassword(changePass.OldPassword, user.Password);

                if (isPassValid)
                {
                    if (changePass.NewPassword != changePass.ConfirmPassword)
                    {
                        ViewBag.Valid = "Hasła się różnią";
                        return View();
                    }

                    var hashedPass = PasswordHasher.HashPassword(changePass.NewPassword);
                    user.Password = hashedPass;
                    _userService.Update(user);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
           


    }
}
