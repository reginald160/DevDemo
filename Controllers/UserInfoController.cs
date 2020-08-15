using DevDemo.Data;
using DevDemo.Models;
using DevDemo.Services;
using DevDemo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevDemo.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public UserInfoController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public IActionResult UserUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserUpload(UserInfoViewModel Vmodel)
        {
            if (ModelState.IsValid)
            {  
                UserInfo user = new UserInfo()
                {   UserInfoId = Vmodel.UserInfoId,
                    Name = Vmodel.Name,
                    Email = Vmodel.Email,
                    Address = Vmodel.Address,
                    TransNumber = _unitOfWork.GetTransactionNum(),
                    Date = DateTimeOffset.Now
                };
                _context.userInfo.Add(user);
                string webrootPath = _hostEnvironment.WebRootPath;
                var files = Vmodel.UserFiles;
                var uploads = Path.Combine(webrootPath, "images");

                for (int i = 0; i < files.Count; i++)
                {
                    string fileName = Vmodel.Name + files[i].FileName;
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[i].CopyTo(fileStreams);
                    }
                    var uploadFile = new DocUpload
                    {     
                        UserInfoId = user.UserInfoId,
                        FileName =  fileName
                    };
                    _context.docUploads.Add(uploadFile); 
                }
                await _context.SaveChangesAsync();
                if (_context.SaveChangesAsync().IsCompleted)
                {
                    _unitOfWork.MailSender(Vmodel.Name, Vmodel.Email, files, user.TransNumber);
                    TempData["Message"] = "File successfully uploaded";

                    return RedirectToAction("GetUser");

                }        
               
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUser(UserSearchViewModel Vmodel)
        {
            if (ModelState.IsValid)
            {

                var user = _unitOfWork.GetUser(Vmodel.TransNum, Vmodel.email); 
                

                return View("UserDetails", user);
                
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserDetails ()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        public async Task<IActionResult> Download(Guid id)
        {
            var filename = await _context.docUploads.Where(x => x.DocUploadId == id).FirstOrDefaultAsync();
            if (filename == null)
                return Content("filename not present");
            string webrootPath = _hostEnvironment.WebRootPath;
            var uploads = Path.Combine(webrootPath, "images");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           uploads, filename.FileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory,_unitOfWork.GetContentType(path), Path.GetFileName(path));
        }



    }
}
