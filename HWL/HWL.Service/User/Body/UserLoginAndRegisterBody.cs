using HWL.Entity;
using HWL.Entity.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.User.Body
{
    public class UserLoginAndRegisterRequestBody
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CheckCode { get; set; }
    }

    public class UserLoginAndRegisterResponseBody
    {
        public UserBaseInfo UserInfo { get; set; }
    }
}
