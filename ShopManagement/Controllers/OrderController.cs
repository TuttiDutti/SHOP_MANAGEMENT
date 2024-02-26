using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Services;
using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.ViewModels;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Imaging;
using IronPdf.Extensions.Mvc.Core;
using IronPdf;
using System.Diagnostics;

namespace ShopManagement.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderService _orderService;
        public UserService _userService;
        public ProductService _productService;
        public OrderItemService _orderItemService;
        public List<NewOrderViewModel> newOrders = new();
		private readonly IRazorViewRenderer _viewRenderService;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public OrderController(OrderService orderService, UserService userService, ProductService productService, OrderItemService orderItemService, IRazorViewRenderer viewRenderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
            _orderItemService = orderItemService;
			_viewRenderService = viewRenderService;
			_httpContextAccessor = httpContextAccessor;
		}


        // GET: OrderController
        public ActionResult Index(string searchString, string sortOrder, string filter)
        {
            var orders = _orderService.GetAll();
            var users = _userService.GetAll();
            var products = _productService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Number.Contains(searchString) || s.Company.Contains(searchString) || s.NIP.Contains(searchString)).ToList();
            }

            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            switch (sortOrder)
            {
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.Date).ToList();
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date).ToList();
                    break;
            }
 

            return View(orders);
        }


        [HttpGet]
        public IActionResult CreatePZ(string searchString)
        {
            var products = _productService.GetNotBLocked()
                .Where(x => x.Amount>0);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString) || s.Company.Contains(searchString)
                                         || s.SerialNumber.Contains(searchString) || s.BarCode.Contains(searchString)).ToList();
            }
            ViewBag.Search = searchString;

            return View(products);
        }
        [HttpPost]
        public IActionResult CreatePZ(List<Card> Card, string CompanyName, string Nip, string Address, string Type)
        {

            Product product = new Product();
            
            
            var user = _userService.GetById(Int32.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));
            var lastOrder = _orderService.GetAll()
                .Where(u => u.UserId == user.Id)
                .Where(t => t.Type == "PZ")
                .OrderByDescending(o => o.Id).FirstOrDefault();
            string number;
            if (lastOrder == null) number = "PZ/" + user.Login + "/0000";
            else number = lastOrder.Number;
            var nextNumber = GenerateNumber.GenerateNextNumber(number, "PZ", user.Login);
            var newOrder = new Order
            {
                Number = nextNumber,
                UserId = user.Id,
                Date = DateTime.Now,
                Company = CompanyName,
                NIP = Nip,
                Address = Address,
                Type = "PZ"
            };
            _orderService.Add(newOrder);

            foreach (var item in Card)
            {
                product = _productService.GetById(item.Id);
                product.Amount += item.Quantity;
                _productService.Update(product);
                var newItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = item.Id,
                    Amount = item.Quantity
                };
                _orderItemService.Add(newItem);
            }
            return RedirectToAction("Index");
        }

        // GET: OrderController/Create
        [HttpGet]
        public IActionResult CreateWZ(string searchString)
        {
            var products = _productService.GetNotBLocked()
                .Where(x => x.Amount > 0);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString) || s.Company.Contains(searchString)
                                         || s.SerialNumber.Contains(searchString) || s.BarCode.Contains(searchString)).ToList();
            }
            ViewBag.Search = searchString;

            return View(products);
        }
        [HttpPost]
        public IActionResult CreateWZ(List<Card> Card, string CompanyName, string Nip, string Address, string Type)
        {
            Product product= new Product();
            var user = _userService.GetById(Int32.Parse(HttpContext.User.Claims.Where(u => u.Type.Contains("nameidentifier")).FirstOrDefault().Value));
            var lastOrder = _orderService.GetAll()
                .Where(u => u.UserId == user.Id)
                .Where(t => t.Type == "WZ")
                .OrderByDescending(o => o.Id).FirstOrDefault();
            string number;
            if (lastOrder == null) number = "WZ/" + user.Login + "/0000";
            else number = lastOrder.Number;
            var nextNumber = GenerateNumber.GenerateNextNumber(number, "WZ", user.Login);
            var newOrder = new Order
            {
                Number = nextNumber,
                UserId = user.Id,
                Date = DateTime.Now,
                Company = CompanyName,
                NIP = Nip,
                Address= Address,
                Type = "WZ"
            };
            _orderService.Add(newOrder);

            foreach (var item in Card)
            {
                product = _productService.GetById(item.Id);
                product.Amount -= item.Quantity;
                _productService.Update(product);
                var newItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = item.Id,
                    Amount = item.Quantity
                };
                _orderItemService.Add(newItem);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _orderService.GetById(id);
            var user = _userService.GetById(order.UserId);
            var userName = user.Name + " " + user.LastName;
            var products = _orderItemService.GetAll()
                .Where(o => o.OrderId == id);

            List<ProductOrderViewModel> list = new List<ProductOrderViewModel>();
            Product product;

            foreach(var item in products)
            {
                product = _productService.GetById(item.ProductId);

                var newProduct = new ProductOrderViewModel()
                {
                    Name = product.Name,
                    Company = product.Company,
                    SerialNumber = product.SerialNumber,
                    NetPrice = product.NetPrice,
                    Amuont = item.Amount,
                    Total = product.NetPrice * item.Amount
                };

                list.Add(newProduct);
            }


            var productOrder = new OrderViewModel()
            {
                Number = order.Number,
                User = userName,
                CreatedDate = order.Date,
                Company = order.Company,
                Address = order.Address,
                NIP = order.NIP,
                ProductOrders = list
            };
            return View(productOrder);
        }

        // GET: OrderController/Edit/5

        //Details -> pobranie dokumentu z bazy powiązanego z zamówieniem
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var order = _orderService.GetById(id);
            var user = _userService.GetById(order.UserId);
            var userName = user.Name + " " + user.LastName;
            var products = _orderItemService.GetAll()
                .Where(o => o.OrderId == id);

            List<ProductOrderViewModel> list = new List<ProductOrderViewModel>();
            Product product;

            foreach (var item in products)
            {
                product = _productService.GetById(item.ProductId);

                var newProduct = new ProductOrderViewModel()
                {
                    Name = product.Name,
                    Company = product.Company,
                    SerialNumber = product.SerialNumber,
                    NetPrice = product.NetPrice,
                    VAT = product.VAT,
                    GrossPrice = product.GrossPrice,
                    Amuont = item.Amount,
                    Total = product.NetPrice * item.Amount
                };

                list.Add(newProduct);
            }


            var productOrder = new OrderViewModel()
            {
                Number = order.Number,
                User = userName,
                CreatedDate = order.Date,
                Company = order.Company,
                Address = order.Address,
                NIP = order.NIP,
                ProductOrders = list,
                Type = order.Type,
            };
            if (_httpContextAccessor.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                ChromePdfRenderer renderer = new ChromePdfRenderer();
                // Render View to PDF document
                productOrder.CreatedDate = productOrder.CreatedDate.Date;
                PdfDocument pdf = renderer.RenderRazorViewToPdf(_viewRenderService, "Views/Order/GeneratePdf.cshtml", productOrder);
                Response.Headers.Add("Content-Disposition", "inline");
                // Output PDF document
                
                return File(pdf.BinaryData, "application/pdf", productOrder.Number +".pdf");
			}
            return RedirectToAction("Index");
		}

	}


}
    

