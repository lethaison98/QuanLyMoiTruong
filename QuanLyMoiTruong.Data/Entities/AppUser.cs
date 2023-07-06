using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public int Piority { get; set; }
        public int Level { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
