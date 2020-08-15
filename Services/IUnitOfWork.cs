using DevDemo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDemo.Services
{
    public interface IUnitOfWork
    {
        long GetTransactionNum();
        void MailSender(string name, string email, List<IFormFile> files,long TransNum);
        IQueryable<UserInfo> GetUser(long transNum, string email);
        Dictionary<string, string> GetMimeTypes();
        string GetContentType(string path);
    }
}
