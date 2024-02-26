using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Migrations;
using ShopManagement.Models;
using ShopManagement.Services;
using ShopManagement.ViewModels;
using System.Diagnostics;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AnnouncementService _announcementService;

        public HomeController(AnnouncementService announcementService)
        {
            _announcementService= announcementService;
        }

        public IActionResult Index()
        {
            var announcement = _announcementService.GetAll();
            foreach(var item in announcement.ToList())
            {
                if (item.Date <= DateTime.Now)
                {
                    _announcementService.Delete(item.Id);
                    announcement.Remove(item);
                }
                    
            }
            var announcements = announcement.OrderByDescending(x => x.Id);
            return View(announcements);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnouncementViewModel announcement)
        {
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                var newAnnouncement = new Announcement()
                {
                    Title = announcement.Title,
                    Description = announcement.Description,
                    Date = announcement.Date,
                    Added = date,
                    UserId = int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value)
                };
            
                _announcementService.Add(newAnnouncement);
                return RedirectToAction("Index");

            }
             return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var announcement = _announcementService.GetById(id);
            if(announcement != null)
            _announcementService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var announcement = _announcementService.GetById(id);
            return View(announcement);
        }

        [HttpPost]
        public IActionResult Edit (Announcement announcement)
        {
            var editAnnoucement = _announcementService.GetById(announcement.Id);
            editAnnoucement.Title = announcement.Title;
            editAnnoucement.Description = announcement.Description;
            editAnnoucement.Date = announcement.Date;
            _announcementService.Update(editAnnoucement);
            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}