﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectRestaurant.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}