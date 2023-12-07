using FoodOrderApp.Data;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _applicationDbContext;

        public PaymentController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index(string cartAppUserId)
        {
            // Get cart User
            var cart = _applicationDbContext.Carts.Include(e => e.Foods).ToList();
            var cartUser = cart.FirstOrDefault(cart => cart.AppUserId == cartAppUserId);
            // Get order User
            var order = _applicationDbContext.Orders.Include(e => e.Foods).ToList();
            var orderUser = order.FirstOrDefault(order => order.AppUserId == cartAppUserId);

            // Lazy loading include column food in CartDetail
            var cartDetails = _applicationDbContext.Carts
                .Where(cart => cart.AppUserId == cartAppUserId)
                .SelectMany(e => e.Foods)
                .Include(cd => cd.Food).ToList();

            var payment = new PaymentViewModel()
            {
                CartUser = cartUser,
                OrderUser = orderUser,
                CartUserDetails = cartDetails
            };

            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Console.WriteLine(orderViewModel?.AppUserId);

            if (orderViewModel == null)
            {
                return BadRequest("Invalid order data");
            }

            try
            {

                var order = new Order()
                {
                    OrderDate = orderViewModel.OrderDate,
                    ReceiveDate = orderViewModel.ReceiveDate,
                    OrderStatus = orderViewModel.OrderStatus,
                    FormDelivery = orderViewModel.FormDelivery,
                    Receiver = orderViewModel.Receiver,
                    Location = orderViewModel.Location,
                    PhoneNumber = orderViewModel.PhoneNumber,
                    Note = orderViewModel.Note,
                    AppUserId = orderViewModel.AppUserId
                };

                var cartDetails = _applicationDbContext.Carts
                 .Where(cart => cart.AppUserId == orderViewModel.AppUserId)
                 .SelectMany(e => e.Foods)
                 .Include(cd => cd.Food).ToList();

                var orderDetails = cartDetails.Select(cd => new OrderDetail
                {
                    Quantity = cd.Quantity,
                    FoodId = cd.FoodId,
                    Food = cd.Food,
                    Order = order,
                    OrderId = order.Id
                }).ToList();

                order.Foods = orderDetails;

                _applicationDbContext.Orders.Add(order);
                var result = _applicationDbContext.SaveChangesAsync();

                if (result.Result > 0)
                {
                    // The order has been successfully added to the database
                    return Json(new { success = true, orderId = order.Id });
                }
                else
                {
                    // No rows were affected, indicating an issue with the database operation
                    return StatusCode(500, "Failed to add the order to the database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        public class OrderViewModel
        {
            public DateTime OrderDate { get; set; }
            public DateTime ReceiveDate { get; set; }
            public OrderStatusCategory OrderStatus { get; set; }
            public FormDeliveryCategory FormDelivery { get; set; }
            public string Receiver { get; set; }
            public string Location { get; set; }
            public string PhoneNumber { get; set; }
            public string Note { get; set; }
            public string AppUserId { get; set; }
        }
    }
}
