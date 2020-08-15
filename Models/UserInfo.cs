using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevDemo.Models
{
    public class UserInfo
    {
        public Guid UserInfoId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long TransNumber { get; set; }
        public DateTimeOffset Date { get; set; } 
        public virtual ICollection<DocUpload> fileUploads { get; set; }
    }
}
