using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.User;

namespace UberEats.Core.Application.DTO.Account
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public UserViewModel UserVm { get; set; }
    }
}
