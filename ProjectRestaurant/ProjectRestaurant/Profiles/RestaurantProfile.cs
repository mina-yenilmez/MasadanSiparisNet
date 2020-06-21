﻿using AutoMapper;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Models;
using ProjectRestaurant.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Profiles
{
    public class RestaurantProfile:Profile
    {
        public RestaurantProfile()
        {
            CreateMap<NewRestaurantSignUpViewModel, NewRestaurantDto>();
            CreateMap<RestaurantSignInViewModel, RestoranLoginDto>();
            CreateMap<RestaurantEditViewModel, Restaurant>();
            CreateMap<Restaurant, RestaurantEditViewModel>();

            CreateMap<RestaurantAdresViewModel, RestaurantAddress>();
            CreateMap<RestaurantAddress, RestaurantAdresViewModel>();

            CreateMap<Menu, MenuViewModel>();
            CreateMap<MenuViewModel, Menu>();
        }
    }
}
