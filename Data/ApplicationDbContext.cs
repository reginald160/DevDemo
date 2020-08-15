using System;
using System.Collections.Generic;
using System.Text;
using DevDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserInfo> userInfo{ get; set; }

        public DbSet<DocUpload> docUploads{ get; set; }
    }
}
