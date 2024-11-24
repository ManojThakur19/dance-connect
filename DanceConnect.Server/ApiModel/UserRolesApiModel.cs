using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceConnect.Server.ApiModel
{
    public class UserRolesApiModel
    {
        public string UserId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
