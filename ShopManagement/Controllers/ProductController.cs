using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Migrations;
using ShopManagement.Models;
using ShopManagement.Services;
using ShopManagement.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductService _productService;
        private PhotoService _photoService;
        private CategoryService _categoryService;

        public ProductController(ProductService productService, PhotoService photoService, CategoryService categoryService)
        {
            _productService = productService;
            _photoService = photoService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string searchString, string sortOrder, string filter)
        {
            var products = _productService.GetAll();
            var categories = _categoryService.GetAll();
            var photos = _photoService.GetAll();
            

            var productViewModel = products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Company = p.Company,
                    Amount = p.Amount,
                    BarCode = p.BarCode,
                    GrossPrice = p.GrossPrice,
                    NetPrice = p.NetPrice,
                    VAT = p.VAT,
                    Offer = p.Offer,
                    SerialNumber = p.SerialNumber,
                    Category = p.Category.Name,
                    MainPhoto = p.Photos.FirstOrDefault(u => u.IsMain)?.Content,
                    IsBlocked = p.IsBlocked

                });
            var cat = _categoryService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Cat = cat;

            if (!String.IsNullOrEmpty(searchString))
            {
                productViewModel = productViewModel.Where(s => s.Name.Contains(searchString) || s.Company.Contains(searchString)
                                         || s.SerialNumber.Contains(searchString) || s.BarCode.Contains(searchString)).ToList();
            }
            ViewBag.Search = searchString;

            if (!String.IsNullOrEmpty(filter))
            {
                productViewModel = productViewModel.Where(r => r.Category == filter).ToList();
            }

            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CompanySortParm = sortOrder == "Company" ? "company_desc" : "Company";

            switch (sortOrder)
            {
                case "name_desc":
                    productViewModel = productViewModel.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Company":
                    productViewModel = productViewModel.OrderBy(s => s.Company).ToList();
                    break;
                case "company_desc":
                    productViewModel = productViewModel.OrderByDescending(s => s.Company).ToList();
                    break;
                default:
                    productViewModel = productViewModel.OrderBy(s => s.Name).ToList();

                    break;
            }

            return View(productViewModel);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoryService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Category = categories;
            return View("Create");
        }

        [HttpPost] 
        public IActionResult Create(NewProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product()
                {
                    Name = product.Name,
                    Company = product.Company,
                    SerialNumber = product.SerialNumber,
                    BarCode = product.BarCode,
                    Amount = product.Amount,
                    CategoryId = product.CategoryId,
                    GrossPrice = product.GrossPrice,
                    NetPrice = product.NetPrice,
                    VAT = product.VAT,
                    Offer = 0,
                    IsBlocked = false,
                    Description = product.Description
                };
                _productService.Add(newProduct);
                return RedirectToAction("Index");


            }
            var categories = _categoryService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Category = categories;

            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();
            var categories = _categoryService.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            ViewBag.Category = categories;

            var model = new Product
            {
                Id = id,
                Name = product.Name,
                Company = product.Company,
                Amount = product.Amount,
                BarCode = product.BarCode,
                SerialNumber = product.SerialNumber,
                GrossPrice = product.GrossPrice,
                NetPrice = product.NetPrice,
                VAT = product.VAT,
                Offer = product.Offer,
                Description = product.Description,
                Category = product.Category,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var products = _productService.GetById(id);
            var category = _categoryService.GetAll();
            var photos = _photoService.GetByProduct(products.Id);
            List<byte[]> data = new List<byte[]>();
            foreach(var item in photos)
            {
                byte[] content = item.Content;
                data.Add(content);
            }
                
            
            ViewBag.Photos = data;
            return View(products);

        }

        

        private bool IsImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
