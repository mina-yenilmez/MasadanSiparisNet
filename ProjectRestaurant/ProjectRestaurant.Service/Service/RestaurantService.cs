﻿using Microsoft.AspNetCore.Identity;
using ProjectRestaurant.Data.Context;
using ProjectRestaurant.Data.Entities;
using ProjectRestaurant.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRestaurant.Service.Service
{
    public class RestaurantService
    {
        private readonly UserManager<Restaurant> _userManager;
        private readonly SignInManager<Restaurant> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RestaurantDbContext _context;
        
        public RestaurantService(
            UserManager<Restaurant> userManager,
            SignInManager<Restaurant> signInManager,
            RoleManager<IdentityRole> roleManager,
            RestaurantDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IdentityResult> NewRestaurant(NewRestaurantDto model)
        {
            IdentityResult result = new IdentityResult();
            var user = new Restaurant {
                Email=model.Email,
                UserName=model.UserName
            };
            result= await _userManager.CreateAsync(user, model.Password);
            var temp = await _userManager.FindByNameAsync(user.UserName);
            RestaurantAddress adres = new RestaurantAddress
            {
                Restaurant = temp,
                RestaurantId = user.Id
            };
            _context.Set<RestaurantAddress>().Add(adres);
            await _context.SaveChangesAsync();
            return result;
            
        }
        public async Task<SignInResult> Login(RestoranLoginDto model)
        {
            SignInResult result = new SignInResult();
            
            var user = await _userManager.FindByNameAsync(model.UserName);
            result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            return result;
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<List<Table>> TableList(string userName)
        {
            var user =await _userManager.FindByNameAsync(userName);
            var masalar = _context.Set<Table>().Where(x => x.RestaurantId == user.Id).ToList();
            return masalar;
        }
        public async void AddTable(Table masa, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            masa.Restaurant = user;
            masa.RestaurantId = user.Id;
             _context.Set<Table>().Add(masa);
            var result =await _context.SaveChangesAsync();
        }
        public async Task<Restaurant> GetRestaurantInfo(string userName)
        {
            var restaurant =await _userManager.FindByNameAsync(userName);
            
            return restaurant;
        }
        public async Task<IdentityResult> UpdateRestaurantInfo(Restaurant model)
        {
            var rest = await _userManager.FindByNameAsync(model.UserName);
            rest.RestaurantAddress = model.RestaurantAddress;
            rest.AmountOfWorkers = model.AmountOfWorkers;
            rest.Email = model.Email;
            rest.PhoneNumber = model.PhoneNumber;
            rest.RestaurantName = model.RestaurantName;
            rest.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(rest);
            return result;
        }
    }
}
