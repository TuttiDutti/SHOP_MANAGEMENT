using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Models;
using ShopManagement.Services;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private PhotoService _photoService;

        public PhotoController(PhotoService photoService)
        {
            _photoService = photoService;
        }
        public IActionResult Index(int id)
        {
            var photos = _photoService.GetByProduct(id);
            ViewBag.ProdId = id;
            return View(photos);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var photo = _photoService.GetById(id);
            var photos = _photoService.GetByProduct(photo.ProductId);
            foreach (var item in photos)
            {
                if (item.IsMain == true)
                {
                    item.IsMain = false;
                    _photoService.Update(item);
                    break;
                }
            }

            photo.IsMain = true;
            _photoService.Update(photo);
                return RedirectToAction("Index", new {id = photo.ProductId });
        }
        [HttpGet]
        public IActionResult AddPhoto(int id)
        {
            var photo = new Photo { ProductId = id };
            ViewBag.ProdId = id;
            return View(photo);
        }
        [HttpPost]
        public async Task<IActionResult> AddPhoto(Photo pht)
        {
            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {
                var uploadedPhotos = new List<Photo>();

                foreach (var file in HttpContext.Request.Form.Files)
                {
                        
                        byte[] fileBytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            fileBytes = memoryStream.ToArray();
                        }

                        
                        var photo = new Photo
                        {
                            Title = file.FileName,
                            Content = fileBytes,
                            IsMain = false,
                            ProductId = pht.ProductId
                        };

                        _photoService.Add(photo);
                        uploadedPhotos.Add(photo);                   
                }
                return RedirectToAction("Index", new { id = pht.ProductId });


            }

            return BadRequest("Brak przesłanych plików.");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prodId = _photoService.GetById(id).ProductId;
            _photoService.Delete(id);
            return RedirectToAction("Index", new { id = prodId });
        }


    }
}
