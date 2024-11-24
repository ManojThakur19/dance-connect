using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceConnect.Server.ApiModel
{
    public class UserApiModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}
