using System.Collections.Generic;
using AutoMapper;
using Magazine011.Data;
using Magazine011.Services;
using Magazine011.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Magazine011.Controllers
{
    public class SQ011Controller : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #region Do not edit this area
        public SQ011Controller(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.Users;

            // map user to the view model
            var usersToList = _mapper.Map<List<UserForListViewModel>>(users);

            return View(usersToList);
        }


    }
}



//public List<string> ClassMembers { get; set; } = new List<string>
//{
//    "Ifunanya", "Kunle", "Emma I.", "Daniel", "Hope", "Aba", "Ifeanyi",
//    "Ikechukwu", "Omolara", "Mary", "Tosin", "Akinkumi", "Chidozie", "Ikenna"
//};


//var usersToList = new List<UserForListViewModel>();
//foreach(var user in users)
//{
//    usersToList.Add(
//        new UserForListViewModel
//        {
//            Id = user.Id,
//            Name = user.Name,
//            Email = user.Email
//        }
//    );
//}