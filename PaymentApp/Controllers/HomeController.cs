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
                _logger.LogError(ex, "An error occurred");
                return View("Index", new List<PaymentViewModel>());
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var paymentDetail = await _payment.GetPaymentDetail(id);
                var viewModel = new PaymentViewModel
                {
                    PaymentDetailId = paymentDetail.PaymentDetailId,
                    CardOwnerName = paymentDetail.CardOwnerName,
                    CardNumber = paymentDetail.CardNumber,
                    ExpirationDate = paymentDetail.ExpirationDate,
                    SecurityCode = paymentDetail.SecurityCode
                };

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return View("Index", new List<PaymentViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var paymentDetail = new PaymentDetailModel
                    {
                        CardOwnerName = viewModel.CardOwnerName,
                        CardNumber = viewModel.CardNumber,
                        ExpirationDate = viewModel.ExpirationDate,
                        SecurityCode = viewModel.SecurityCode
                    };

                    await _payment.PostPaymentDetail(paymentDetail);

                    return Json(new { success = true });  // Return success response
                }
                else
                {
                    return BadRequest(ModelState);  // Return validation errors
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");  // Return generic server error
            }
        }




        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var paymentDetail = await _payment.GetPaymentDetail(id);
                var viewModel = new PaymentViewModel
                {
                    PaymentDetailId = paymentDetail.PaymentDetailId,
                    CardOwnerName = paymentDetail.CardOwnerName,
                    CardNumber = paymentDetail.CardNumber,
                    ExpirationDate = paymentDetail.ExpirationDate,
                    SecurityCode = paymentDetail.SecurityCode
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, PaymentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var paymentDetail = new PaymentDetailModel
                    {
                        PaymentDetailId = viewModel.PaymentDetailId,
                        CardOwnerName = viewModel.CardOwnerName,
                        CardNumber = viewModel.CardNumber,
                        ExpirationDate = viewModel.ExpirationDate,
                        SecurityCode = viewModel.SecurityCode
                    };

                    await _payment.PutPaymentDetail(id, paymentDetail);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _payment.DeletePaymentDetail(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return RedirectToAction("Index");
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
