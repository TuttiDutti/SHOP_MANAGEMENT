using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Models;
using ShopManagement.Services;
using ShopManagement.ViewModels;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class AbsenceController : Controller
    {
        private UserService _userService;
        private AbsenceService _absenceService;
        private AbsenceTypeService _absenceTypeService;

        public AbsenceController(UserService userService, AbsenceService absenceService, AbsenceTypeService absenceTypeService)
        {
            _userService = userService;
            _absenceService = absenceService;
            _absenceTypeService = absenceTypeService;
        }

        public ActionResult Index(int filterUser, int filterType, string sortOrder)
        {
            var absences = _absenceService.GetAll();
            var absenceTypes = _absenceTypeService.GetAll();
            var users = _userService.GetAll();

            var user = _userService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name + " " + r.LastName,
            }).ToList();

            ViewBag.Users = user;

            var type = _absenceTypeService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Types = type;

            if(filterUser > 0)
            {
                absences = absences.Where(u => u.UserId == filterUser).ToList();
            }
            if (filterType > 0)
            {
                absences = absences.Where(u => u.AbsenceTypeId == filterType).ToList();
            }

            ViewBag.FromSortParm = sortOrder == "From" ? "from_desc" : "From";
            ViewBag.ToSortParm = sortOrder == "To" ? "to_desc" : "To";

            switch (sortOrder)
            {
                case "from_desc":
                    absences = absences.OrderByDescending(s => s.DateFrom).ToList();
                    break;
                case "To":
                    absences = absences.OrderBy(s => s.DateTo).ToList();
                    break;
                case "to_desc":
                    absences = absences.OrderByDescending(s => s.DateTo).ToList();
                    break;
                default:
                    absences = absences.OrderBy(s => s.DateFrom).ToList();

                    break;
            }

            return View(absences);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var type = _absenceTypeService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
            var user = _userService.GetById(
                        int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));
            ViewBag.VacationDays = user.VacationDays;

            ViewBag.Types = type;
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(AbsenceViewModel absence)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetById(
                        int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));
                var hours = user.VacationDays;
                TimeSpan time = absence.DateTo - absence.DateFrom;


                if (absence.AbsenceTypeId == 1)
                {
                    if (hours < (time.TotalDays + 1))
                    {
                        var absenceType = _absenceTypeService.GetAll().Select(r => new SelectListItem
                        {
                            Value = r.Id.ToString(),
                            Text = r.Name
                        }).ToList();
                        ViewBag.Types = absenceType;
                        ViewBag.VacationDays = user.VacationDays;                       
                        ViewBag.Valid = "Przekroczono dostępny urlop";
                        return View();
                    }
                }
                var newAbsence = new Absence()
                {
                    DateFrom = absence.DateFrom,
                    DateTo = absence.DateTo,
                    UserId = int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value),
                    AbsenceTypeId = absence.AbsenceTypeId
                };
                if (absence.AbsenceTypeId == 1) newAbsence.Status = "Planowany";
                else newAbsence.Status = "Zaakceptowany";

                _absenceService.Add(newAbsence);

                hours = hours - (int)time.TotalDays - 1;
                user.VacationDays = hours;
                _userService.Update(user);

                return RedirectToAction("Index");
            }

            var type = _absenceTypeService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Types = type;

            return View();

        }

       

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var absence = _absenceService.GetById(id);           

            if (absence == null)
                return NotFound();

            var user = _userService.GetById(
                       int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));
            var hours = user.VacationDays;
            TimeSpan time = absence.DateTo - absence.DateFrom;
            hours = hours + (int)time.TotalDays + 1;
            user.VacationDays = hours;
            _userService.Update(user);
            _absenceService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Accept(int id)
        {
            var absence = _absenceService.GetById(id);
            absence.Status = "Zaakceptowany";
            _absenceService.Update(absence);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cancel(int id)
        {
            var absence = _absenceService.GetById(id);
            absence.Status = "Anulowany";

            var user = _userService.GetById(
                       int.Parse(HttpContext.User.Claims.FirstOrDefault(u => u.Type.Contains("nameidentifier")).Value));
            var hours = user.VacationDays;
            TimeSpan time = absence.DateTo - absence.DateFrom;
            hours = hours + (int)time.TotalDays +1;
            user.VacationDays = hours;
            _userService.Update(user);

            _absenceService.Update(absence);
            return RedirectToAction("Index");
        }


    }
}
