using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductShop.Controllers
{
    public class DiscountController : Controller
    {
        [Authorize(Policy = "ValuableCustomer")]
        public IActionResult Index()
        {
            return View("Discount");
        }
    }
}
