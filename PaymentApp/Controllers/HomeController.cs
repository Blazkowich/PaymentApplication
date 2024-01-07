using Microsoft.AspNetCore.Mvc;
using PaymentApp.Models;
using Services;
using Services.Models;
using System.Diagnostics;

namespace PaymentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentDetails _payment;

        public HomeController(ILogger<HomeController> logger, IPaymentDetails payment)
        {
            _logger = logger;
            _payment = payment;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            try
            {
                var actionResult = await _payment.GetPaymentDetails();

                    var viewModelList = actionResult.Select(pd => new PaymentViewModel
                    {
                        PaymentDetailId = pd.PaymentDetailId,
                        CardOwnerName = pd.CardOwnerName,
                        CardNumber = pd.CardNumber,
                        ExpirationDate = pd.ExpirationDate,
                        SecurityCode = pd.SecurityCode
                    }).ToList();

                    return View("Index", viewModelList);
               
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                _logger.LogError(ex, "An error occurred");
                return View("Index", new List<PaymentViewModel>());
            }
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
