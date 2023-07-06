using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Roles { get; set; }
        public string Claims { get; set; }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
