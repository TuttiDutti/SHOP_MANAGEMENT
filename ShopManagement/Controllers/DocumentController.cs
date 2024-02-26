using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Models;
using ShopManagement.Services;
using System.Security.Claims;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private DocumentService _documentService;
        private UserService _userService;
        private DocumentTypeService _documentTypeService;

        public DocumentController(DocumentService documentService, UserService userService, DocumentTypeService documentTypeService)
        {
            _documentService = documentService;
            _userService = userService;
            _documentTypeService = documentTypeService;
        }

        public IActionResult Index(string searchString)
        {
            ViewBag.Search = searchString;
            if (User.IsInRole("Admin"))
            {
                var documents = _documentService.GetAll();
                var documentTypes = _documentTypeService.GetAll();
                var users = _userService.GetAll();
                if (!String.IsNullOrEmpty(searchString))
                {
                    documents = documents.Where(s => s.FileName.Contains(searchString) || s.Description.Contains(searchString)).ToList();
                }
                return View(documents);

            }
            else
            {
                var documents = _documentService.GetPublicDocument();
                var documentTypes = _documentTypeService.GetAll();
                if (!String.IsNullOrEmpty(searchString))
                {
                    documents = documents.Where(s => s.FileName.Contains(searchString) || s.Description.Contains(searchString)).ToList();
                }
                var users = _userService.GetAll();
                return View(documents);

            }

        }

        [HttpGet]
        public IActionResult Upload() 
        {
            var type = _documentTypeService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Types = type;
            return View("Upload");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, Document document)
        {
            if (file != null && file.Length > 0)
            {
                   if(!User.IsInRole("Admin")) document.IsPrivate = false;

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileData = memoryStream.ToArray();

                            var newDocument = new Document()
                            {
                                UserId = int.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value),
                                DocumentTypeId = document.DocumentTypeId,
                                FileName = file.FileName,
                                UploadDate = DateTime.UtcNow,
                                IsPrivate = document.IsPrivate,
                                Description = document.Description,
                                Content = fileData 
                            };
                            _documentService.Add(newDocument);
                    }
                    return RedirectToAction("Index");
            }
                
         
            else
            {
                var type = _documentTypeService.GetAll().Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();

                ViewBag.Types = type;
                return View();
            }
        }

        public IActionResult Download(int id)
        {
            var document = _documentService.GetAll().FirstOrDefault(d => d.Id == id);

            if (document != null)
            {
                var stream = new MemoryStream(document.Content);
                return File(stream, "application/octet-stream", document.FileName);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var doc = _documentService.GetById(id);
            if (doc == null)
                return NotFound();
            _documentService.Delete(id);
            return RedirectToAction("Index");
        }

        
        public IActionResult DocumentType()
        {
            var types = _documentTypeService.GetAll();
            return View(types);
        }

        [HttpGet]
        public IActionResult CreateType()
        {
           
            return View("CreateType");
        }

        
        [HttpPost]
        public IActionResult CreateType(DocumentType documentType)
        {
            _documentTypeService.Add(documentType);

            return RedirectToAction("DocumentType");
        }
    }
}
