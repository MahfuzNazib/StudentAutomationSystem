using Microsoft.AspNetCore.Mvc;
using StudentAutomationSystem.Models.Product;

namespace StudentAutomationSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ProductDBHandel _productDBHandel;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _productDBHandel = new ProductDBHandel(_configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewProduct(Product product)
        {
            string Message = "";
            try
            {
                if (_productDBHandel.AddNewProduct(product))
                {
                    Message = "Product Saved";
                    return RedirectToAction("AllProducts", TempData["Message"] = Message);
                }
                else
                {
                    Message = "Something Wrong";
                    return RedirectToAction("AllProducts", TempData["Message"] = Message);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
                return RedirectToAction("AllProducts", TempData["Message"] = Message);
            }
            
        }

        [HttpGet]
        public ActionResult AllProducts()
        {
            List<Product> products = _productDBHandel.GetAllProduct();
            return View(products);
        }
    }
}
