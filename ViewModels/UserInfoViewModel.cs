using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevDemo.ViewModels
{
    public class UserInfoViewModel
    {
        [Required,Display(Name = "Full Name", Prompt = "Enter Full Name")]
        public string Name { get; set; }
        [Required,Display(Name = "Email", Prompt = "Enter Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required,DataType(DataType.MultilineText),Display(Name = "Address", Prompt = "Enter Address")]    
        public string Address { get; set; }    
        [Required,Display(Name = "Upload Documents"), DataType(DataType.Upload)]
        public List<IFormFile> UserFiles { get; set; }
        public Guid UserInfoId { get; set; }
       
    }
}
