using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;


        public CartController(IProductRepository repo, IOrderProcessor orderproc)
        {
            repository = repo;
            orderProcessor = orderproc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl, int quantity = 1)
        {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

      
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

		public RedirectToRouteResult UpdateQuantity(Cart cart, int productId, int quantity, string returnUrl)
		{
			return RedirectToAction("Index", new { returnUrl });
		}

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

		[HttpGet]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails model)
		{
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, model);
                cart.Clear();
                return View("Completed");
            }
			return View(model);
		}

		public ViewResult Completed(Cart cart)
		{
			return View();
		}
	}
}