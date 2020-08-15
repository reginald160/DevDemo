using DevDemo.Data;
using DevDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DevDemo.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public long GetTransactionNum()
        {
            long trackID = 0;
            var rand = new Random();
            var bytes = new byte[1];
            rand.NextBytes(bytes);
            for (int i = 1; i <= 9; i++)
            {
                trackID = (rand.Next(1, 7) + (DateTime.Now.Ticks / 1000000));
            }
            return trackID;
        }

        public void MailSender(string name, string email, List<IFormFile> files, long TransNum)
        {
            MailMessage msg = new MailMessage 
            {
                From = new MailAddress("ozougwu2016@gmail.com"), 
            };
            msg.To.Add(email); 

            msg.Subject = "Developers Test";
            msg.Body = "Hello " + name + ", \n Your transaction number is "+ TransNum  + ", find below attachment of your documents" ; 
            foreach (var filepath in files)
            {
                string fileName = Path.GetFileName(filepath.FileName);

                msg.Attachments.Add(new Attachment(filepath.OpenReadStream(), fileName));
            }

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com"
            };
            NetworkCredential credential = new NetworkCredential
            {  // Server Email credential
                UserName = "ozougwu2016@gmail.com",
                Password = "principal160"
            };
            client.Credentials = credential;
            client.EnableSsl = true;
            client.Port = 587;
            client.Send(msg);

        }
        public IQueryable<UserInfo> GetUser(long transNum, string email)
        {
            var Query = _context.userInfo.Where(T => T.TransNumber == transNum && T.Email.Contains(email)).Include(x => x.fileUploads);

            return Query;           
        }

       public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"}, 
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
