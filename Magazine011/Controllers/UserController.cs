using System;
using System.IO;
using AutoMapper;
using Magazine011.Models;
using Magazine011.Services;
using Magazine011.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Magazine011.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UserController(IUserService userService, IMapper mapper, IWebHostEnvironment env)
        {
            _userService = userService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public IActionResult Profile(string userId)
        {
            var user = _userService.GetUserById(userId);

            // map to user details view model
            var userDetails = _mapper.Map<UserDetailViewModel>(user);

            return View(userDetails);
        }

        [HttpGet]
        public IActionResult EditProfile(string userId)
        {
            var user = _userService.GetUserById(userId);

            // map to user details view model
            var userToEdit = _mapper.Map<EditProfileViewModel>(user);

            return View(userToEdit);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            // validate if model is not null
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserById(model.Id);
            if(user == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            // map edit profile view model to user
            user.Name = model.Name;
            user.Role = model.Role;

            // update user to data source
            var updatedUserResult = _userService.UpdateUser(user);

            if (updatedUserResult)
            {
                return RedirectToAction("Index", "SQ011");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AddUserViewModel model)
        {
            // validate that the model is not empty
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserByEmail(model.Email);
            if(user != null)
            {
                ModelState.AddModelError("Invalid", "Email aready exists");
                return View(model);
            }

            // map from add user view model to user
            var userToAdd = _mapper.Map<User>(model);

            // add to data source
            var addUserResult = _userService.AddUser(userToAdd);

            return RedirectToAction("index", "SQ011");

        }

        [HttpGet]
        public IActionResult ChangeProfilePix(string userId)
        {

            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            // map from add user view model to user
            var rs = _mapper.Map<ProfilePixViewModel>(user);

            return View(rs);
        }

        [HttpPost]
        public IActionResult ChangeProfilePix(ProfilePixViewModel model)
        {
            if(model.Pix == null)
            {
                ViewBag.ErrorMsg = "Null photo";
                return View(model);
            }

            // path to the wwwroot folder
            var folderPath = _env.WebRootPath + "/images";

            // unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Pix.FileName;

            // full path
            var fullPath = Path.Combine(folderPath, uniqueFileName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                model.Pix.CopyTo(fs);
            }

            // save filename against the user
            var user = _userService.GetUserById(model.Id);
            if(user == null)
            {
                ViewBag.ErrorMsg = "Null user";
            }

            user.Photo = "/" + uniqueFileName;

            // update user
            var res = _userService.UpdateUser(user);

            return RedirectToAction("Index", "SQ011");

        }
    }
}
