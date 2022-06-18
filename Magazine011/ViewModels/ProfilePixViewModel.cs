using System;
using Microsoft.AspNetCore.Http;

namespace Magazine011.ViewModels
{
    public class ProfilePixViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public IFormFile Pix { get; set; }
    }
}
