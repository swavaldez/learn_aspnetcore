using Microsoft.AspNetCore.Mvc;
using odetofood.Models;
using odetofood.Services;
using odetofood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odetofood.Controllers
{
    public class HomeController : Controller
    {
        IRestaurantData _restaurantData;
        IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,
            IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetMessageOfTheDay()
            };


            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RestaurantEditModel model)
        {
            var newRestaurant = new Restaurant
            {
                Name = model.Name,
                Cuisine = model.Cuisine
            };

            _restaurantData.Add(newRestaurant);

            return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
        }

    }
}
