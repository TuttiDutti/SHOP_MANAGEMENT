using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopManagement.Models;
using ShopManagement.Services;

namespace ShopManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        public CategoryService _categoryService;
        public ProductService _productService;

        public CategoryController(CategoryService categoryService, ProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(category);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            if(category == null)
                return NotFound();
            var model = new Category
            {
                Id = id,
                Name = category.Name,

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddOffer(int id)
        {
            var category = _categoryService.GetById(id);

            return View(category);

        }

        [HttpPost]
        public IActionResult AddOffer(int id, int offer) 
        {
            var products = _productService.GetByCategory(id);
            decimal finalOffer = (1 - ((decimal)offer / 100));

            foreach (var item in products)
            {
                    item.Offer = offer;
                    item.GrossPrice = item.GrossPrice * finalOffer;
                    item.NetPrice = item.NetPrice * finalOffer;               

                _productService.Update(item);
            }


            return RedirectToAction("Index");
        }
    }
}
