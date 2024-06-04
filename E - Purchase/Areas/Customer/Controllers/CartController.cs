using E_Purchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_Models.Models;
using E_Purchase_Models.ViewModels;
using E_Purchase_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E___Purchase.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIdGuid = Guid.Parse(userId);

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUseId == userIdGuid,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                double price = GetPriceBasedOnQuantity(cart);
                if (ShoppingCartVM.OrderHeader.OrderTotal != null)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += price * cart.Count;
                }
            }

            return View(ShoppingCartVM);
        }

		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost(Guid cartId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIdGuid = Guid.Parse(userId);


            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUseId == userIdGuid,
               includeProperties: "Product");

            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);



            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                double price = GetPriceBasedOnQuantity(cart);
                if (ShoppingCartVM.OrderHeader.OrderTotal != null)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += price * cart.Count;
                }
            }

            if (ShoppingCartVM.OrderHeader.ApplicationUser.CompanyId.GetValueOrDefault() == null)
            {
                //It is a regular customer account.
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.StatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
				//Its a company user
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = (Guid)cart.ProductId,
                    OrderHeaderId = (Guid)ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Product.Price,
                    Count = cart.Count
                };
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}

			if (ShoppingCartVM.OrderHeader.ApplicationUser.CompanyId.GetValueOrDefault() == null)
			{
				//It is a regular customer account, we need to capture payment.
                //To implement stripe logic
				
			}

			return RedirectToAction(nameof(OrderConfirmation), new {id = ShoppingCartVM.OrderHeader.Id});
        }

        public IActionResult OrderConfirmation(Guid Id)
        {
            return View();
        }




		public IActionResult Summary(Guid cartId)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			var userIdGuid = Guid.Parse(userId);

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUseId == userIdGuid,
				includeProperties: "Product"),
				OrderHeader = new()
			};

			ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostCode;



			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				double price = GetPriceBasedOnQuantity(cart);
				if (ShoppingCartVM.OrderHeader.OrderTotal != null)
				{
					ShoppingCartVM.OrderHeader.OrderTotal += price * cart.Count;
				}
			}

			return View(ShoppingCartVM);
		}

		public IActionResult plus(Guid cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                cartFrmDb.Count += 1;
            }
            _unitOfWork.ShoppingCart.Update(cartFrmDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(Guid cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                if (cartFrmDb.Count <= 1)
                {
                    _unitOfWork.ShoppingCart.Remove(cartFrmDb);

                }else
                {
                    cartFrmDb.Count -= 1;
                    _unitOfWork.ShoppingCart.Update(cartFrmDb);

                }
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));


        }

        public IActionResult remove(Guid cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                _unitOfWork.ShoppingCart.Remove(cartFrmDb);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        


        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count >= 50 && shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }

    }
}
