using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDemo.Models
{
    public class DocUpload
    {
        public Guid DocUploadId { get; set; }
        public string FileName { get; set; }
        public Guid UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
